namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class YouveLoggedOutPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("You have been signed out");

        await Assertions.Expect(page.Locator("//a[.='sign in']")).ToBeVisibleAsync();
    }

    public async Task CickSignInInYouveLoggedOutPage()
    {
        await page.Locator("//a[.='sign in']").ClickAsync();
    }
}
