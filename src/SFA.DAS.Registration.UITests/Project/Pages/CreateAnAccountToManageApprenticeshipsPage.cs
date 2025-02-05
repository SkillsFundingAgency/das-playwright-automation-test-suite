using SFA.DAS.Registration.UITests.Project.Tests.Pages.StubPages;
using System;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public class CreateAnAccountToManageApprenticeshipsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create an account to manage apprenticeships");

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

        return new StubSignInEmployerPage(context);
    }
}
