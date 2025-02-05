using SFA.DAS.Registration.UITests.Project.Pages.StubPages;

namespace SFA.DAS.Registration.UITests.Project.Tests.Pages.StubPages;

public class StubSignInEmployerPage(ScenarioContext context) : StubSignInBasePage(context)
{
    protected override string PageTitle => "Stub Authentication - Enter sign in details";

    public async Task<StubYouHaveSignedInEmployerPage> Register(string email = null)
    {
        email = string.IsNullOrEmpty(email) ? objectContext.GetRegisteredEmail() : email;

        await EnterLoginDetailsAndClickSignIn(email, email);

        return GoToStubYouHaveSignedInPage(email, email, true);
    }

    public async Task<StubYouHaveSignedInEmployerPage> Login(EasAccountUser loginUser) => Login(loginUser.Username, loginUser.IdOrUserRef);

    public StubYouHaveSignedInEmployerPage Login(string Username, string IdOrUserRef)
    {
        await EnterLoginDetailsAndClickSignIn(Username, IdOrUserRef);

        return GoToStubYouHaveSignedInPage(Username, IdOrUserRef, false);
    }

    private StubYouHaveSignedInEmployerPage GoToStubYouHaveSignedInPage(string username, string idOrUserRef, bool newUser) => new(context, username, idOrUserRef, newUser);
}
