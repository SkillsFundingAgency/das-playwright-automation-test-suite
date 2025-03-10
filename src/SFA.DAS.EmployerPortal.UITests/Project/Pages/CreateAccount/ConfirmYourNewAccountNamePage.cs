namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class ConfirmYourNewAccountNamePage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm your new account name");

    public async Task<YouAccountNameHasBeenChangeToPage> ContinueToAcknowledge(string newAccountName)
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YouAccountNameHasBeenChangeToPage(context, newAccountName));
    }
}
