using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

public class ManageTrainingProviderInformationPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage training provider information");
    }

    public async Task<SearchforATrainingProviderPage> ClickSearchForATrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Search for a training provider" }).ClickAsync();

        // Fix typo: 'SearchforATrainingProvidersPag' -> 'SearchforATrainingProvidersPage'
        return await VerifyPageAsync(() => new SearchforATrainingProviderPage(context));
    }

    public async Task<UKPRNAllowListPage> ClickAddUkprnToAllowList()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add a UKPRN to the allow list" }).ClickAsync();

        return await VerifyPageAsync(() => new UKPRNAllowListPage(context));
    }

    public async Task<UKPRNPage> ClickAddNewTrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add a new training provider" }).ClickAsync();

        return await VerifyPageAsync(() => new UKPRNPage(context));
    }
}
