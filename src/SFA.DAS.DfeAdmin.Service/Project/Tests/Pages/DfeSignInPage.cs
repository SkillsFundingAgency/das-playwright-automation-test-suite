using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;

namespace SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;

public class DfeSignInPage(ScenarioContext context) : SignInBasePage(context)
{
    public static string DfePageTitle => "Access the DfE Sign-in service";

    public ILocator DfePageIdentifier => page.Locator("h1");

    public override async Task VerifyPage() => await Assertions.Expect(DfePageIdentifier).ToContainTextAsync(DfePageTitle);

    public async Task SubmitValidLoginDetails(DfeAdminUser dfeAdminUser)
    {
        await VerifyPage();

        await EnterValidLoginDetails(dfeAdminUser.Username, dfeAdminUser.Password);

        try
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync(new() { Timeout = 300000 });
        }
        catch (TimeoutException ex)
        {
            //do nothing 
            objectContext.SetDebugInformation($"{DfePageTitle} resulted in {ex.Message}");
        }
    }
}
