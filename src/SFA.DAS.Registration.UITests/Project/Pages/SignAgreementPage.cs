using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class SignAgreementPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"Do you accept the employer agreement");
    }

    public async Task<AccessDeniedPage> ClickYesAndContinueDoYouAcceptTheEmployerAgreementOnBehalfOfPage()
    {
        await page.GetByText("Yes, I accept the agreement").ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AccessDeniedPage(context));
    }

    public async Task<YouHaveAcceptedYourEmployerAgreementPage> SignAgreementFromCreateAccountTasks()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I accept the agreement" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YouHaveAcceptedYourEmployerAgreementPage(context));
    }

    public async Task<YouHaveAcceptedTheEmployerAgreementPage> SignAgreementFromHomePage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I accept the agreement" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YouHaveAcceptedTheEmployerAgreementPage(context));
    }

    public async Task<CreateYourEmployerAccountPage> DoNotSignAgreement()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Not yet" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}
