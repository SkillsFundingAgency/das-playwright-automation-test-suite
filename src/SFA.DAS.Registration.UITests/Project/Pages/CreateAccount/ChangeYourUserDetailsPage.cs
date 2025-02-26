namespace SFA.DAS.Registration.UITests.Project.Pages.CreateAccount;

public class ChangeYourUserDetailsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change your user details");

    public async Task<ConfirmYourUserDetailsPage> EnterName()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "First name" }).FillAsync(registrationDataHelper.FirstName);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Last name" }).FillAsync(" " + registrationDataHelper.LastName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmYourUserDetailsPage(context));
    }
}