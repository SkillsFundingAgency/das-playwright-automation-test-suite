namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class SignInToYourApprenticeshipServiceAccountPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Sign in to your apprenticeship service account");
    }

    public async Task<CreateAnAccountToManageApprenticeshipsPage> GoManageApprenticeLandingPage()
    {
        var url = UrlConfig.EmployerApprenticeshipService_BaseUrl;

        objectContext.SetDebugInformation(url);

        await driver.Page.GotoAsync(url);

        return await VerifyPageAsync(() => new CreateAnAccountToManageApprenticeshipsPage(context));
    }
}
