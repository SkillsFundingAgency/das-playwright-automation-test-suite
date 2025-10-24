namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

public class SearchforATrainingProviderPage : BasePage
{
    public SearchforATrainingProviderPage(ScenarioContext context) : base(context)
    {
    }

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search for a training provider");
    }

    public async Task<SearchforATrainingProviderPage> ClickSearchForATrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Search for a training provider" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchforATrainingProviderPage(context));
    }
    public async Task<ManageTrainingProviderInformationPage> GoBackToManageTrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back" }).ClickAsync();
        return await VerifyPageAsync(() => new ManageTrainingProviderInformationPage(context));
    }
}