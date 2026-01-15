namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class YouAccountNameHasBeenChangeToPage(ScenarioContext context, string newAccountName) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync($"Your account name has been changed to {newAccountName}");

    public async Task<CreateYourEmployerAccountPage> ContinueToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}
