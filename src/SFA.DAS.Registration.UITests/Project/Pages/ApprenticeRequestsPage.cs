namespace SFA.DAS.Registration.UITests.Project.Pages;

public class ApprenticeRequestsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentice requests");
    }
}
