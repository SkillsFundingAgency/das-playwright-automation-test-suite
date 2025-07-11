namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages;

public class AP_PR1_SearchForYourOrganisationPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search for your organisation");
    }

    public async Task<AP_PR2_SearchResultsForPage> EnterInvalidOrgNameAndSearchInSearchForYourOrgPage(string searchTerm)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search using either the:" }).FillAsync(searchTerm);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        return await VerifyPageAsync(() => new AP_PR2_SearchResultsForPage(context));
    }
}

public class AP_PR2_SearchResultsForPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("form")).ToContainTextAsync("Search results for");
    }

    public async Task VerifyInvalidSearchResultText()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("We cannot find your organisation details");
    }

    public async Task EnterInvalidOrgNameAndSearchInSearchResultsForPage(string searchTerm)
    {
        await page.Locator("#SearchString").FillAsync(searchTerm);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    }
}
