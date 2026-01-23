using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;


public class InterimYourOrganisationsAndAgreementsPage(ScenarioContext context, bool navigate) : InterimEmployerBasePage(context, navigate)
{
    protected override string Linktext => "Your organisations and agreements";

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("About your agreement");
    }
}

public class AboutYourAgreementPage(ScenarioContext context) : InterimYourOrganisationsAndAgreementsPage(context, false)
{

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
