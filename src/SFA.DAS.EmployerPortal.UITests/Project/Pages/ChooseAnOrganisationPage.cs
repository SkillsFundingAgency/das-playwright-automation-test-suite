namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class ChooseAnOrganisationPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose your organisation");
    }

    public async Task<CheckYourDetailsPage> SelectFirstOrganisationAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = objectContext.GetOrganisationName() }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourDetailsPage(context));
    }
}
