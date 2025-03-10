namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class DetailsUpdatedPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Details updated");
    }

    public async Task<HomePage> SelectGoToHomePageOptionAndContinueInDetailsUpdatedPage()
    {
        await page.GetByRole(AriaRole.Radio).Nth(1).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));

    }
}
