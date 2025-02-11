using SFA.DAS.Registration.UITests.Project.Pages.StubPages;
using System;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public class CreateAnAccountToManageApprenticeshipsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public static string PageTitle => "Create an account to manage apprenticeships";

    public ILocator PageIdentifier => page.Locator("h1");

    public override async Task VerifyPage()
    {
        await Assertions.Expect(PageIdentifier).ToContainTextAsync(PageTitle);

        if (await page.GetByRole(AriaRole.Button, new() { Name = "Accept all cookies" }).IsVisibleAsync())
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Accept all cookies" }).ClickAsync();
        }
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

public class AccountUnavailablePage(ScenarioContext context) : RegistrationBasePage(context)
{
    public static string PageTitle => "This account is unavailable";

    public ILocator PageIdentifier => page.Locator("#main-content");

    public override async Task VerifyPage() => await Assertions.Expect(PageIdentifier).ToContainTextAsync(PageTitle);
}
