namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class ConfirmPAYESchemePage(ScenarioContext context, string paye) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Confirm PAYE scheme");

    public async Task<PAYESchemeAddedPage> ClickContinueInConfirmPAYESchemePage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Yes, continue" }).ClickAsync();

        return await VerifyPageAsync(() => new PAYESchemeAddedPage(context, paye));
    }
}
