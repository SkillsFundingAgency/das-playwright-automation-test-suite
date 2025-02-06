namespace SFA.DAS.Registration.UITests.Project.Pages.StubPages;

public class StubYouHaveSignedInEmployerPage(ScenarioContext context, string username, string idOrUserRef, bool newUser) : StubYouHaveSignedInBasePage(context, username, idOrUserRef, newUser)
{
    public override async Task VerifyPage()
    {
        await base.VerifyPage();

        if (newUser)
        {
            idOrUserRef = await new UsersSqlDataHelper(objectContext, context.Get<DbConfig>()).GetUserId(username);

            objectContext.UpdateLoginIdOrUserRef(username, idOrUserRef);
        }
    }

    //public async Task<MyAccountTransferFundingPage> ContinueToMyAccountTransferFundingPage()
    //{
    //    await Continue();
    //    return new MyAccountTransferFundingPage(context);
    //}

    public async Task<YourAccountsPage> ContinueToYourAccountsPage()
    {
        await Continue();

        return await VerifyPageAsync(() => new YourAccountsPage(context));
    }

    public async Task<HomePage> ContinueToHomePage()
    {
        await Continue();

        return new HomePage(context);
    }

    public async Task<AccountUnavailablePage> GoToAccountUnavailablePage()
    {
        await Continue();

        return await VerifyPageAsync(() => new AccountUnavailablePage(context));
    }

    //public async Task<StubAddYourUserDetailsPage> ContinueToStubAddYourUserDetailsPage()
    //{
    //    await Continue();
    //    return new StubAddYourUserDetailsPage(context);
    //}

    public async Task Continue() => await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

}
