using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class YouHaveAcceptedYourEmployerAgreementPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("accepted your employer agreement");

        await Assertions.Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Employer agreement accepted");
    }

    public async Task<CreateYourEmployerAccountPage> SelectContinueToCreateYourEmployerAccount()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}
