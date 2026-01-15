using SFA.DAS.EmployerPortal.UITests.Project.Pages.StubPages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class CreateAnAccountToManageApprenticeshipsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public static string PageTitle => "Create an account to manage apprenticeships";

    public ILocator PageIdentifier => page.Locator("h1");

    public override async Task VerifyPage()
    {
        await Assertions.Expect(PageIdentifier).ToContainTextAsync(PageTitle);

        await AcceptAllCookiesIfVisible();
    }

    public async Task<StubSignInEmployerPage> GoToStubSignInPage() => await StubSignInPage(async () => await page.GetByRole(AriaRole.Link, new() { Name = "sign in" }).ClickAsync());

    public async Task<StubSignInEmployerPage> ClickOnCreateAccountLink() => await StubSignInPage(async () => await page.GetByRole(AriaRole.Button, new() { Name = "Create account" }).ClickAsync());

    private async Task<StubSignInEmployerPage> StubSignInPage(Func<Task> action)
    {
        await action();

        return await VerifyPageAsync(() => new StubSignInEmployerPage(context));
    }
}

public class CheckIndexPage(ScenarioContext context) : CheckPage(context)
{
    protected override string PageTitle => CreateAnAccountToManageApprenticeshipsPage.PageTitle;

    protected override ILocator PageLocator => new CreateAnAccountToManageApprenticeshipsPage(context).PageIdentifier;
}

public class AccountUnavailablePage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public static string PageTitle => "This account is unavailable";

    public ILocator PageIdentifier => page.Locator("#main-content");

    public override async Task VerifyPage() => await Assertions.Expect(PageIdentifier).ToContainTextAsync(PageTitle);
}
