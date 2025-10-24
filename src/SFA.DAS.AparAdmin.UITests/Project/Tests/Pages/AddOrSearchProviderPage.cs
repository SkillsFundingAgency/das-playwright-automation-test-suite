namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

public class AddOrSearchProvidersPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Staff dashboard");
    }

    public async Task<SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddOrSearchProvidersPage> ClickAddOrSearchForProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add or search for a provider" }).ClickAsync();

        return await VerifyPageAsync(() => new SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddOrSearchProvidersPage(context));
    }

    public async Task<SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchforATrainingProviderPage> ClickSearchForATrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Search for a training provider" }).ClickAsync();

        // Fix typo: 'SearchforATrainingProvidersPag' -> 'SearchforATrainingProvidersPage'
        return await VerifyPageAsync(() => new SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchforATrainingProviderPage(context));
    }

    public async Task<SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate.UKPRNAllowListPage> ClickAddUkprnToAllowList()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add a UKPRN to the allow list" }).ClickAsync();

        return await VerifyPageAsync(() => new SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate.UKPRNAllowListPage(context));
    }

    public async Task<SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney.UKPRNPage> ClickAddNewTrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add a new training provider" }).ClickAsync();

        return await VerifyPageAsync(() => new SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney.UKPRNPage(context));
    }
}
