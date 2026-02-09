namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class SignInToYourApprenticeshipServiceAccountPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("govuk-heading-xl")).ToContainTextAsync("Sign in to your apprenticeship service account");
    }

    public async Task<CreateAnAccountToManageApprenticeshipsPage> GoManageApprenticeLandingPage()
    {
        await Navigate(UrlConfig.EmployerApprenticeshipService_BaseUrl);

        return await VerifyPageAsync(() => new CreateAnAccountToManageApprenticeshipsPage(context));
    }
}
