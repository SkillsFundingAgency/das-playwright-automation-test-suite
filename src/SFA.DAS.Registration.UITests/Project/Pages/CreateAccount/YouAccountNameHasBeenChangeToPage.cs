namespace SFA.DAS.Registration.UITests.Project.Pages.CreateAccount;

public class YouAccountNameHasBeenChangeToPage(ScenarioContext context, string newAccountName) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync($"Your account name has been changed to {newAccountName}");

    public async Task<CreateYourEmployerAccountPage> ContinueToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}
