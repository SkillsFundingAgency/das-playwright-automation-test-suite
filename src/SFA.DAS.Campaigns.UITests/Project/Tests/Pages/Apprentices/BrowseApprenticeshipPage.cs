using Microsoft.Playwright;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class BrowseApprenticeshipPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Browse by interest", new() { IgnoreCase = true });

    public async Task<IPage> ClickExternalFindAnApprenticeshipLink()
    {
        var waitForPageTask = page.Context.WaitForPageAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Find an apprenticeship" }).ClickAsync();

        var externalPage = await waitForPageTask;

        await externalPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        return externalPage;
    }
}