namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class ApprenticeHomePage(ScenarioContext context) : CampaingnsBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(driver.Page.Locator("h1")).ToContainTextAsync("Become an apprentice");
}
