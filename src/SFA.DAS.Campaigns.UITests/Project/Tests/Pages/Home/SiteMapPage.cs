namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

public class SiteMapPage(ScenarioContext context) : CampaignsHeaderBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Sitemap");
}
