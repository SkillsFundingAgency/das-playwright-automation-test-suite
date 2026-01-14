namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class AccessDeniedPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Access denied");
    }

    public async Task<HomePage> GoBackToTheServiceHomePage(string orgName)
    {
        objectContext.SetOrganisationName(orgName);

        await page.GetByRole(AriaRole.Link, new() { Name = "Go back to the service home" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}