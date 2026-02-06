namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class YouveLoggedOutPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("You have been signed out");

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "sign in" })).ToBeVisibleAsync();
    }

    public async Task<SignInToYourApprenticeshipServiceAccountPage> CickSignInInYouveLoggedOutPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "sign in" }).ClickAsync();

        return await VerifyPageAsync(() => new SignInToYourApprenticeshipServiceAccountPage(context));
    }
}
