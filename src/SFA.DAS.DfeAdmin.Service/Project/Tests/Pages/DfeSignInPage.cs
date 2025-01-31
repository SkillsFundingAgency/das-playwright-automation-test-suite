using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;

namespace SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;

public class DfeSignInPage(ScenarioContext context) : SignInBasePage(context)
{
    public static string DfePageTitle => "Access the DfE Sign-in service";

    public ILocator DfePageIdentifier => page.Locator("h1");

    private ILocator DfeSignInButton => page.GetByRole(AriaRole.Button, new() { Name = "Sign in" });

    public override async Task VerifyPage() => await Assertions.Expect(DfePageIdentifier).ToContainTextAsync(DfePageTitle);

    public async Task SubmitValidLoginDetails(DfeAdminUser dfeAdminUser)
    {
        await VerifyPage();

        await EnterValidLoginDetails(dfeAdminUser.Username, dfeAdminUser.Password);

        await DfeSignInButton.ClickAsync();

        await Assertions.Expect(DfeSignInButton).ToBeHiddenAsync(new() { Timeout = LandingPageTimeout});
    }
}
