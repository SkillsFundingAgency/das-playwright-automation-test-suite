namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class ApprenticeAreTheyRightForYouPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are they right for you?");

    public async Task<ApprenticeAreTheyRightForYouPage> VerifyApprenticeAreTheyRightForYouPageSubHeadings() => await VerifyFiuCards(() => NavigateToAreApprenticeShipRightForMe());
}
