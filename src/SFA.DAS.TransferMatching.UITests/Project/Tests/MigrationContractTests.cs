using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests;

[TestFixture]
[Category("Unittests")]
public class MigrationContractTests
{
    [Test]
    public void EveryMigratedScenarioStepHasExactlyOneBinding()
    {
        var assembly = typeof(MigrationContractTests).Assembly;
        var config = System.Text.Json.JsonDocument.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "reqnroll.json")));
        var assemblies = config.RootElement.GetProperty("bindingAssemblies").EnumerateArray()
            .Select(x => Assembly.Load(x.GetProperty("assembly").GetString())).Append(assembly).ToArray();
        var bindings = assemblies.SelectMany(x => x.GetTypes())
            .Where(x => x.GetCustomAttribute<BindingAttribute>() != null)
            .SelectMany(x => x.GetMethods())
            .SelectMany(method => method.GetCustomAttributes<StepDefinitionBaseAttribute>()
                .Select(attribute => (Method: method, Attribute: attribute))).ToArray();
        var features = assembly.GetManifestResourceNames().Where(x => x.EndsWith(".feature")).ToArray();
        Assert.That(features, Has.Length.EqualTo(39), "Every source feature must be preserved.");
        var failures = new List<string>();
        foreach (var feature in features)
        {
            using var reader = new StreamReader(assembly.GetManifestResourceStream(feature));
            string kind = null;
            while (reader.ReadLine() is { } line)
            {
                var match = Regex.Match(line.Trim(), @"^(Given|When|Then|And|But)\s+(.+)$");
                if (!match.Success) continue;
                if (match.Groups[1].Value is "Given" or "When" or "Then") kind = match.Groups[1].Value;
                var step = match.Groups[2].Value.Trim();
                var count = bindings.Where(x => x.Attribute.GetType().Name == kind + "Attribute")
                    .Count(x => Regex.IsMatch(step, "^(?:" + x.Attribute.Expression + ")$"));
                if (count != 1) failures.Add($"{feature}: {kind} {step}: {count} bindings");
            }
        }
        Assert.That(failures, Is.Empty, string.Join(Environment.NewLine, failures));
    }

    [TestCase(12, 18000, "18,000")]
    [TestCase(24, 18000, "7,200")]
    [TestCase(24, 36000, "14,400")]
    public async Task EstimatedFirstYearCostUsesTheApplicationCostingModel(int duration, int cost, string expected)
    {
        var course = await new SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers.CoursesDataHelper().GetCourse("2");
        var helper = new TMDataHelper(course) { Duration = duration, Cost = cost };
        using var culture = new CultureScope();
        Assert.That(helper.GetEstimatedCostOfTrainingForApplicationDetail(), Is.EqualTo(expected));
    }

    [Test]
    public void AllApplicationStatusesHaveReceiverLabels() =>
        Assert.That(Enum.GetValues<ApplicationStatus>().Select(x => x.GetLabelForReceiver()), Has.None.Empty);

    [Test]
    public void PageObjectsUseTheSharedPlaywrightBaseAndAsyncActions()
    {
        var assembly = typeof(MigrationContractTests).Assembly;
        var pages = assembly.GetTypes().Where(x => x.IsClass && !x.IsNested
            && x.Namespace == "SFA.DAS.TransferMatching.UITests.Project.Tests.Pages").ToArray();
        Assert.That(pages, Is.Not.Empty);
        foreach (var page in pages.Where(x => !x.IsAbstract))
        {
            Assert.That(typeof(BasePage).IsAssignableFrom(page), Is.True, page.Name);
            var synchronousActions = page.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => !x.IsSpecialName && x.ReturnType == typeof(void)).Select(x => x.Name).ToArray();
            Assert.That(synchronousActions, Is.Empty, $"{page.Name} must await page actions.");
        }
        Assert.That(assembly.GetReferencedAssemblies().Select(x => x.Name), Has.None.Contains("Selenium"));
    }

    [Test]
    public void PledgeTrackingUsesTheActiveSenderRatherThanTheFirstConfiguredAccount()
    {
        var context = new ObjectContext();
        var users = new UserCreds("test-sender", "test-user-reference");
        users.AccountDetails.Add(new(0, "1", "FIRST", "First account", "", "First account", "", "", ""));
        users.AccountDetails.Add(new(1, "2", "SECOND", "Second account", "", "Second account", "", "", ""));
        context.Set("users", users);
        context.SetOrganisationName("Second account");
        context.SetPledgeDetailList();
        context.SetPledgeAmount(50000);
        context.SetPledgeCreatedOn(new DateTime(2026, 1, 1));
        context.SetPledgeDetail("PLEDGE", "FIRST");
        var pledge = context.GetPledgeDetail();
        Assert.Multiple(() =>
        {
            Assert.That(pledge.EmployerAccountId, Is.EqualTo("2"));
            Assert.That(pledge.SenderHashedAccountId, Is.EqualTo("SECOND"));
            Assert.That(pledge.Amount, Is.EqualTo(50000));
        });
        context.SetPledgeApplication("PLEDGE", "APPLICATION");
        Assert.That(context.GetPledgeApplication("PLEDGE"), Is.EqualTo("APPLICATION"));
    }

    private sealed class CultureScope : IDisposable
    {
        private readonly System.Globalization.CultureInfo previous = System.Globalization.CultureInfo.CurrentCulture;
        public CultureScope() => System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
        public void Dispose() => System.Globalization.CultureInfo.CurrentCulture = previous;
    }
}
