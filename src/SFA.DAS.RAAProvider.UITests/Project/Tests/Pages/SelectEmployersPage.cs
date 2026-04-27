
using System.Linq;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

public class SelectEmployersPage(ScenarioContext context) : RaaBasePage(context)
{
    private List<(string HashedId, string Value)> _values = [];

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Which employer do you want to create a vacancy for?");
    }

    public async Task<(CreateAnApprenticeshipAdvertOrVacancyPage, bool)> SelectEmployer(string empHashedId)
    {
        List<string> employers = await GetEmployers(empHashedId);

        var validemployers = await context.Get<ProviderCreateVacancySqlDbHelper>().GetValidHashedId(employers);

        var hashedid = GetRandomElementFromListOfElements(validemployers);

        (string hashedidvalue, int noOfLegalEntity) = ((string)hashedid[0], (int)hashedid[1]);

        objectContext.SetDebugInformation($"Employer with hashed id '{hashedidvalue}' has {noOfLegalEntity} legal entities");

        var legalEntity = _values.Where(x => x.HashedId == hashedidvalue).ToList();

        objectContext.SetDebugInformation($"Employer with hashed id '{hashedidvalue}' has {legalEntity.Count} legal entities with provider permission");

        var value = GetRandomElementFromListOfElements(legalEntity).Value;

        await page.Locator($".govuk-table .das-button--inline-link[value='{value}']").ClickAsync();

        if (noOfLegalEntity > 1) noOfLegalEntity = legalEntity.Count;

        objectContext.SetDebugInformation($"Selected employer with hashed id '{value}' who has {noOfLegalEntity} legal entities with provider permission");

        await page.Locator("#confirm-yes").ClickAsync();

        await SaveAndContinue();
        
        return (await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context)), noOfLegalEntity > 1);
    }

    private static T GetRandomElementFromListOfElements<T>(List<T> elements) => RandomDataGenerator.GetRandomElementFromListOfElements(elements);
    
    public async Task<List<string>> GetEmployers(string empHashedId)
    {
        _values = await GetEmpDetails();
        if (string.IsNullOrEmpty(empHashedId)) return _values.Select(x => x.HashedId).ToList();
        return [empHashedId];
    }

    public async Task<List<(string hashedId, string value)>> GetEmpDetails()
    {
        var value = await page.Locator(".govuk-table .das-button--inline-link")
        .EvaluateAllAsync<string[]>("elements => elements.map(e => e.value)");

        return value.Select(x => (x?.Split('|')[1], x)).ToList();
    }
}
