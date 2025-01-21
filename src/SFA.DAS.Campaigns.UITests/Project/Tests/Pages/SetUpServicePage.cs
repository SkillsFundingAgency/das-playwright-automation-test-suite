namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

public class SetUpServicePage(ScenarioContext context) : CampaingnsHeaderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create an account to search and apply for apprenticeships");
    }
}
