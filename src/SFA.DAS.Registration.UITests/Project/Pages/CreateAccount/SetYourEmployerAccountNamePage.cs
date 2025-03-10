namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class SetYourEmployerAccountNamePage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Set your account name");

    public async Task<YourAccountNameHasBeenChangedPage> SelectoptionToSkipNameChange()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, I don't need to change my" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAccountNameHasBeenChangedPage(context));
    }

    public async Task<ConfirmYourNewAccountNamePage> SelectoptionToChangeAccountName(string newAccountName)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I want to change my" }).CheckAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Enter new account name." }).FillAsync(newAccountName);

        objectContext.SetOrganisationName(newAccountName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmYourNewAccountNamePage(context));
    }

    public async Task<CreateYourEmployerAccountPage> GoBackToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}
