namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class AccessDeniedPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Access denied");
    }

    public async Task VerifyHomeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();
    }

    public async Task<HomePage> GoBackToTheServiceHomePage(string orgName)
    {
        objectContext.SetOrganisationName(orgName);

        await page.GetByRole(AriaRole.Link, new() { Name = "Go back to the service home" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}