using SFA.DAS.EmployerPortal.UITests.Project.Pages.StubPages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class YouveLoggedOutPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have been signed out");

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "sign in" }).First).ToBeVisibleAsync();
    }

    public async Task<StubSignInEmployerPage> CickSignInInYouveLoggedOutPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "sign in" }).First.ClickAsync();

        return await VerifyPageAsync(() => new StubSignInEmployerPage(context));
    }
}
