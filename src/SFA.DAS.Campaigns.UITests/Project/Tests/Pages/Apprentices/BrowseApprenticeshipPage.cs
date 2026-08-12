namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class BrowseApprenticeshipPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    private IPage Page => context.Get<IPage>();

    public override async Task VerifyPage() => await Assertions.Expect(Page.Locator("h1")).ToContainTextAsync("Browse by interest", new() { IgnoreCase = true });

    public async Task<IPage> ClickExternalFindAnApprenticeshipLink()
    {
        var waitForPageTask = Page.Context.WaitForPageAsync();

        await Page.GetByRole(AriaRole.Link, new() { Name = "Find an apprenticeship" }).ClickAsync();

        var externalPage = await waitForPageTask;

        await externalPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        return externalPage;
    }
}