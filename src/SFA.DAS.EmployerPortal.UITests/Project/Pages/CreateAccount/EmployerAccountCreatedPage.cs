namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class EmployerAccountCreatedPage(ScenarioContext context) : EmpAccountCreationBase(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Employer account created");

        await SetEasNewUser();
    }

    public async Task<HomePage> SelectGoToYourEmployerAccountHomepage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Go to your employer account" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}
