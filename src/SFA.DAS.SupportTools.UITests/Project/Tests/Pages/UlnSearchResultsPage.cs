namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class UlnSearchResultsPage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("View ULN");

    public async Task<UlnDetailsPage> SelectULN(CohortDetails cohortDetails)
    {
        await page.GetByRole(AriaRole.Row, new() { Name = cohortDetails.UlnName }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new UlnDetailsPage(context, cohortDetails));
    }
}