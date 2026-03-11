namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

public class CampaingnsDynamicFiuPage(ScenarioContext context, string pageTitle) : CampaingnsHeaderBasePage(context)
{
    public readonly string PageTitle = pageTitle;

    public override async Task VerifyPage() => await Task.CompletedTask;

    public async Task VerifyPageAsync()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);

        await VerifyLinks();

        await VerifyVideoLinks();
    }
}
