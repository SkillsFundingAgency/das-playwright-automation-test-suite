using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SFA.DAS.Transfers.UITests.Project.Tests.Contracts;

[TestFixture]
[Category("Unittests")]
public class TransfersBindingContractTests
{
    private static readonly Assembly Assembly = typeof(TransfersBindingContractTests).Assembly;

    public static IEnumerable<TestCaseData> Features() =>
        Assembly.GetManifestResourceNames().Where(x => x.EndsWith(".feature", StringComparison.Ordinal))
            .Select(x => new TestCaseData(x).SetName($"BindingCoverage_{x.Split('.')[^2]}"));

    [TestCaseSource(nameof(Features))]
    public void EveryFeatureStepHasExactlyOneDiscoverableBinding(string resourceName)
    {
        var registrations = BindingAssemblies().SelectMany(x => x.GetTypes())
            .Where(x => x.CustomAttributes.Any(a => a.AttributeType == typeof(BindingAttribute)))
            .SelectMany(type => type.GetMethods().SelectMany(method => method.CustomAttributes
                .Where(attribute => attribute.AttributeType.Name is "GivenAttribute" or "WhenAttribute" or "ThenAttribute")
                .Select(attribute => (Type: type, Method: method, Kind: attribute.AttributeType.Name[..^9],
                    Pattern: (string)attribute.ConstructorArguments[0].Value))))
            .ToList();

        using var stream = Assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        var lines = reader.ReadToEnd().Split('\n').Select(x => x.Trim()).ToList();
        var tags = lines.Where(x => x.StartsWith('@')).SelectMany(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(x => x.TrimStart('@')).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var stepKind = string.Empty;
        var checkedSteps = 0;
        foreach (var line in lines)
        {
            var step = Regex.Match(line, @"^(Given|When|Then|And|But)\s+(.+)$");
            if (!step.Success)
                continue;
            if (step.Groups[1].Value is not ("And" or "But"))
                stepKind = step.Groups[1].Value;
            var text = step.Groups[2].Value;
            var matches = registrations.Where(x => x.Kind == stepKind
                && ScopeMatches(x.Type.CustomAttributes.Concat(x.Method.CustomAttributes), tags)
                && Regex.IsMatch(text, $"^(?:{x.Pattern})$")).ToList();
            Assert.That(matches.Count, Is.EqualTo(1),
                $"{resourceName}: {stepKind} {text} matched {matches.Count} bindings: {string.Join(", ", matches.Select(x => $"{x.Type.FullName}.{x.Method.Name}"))}");
            checkedSteps++;
        }
        Assert.That(checkedSteps, Is.GreaterThan(0), $"No steps checked in {resourceName}.");
    }

    [Test]
    public void TransfersUiBindingsReturnTasks()
    {
        var methods = Assembly.GetTypes().Where(x => x.Namespace == "SFA.DAS.Transfers.UITests.Project.Tests.Steps")
            .SelectMany(x => x.GetMethods()).Where(x => x.CustomAttributes.Any(a =>
                a.AttributeType.Name is "GivenAttribute" or "WhenAttribute" or "ThenAttribute")).ToList();
        Assert.That(methods, Is.Not.Empty);
        foreach (var method in methods)
            Assert.That(typeof(Task).IsAssignableFrom(method.ReturnType), Is.True, $"{method.DeclaringType?.Name}.{method.Name} must propagate asynchronous work.");
    }

    private static IEnumerable<Assembly> BindingAssemblies()
    {
        yield return Assembly;
        using var config = JsonDocument.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "reqnroll.json")));
        foreach (var registration in config.RootElement.GetProperty("bindingAssemblies").EnumerateArray())
            yield return System.Reflection.Assembly.Load(registration.GetProperty("assembly").GetString());
    }

    private static bool ScopeMatches(IEnumerable<CustomAttributeData> attributes, HashSet<string> tags)
    {
        var scopes = attributes.Where(x => x.AttributeType == typeof(ScopeAttribute)).ToList();
        if (scopes.Count == 0)
            return true;
        return scopes.Any(scope => scope.NamedArguments.All(argument =>
            argument.MemberName == "Tag" && tags.Contains((string)argument.TypedValue.Value)));
    }
}
