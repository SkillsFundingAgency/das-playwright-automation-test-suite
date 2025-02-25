namespace SFA.DAS.Registration.UITests.Project.Pages;

public class ManageYourApprenticesPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage your apprentices");
    }
}
