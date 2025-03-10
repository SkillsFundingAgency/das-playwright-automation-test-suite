namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class RenameAccountPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Rename account");
    }

    public async Task<HomePage> EnterNewNameAndContinue(string newOrgName)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "New account name" }).FillAsync(newOrgName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        objectContext.SetOrganisationName(newOrgName);

        return await VerifyPageAsync(() => new HomePage(context));
    }
}
