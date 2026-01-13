using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class UsingYourGovtGatewayDetailsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Add a PAYE scheme using your Government Gateway details");
    }

    public async Task<GgSignInPage> ContinueToGGSignIn()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return new GgSignInPage(context);
    }
}
