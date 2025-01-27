using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;

namespace SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;

public class DfeSignInPage(ScenarioContext context) : SignInBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Access the DfE Sign-in service");

    public async Task SubmitValidLoginDetails(DfeAdminUser dfeAdminUser)
    {
        await VerifyPage();

        await EnterValidLoginDetails(dfeAdminUser.Username, dfeAdminUser.Password);

        await page.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync();
    }
}
