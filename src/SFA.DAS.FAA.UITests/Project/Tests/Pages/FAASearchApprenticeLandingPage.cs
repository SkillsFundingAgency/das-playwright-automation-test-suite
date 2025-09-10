namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAASearchApprenticeLandingPage(ScenarioContext context) : FAASignedInLandingBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync("Find an apprenticeship account created.");

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search apprenticeships");
    }
}
