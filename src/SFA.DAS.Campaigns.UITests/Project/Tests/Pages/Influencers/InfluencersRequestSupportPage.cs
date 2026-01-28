namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Influencers;

public class InfluencersRequestSupportPage(ScenarioContext context) : InfluencersBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Submit a request");

    public async Task<InfluencersRequestSupportPage> VerifySubHeadings() => await VerifyFiuCards(() => NavigateToRequestSupportPage());
}

