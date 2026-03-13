namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class SelectAStandardPage(ScenarioContext context, string pageTitle = "Select a standard") : ManagingStandardsBasePage(context)
{
    private readonly string _pageTitle = pageTitle;
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(_pageTitle);
    }

    public async Task<AddAstandardPage> SelectAStandardAndContinue(string standardName)
    {
        await page.Locator("#SelectedLarsCode").FillAsync(standardName);

        await page.GetByRole(AriaRole.Option, new() { Name = standardName }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAstandardPage(context, standardName));
    }
}
