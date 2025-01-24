namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Influencers;

public class InfluencersResourceHubPage(ScenarioContext context) : InfluencersBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Resource hub");

    public async Task<InfluencersResourceHubPage> VerifySubHeadings() => await VerifyFiuCards(() => NavigateToResourceHubPage());
}

