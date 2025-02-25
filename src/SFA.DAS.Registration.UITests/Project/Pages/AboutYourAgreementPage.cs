using SFA.DAS.Registration.UITests.Project.Pages.CreateAccount;
using SFA.DAS.Registration.UITests.Project.Pages.InterimPages;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public class AboutYourAgreementPage(ScenarioContext context) : InterimEmployerBasePage(context, false)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("About your agreement");
    }

    public async Task<SignAgreementPage> ClickContinueToYourAgreementButtonInAboutYourAgreementPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SignAgreementPage(context));
    }

    public async Task<SignAgreementPage> ClickContinueToYourAgreementButtonToDoYouAcceptTheEmployerAgreementPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SignAgreementPage(context));

    }

    public async Task<CreateYourEmployerAccountPage> GoBackToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}
