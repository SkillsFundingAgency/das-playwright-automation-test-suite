namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class BrowseApprenticeshipPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Browse by interests", new() { IgnoreCase = true });

    public async Task NavigateToSectorCard(string sectorName) =>
        await page.GetByRole(AriaRole.Link, new() { Name = sectorName, Exact = false }).First.ClickAsync();

    public async Task VerifyHeading(string sectorName) =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(sectorName, new() { IgnoreCase = true });

    public async Task<IPage> ClickExternalFindAnApprenticeshipLink()
    {
        var waitForPageTask = page.Context.WaitForPageAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Find an apprenticeship" }).ClickAsync();
        var externalPage = await waitForPageTask;
        await externalPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        return externalPage;
    }
}