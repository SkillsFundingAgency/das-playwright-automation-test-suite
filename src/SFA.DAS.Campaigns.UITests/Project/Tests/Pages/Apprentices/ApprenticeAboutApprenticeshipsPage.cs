namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class ApprenticeAboutApprenticeshipsPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("About apprenticeships");

    public async Task<ApprenticeAboutApprenticeshipsPage> VerifyAboutApprenticeshipsPageSubHeadings() =>
        await VerifyFiuCards(() => NavigateToAboutApprenticeshipsPage());
}