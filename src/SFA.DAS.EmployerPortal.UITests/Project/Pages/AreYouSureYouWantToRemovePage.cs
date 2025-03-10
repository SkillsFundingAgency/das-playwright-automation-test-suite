namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class AreYouSureYouWantToRemovePage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are you sure you want to remove");
    }

    public async Task<YourOrganisationsAndAgreementsPage> SelectYesRadioOptionAndClickContinueInRemoveOrganisationPage()
    {
        await page.Locator("#remove-yes").CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourOrganisationsAndAgreementsPage(context));
    }
}
