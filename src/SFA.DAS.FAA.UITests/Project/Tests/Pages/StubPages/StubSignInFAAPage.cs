namespace SFA.DAS.FAA.UITests.Project.Tests.Pages.StubPages;

public class StubSignInFAAPage(ScenarioContext context) : StubSignInBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Stub Authentication - Enter sign in details");

    internal static string StubSignInFAAPageTitle => "Stub Authentication - Enter sign in details";

    public async Task<StubYouHaveSignedInFAAPage> SubmitValidUserDetails(FAAPortalUser loginUser)
    {
        return await GoToStubYouHaveSignedInFAAPage(loginUser.Username, loginUser.IdOrUserRef, loginUser.MobilePhone, false);
    }

    public async Task<StubYouHaveSignedInFAAPage> SubmitValidUserDetails(FAAPortalSecondUser loginUser)
    {
        return await GoToStubYouHaveSignedInFAAPage(loginUser.Username, loginUser.IdOrUserRef, loginUser.MobilePhone, false);
    }

    public async Task<StubYouHaveSignedInFAAPage> SubmitNewUserDetails(FAAPortalUser loginUser) => await GoToStubYouHaveSignedInFAAPage(loginUser.Username, loginUser.IdOrUserRef, loginUser.MobilePhone, true);

    private async Task<StubYouHaveSignedInFAAPage> GoToStubYouHaveSignedInFAAPage(string email, string idOrUserRef, string mobilePhone, bool newUser)
    {
        await EnterLoginDetails(email, idOrUserRef);

        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "Mobile phone" }).FillAsync(mobilePhone);

        await ClickSignIn();

        return await VerifyPageAsync(() => new StubYouHaveSignedInFAAPage(context, email, idOrUserRef, newUser));
    }
}
