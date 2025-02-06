namespace SFA.DAS.Registration.UITests.Project.Pages.StubPages;

public class CheckStubSignInPage(ScenarioContext context) : CheckPage(context)
{
    protected override string PageTitle => StubSignInEmployerPage.PageTitle;

    protected override ILocator PageLocator => new StubSignInEmployerPage(context).PageIdentifier;

    public override async Task VerifyPage() => await new StubSignInEmployerPage(context).VerifyPage();
}

public class StubSignInEmployerPage(ScenarioContext context) : StubSignInBasePage(context)
{
    public static string PageTitle => "Stub Authentication - Enter sign in details";

    public ILocator PageIdentifier => page.Locator("h1");

    public override async Task VerifyPage()
    {
        await Assertions.Expect(PageIdentifier).ToContainTextAsync(PageTitle);
    }

    public async Task<StubYouHaveSignedInEmployerPage> Register(string email = null)
    {
        email = string.IsNullOrEmpty(email) ? objectContext.GetRegisteredEmail() : email;

        await EnterLoginDetailsAndClickSignIn(email, email);

        return await GoToStubYouHaveSignedInPage(email, email, true);
    }

    public async Task<StubYouHaveSignedInEmployerPage> Login(EasAccountUser loginUser) => await Login(loginUser.Username, loginUser.IdOrUserRef);

    public async Task<StubYouHaveSignedInEmployerPage> Login(string Username, string IdOrUserRef)
    {
        await EnterLoginDetailsAndClickSignIn(Username, IdOrUserRef);

        return await GoToStubYouHaveSignedInPage(Username, IdOrUserRef, false);
    }

    private async Task<StubYouHaveSignedInEmployerPage> GoToStubYouHaveSignedInPage(string username, string idOrUserRef, bool newUser) => await VerifyPageAsync(() => new StubYouHaveSignedInEmployerPage(context, username, idOrUserRef, newUser));
}
