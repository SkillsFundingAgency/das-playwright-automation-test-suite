namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;

public class AS_LandingPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprenticeship assessment service");

        await AcceptAllCookiesIfVisible();
    }

    public async Task<StubSignInAssessorPage> GoToStubSign()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();

        return await VerifyPageAsync(() => new StubSignInAssessorPage(context));
    }

    public async Task<AS_LoggedInHomePage> AlreadyLoginGoToLoggedInHomePage()
    {
        await CheckAndLogin();

        return await VerifyPageAsync(() => new AS_LoggedInHomePage(context));
    }

    private async Task CheckAndLogin()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();

        if (await new CheckStubSignInAssessorPage(context).IsPageDisplayed())
        {
            var page = await new StubSignInAssessorPage(context).SubmitValidUserDetails(context.Get<EPAOAssessorPortalLoggedInUser>());

            await page.Continue();
        }
    }

}

public class StubSignInAssessorPage(ScenarioContext context) : StubSignInBasePage(context)
{
    internal static string StubSignInAssessorPageTitle => "Stub Authentication - Enter sign in details";

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(StubSignInAssessorPageTitle);
    }

    public async Task<StubYouHaveSignedInAssessorPage> SubmitValidUserDetails(GovSignUser loginUser)
    {
        return await GoToStubYouHaveSignedInAssessorPage(loginUser.Username, loginUser.IdOrUserRef, false);
    }

    public async Task<StubYouHaveSignedInAssessorPage> CreateAccount(string email) => await GoToStubYouHaveSignedInAssessorPage(email, email, true);

    private async Task<StubYouHaveSignedInAssessorPage> GoToStubYouHaveSignedInAssessorPage(string email, string idOrUserRef, bool newUser)
    {
        objectContext.SetDebugInformation($"Entering {idOrUserRef} and {email}");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Gov UK Identifier" }).FillAsync(idOrUserRef);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync(email);

        await ClickSignIn();

        return new StubYouHaveSignedInAssessorPage(context, email, idOrUserRef, newUser);
    }
}

public class CheckStubSignInAssessorPage(ScenarioContext context) : CheckPage(context)
{
    protected override ILocator PageLocator => page.Locator("h1");

    protected override string PageTitle => StubSignInAssessorPage.StubSignInAssessorPageTitle;
}

public class StubYouHaveSignedInAssessorPage(ScenarioContext context, string username, string idOrUserRef, bool newUser) : StubYouHaveSignedInBasePage(context, username, idOrUserRef, newUser)
{

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You've signed in");

        await Assertions.Expect(page.Locator("#estimate-start-transfer")).ToContainTextAsync("Id: 4cfa523e-0c49-4d16-a448-a82ec2860c7d");

        await Assertions.Expect(page.Locator("#estimate-start-transfer")).ToContainTextAsync("Email: epaomailinator+EPA0007@gmail.com");
    }

    public async Task Continue() => await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();
}
