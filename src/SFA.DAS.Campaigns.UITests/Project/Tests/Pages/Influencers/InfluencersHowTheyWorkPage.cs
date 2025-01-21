namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Influencers;

public class InfluencersHowTheyWorkPage(ScenarioContext context) : InfluencersBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How they work");

    public async Task<InfluencersHowTheyWorkPage> VerifyInfluencersHowTheyWorkPageSubHeadings() => await VerifyFiuCards(() => NavigateToHowDoTheyWorkPage());
}

