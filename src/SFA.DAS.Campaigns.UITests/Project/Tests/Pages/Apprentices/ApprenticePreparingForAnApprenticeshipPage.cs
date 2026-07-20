namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class ApprenticePreparingForAnApprenticeshipPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Preparing for an apprenticeship");

    public async Task<ApprenticePreparingForAnApprenticeshipPage> VerifyPreparingForAnApprenticeshipPageSubHeadings() =>
        await VerifyFiuCards(() => NavigateToPreparingForAnApprenticeshipPage());
}