namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;

public class BatchSearchPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("label")).ToContainTextAsync("Batch search");

    public async Task<BatchSearchResultsPage> SearchBatches()
    {
        await page.Locator("#BatchNumber").FillAsync("1080");

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        return await VerifyPageAsync(() => new BatchSearchResultsPage(context));
    }
}

public class BatchSearchResultsPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search results");

    public async Task VerifyingBatchDetails()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Batch details");
    }
}