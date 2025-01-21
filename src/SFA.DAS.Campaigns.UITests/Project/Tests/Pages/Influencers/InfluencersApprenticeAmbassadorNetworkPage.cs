namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Influencers;

public class InfluencersApprenticeAmbassadorNetworkPage(ScenarioContext context) : InfluencersBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprenticeship ambassador network");

    public async Task<InfluencersApprenticeAmbassadorNetworkPage> VerifySubHeadings() => await VerifyFiuCards(() => NavigateToApprenticeAmbassadorNetworkPage());
}

