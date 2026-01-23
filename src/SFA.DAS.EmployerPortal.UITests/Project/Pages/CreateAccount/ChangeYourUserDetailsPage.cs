namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class ChangeYourUserDetailsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Change your user details");

    public async Task<ConfirmYourUserDetailsPage> EnterName()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "First name" }).FillAsync(employerPortalDataHelper.FirstName);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Last name" }).FillAsync(" " + employerPortalDataHelper.LastName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmYourUserDetailsPage(context));
    }
}