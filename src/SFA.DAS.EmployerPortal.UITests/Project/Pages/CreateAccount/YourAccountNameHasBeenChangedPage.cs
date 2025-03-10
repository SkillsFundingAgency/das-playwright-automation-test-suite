namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class YourAccountNameHasBeenChangedPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Account name confirmed");

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have confirmed your Account name");
    }

    public async Task<CreateYourEmployerAccountPage> ContinueToAcknowledge()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}
