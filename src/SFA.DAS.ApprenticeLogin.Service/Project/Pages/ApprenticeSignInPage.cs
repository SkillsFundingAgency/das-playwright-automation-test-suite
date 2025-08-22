namespace SFA.DAS.ApprenticeLogin.Service.Project.Pages;

public class ApprenticeSignInPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(StubSignInApprenticeAccountsPage.StubSignInPageTitle);
    }

    public async Task SubmitUserDetails(ApprenticeUser user)
    {
        var page = await new StubSignInApprenticeAccountsPage(context).SubmitValidUserDetails(user);

        await page.Continue();
    }
}
