namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

public class SiteMapPage(ScenarioContext context) : CampaingnsHeaderBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Sitemap");
}
