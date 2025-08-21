using System;

namespace SFA.DAS.ApprenticeApp.Service.Project.Pages.StubPages;

public class StubSignInApprenticeAccountsPage(ScenarioContext context) : StubSignInBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(StubSignInPageTitle);
    }

    public static string StubSignInPageTitle => "Stub Authentication - Enter sign in details";

    public async Task<StubYouHaveSignedInApprenticeAccountsPage> SubmitValidUserDetails(ApprenticeUser user) => await SubmitValidUserDetails(user.Username, user.IdOrUserRef);

    public async Task<StubYouHaveSignedInApprenticeAccountsPage> SubmitValidUserDetails(string email, string idOrUserRef)
    {
        return await GoToStubYouHaveSignedInApprenticeAccountsPage(email, idOrUserRef, false);
    }

    public async Task<StubYouHaveSignedInApprenticeAccountsPage> CreateAccount(string email) => await GoToStubYouHaveSignedInApprenticeAccountsPage(email, $"{Guid.NewGuid()}", true);

    private async Task<StubYouHaveSignedInApprenticeAccountsPage> GoToStubYouHaveSignedInApprenticeAccountsPage(string email, string idOrUserRef, bool newUser)
    {
        await EnterLoginDetailsAndClickSignIn(email, idOrUserRef);

        return await VerifyPageAsync(() => new StubYouHaveSignedInApprenticeAccountsPage(context, email, idOrUserRef, newUser));
    }
}
