using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public class ThanksForSubscribingPage(ScenarioContext context) : CampaignsVerifyLinks(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Thank you for signing up");
}
