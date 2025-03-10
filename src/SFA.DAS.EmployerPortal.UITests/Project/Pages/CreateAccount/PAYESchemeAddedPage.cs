namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class PAYESchemeAddedPage(ScenarioContext context, string paye) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"{paye} has been added");

    public async Task<UsingYourGovtGatewayDetailsPage> SelectAddAnotherPAYEScheme()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Add another PAYE scheme" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new UsingYourGovtGatewayDetailsPage(context));
    }

    public async Task<HomePage> SelectContinueAccountSetupInPAYESchemeAddedPage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Return to homepage" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}
