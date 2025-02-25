namespace SFA.DAS.Registration.UITests.Project.Pages;

public class AccessDeniedPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Access denied");
    }

    public async Task<HomePage> GoBackToTheServiceHomePage(string orgName)
    {
        objectContext.SetOrganisationName(orgName);

        await page.GetByRole(AriaRole.Link, new() { Name = "Go back to the service home" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}