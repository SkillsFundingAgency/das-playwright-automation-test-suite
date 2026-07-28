using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;


namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

public class CampaignsHomePage(ScenarioContext context) : HubBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Level = 1, Name = "Your guide to apprenticeships" })).ToBeVisibleAsync();
    }

    public async Task<CampaignsHomePage> AcceptCookieAndAlert()
    {
        await VerifyPage();
        return this;
    }

    public async Task<ApprenticeHomePage> GoToApprenticePage()
    {
        await NavigateToCard("Become an apprentice");

        var apprenticePage = new ApprenticeHomePage(context);
        await apprenticePage.VerifyPage();
        return apprenticePage;
    }
}