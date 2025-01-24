namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public class ThanksForSubscribingPage(ScenarioContext context) : CampaingnsVerifyLinks(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Thank you for signing up");
}
