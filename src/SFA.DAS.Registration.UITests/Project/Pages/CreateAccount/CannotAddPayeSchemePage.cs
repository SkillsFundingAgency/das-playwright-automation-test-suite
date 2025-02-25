namespace SFA.DAS.Registration.UITests.Project.Pages.CreateAccount;

public class CannotAddPayeSchemePage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add PAYE Scheme");

    public async Task<CreateYourEmployerAccountPage> GoBackToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Close" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}
