using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;

namespace SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;

public class DfeSignInPage(ScenarioContext context) : SignInBasePage(context)
{
    public static string DfePageTitle => "Access the DfE Sign-in service";

    public ILocator DfePageIdentifier => page.Locator("h1");

    public static string DfePageIdentifierCss => ".govuk-heading-xl";

    public override async Task VerifyPage() => await Assertions.Expect(DfePageIdentifier).ToContainTextAsync(DfePageTitle);

    public async Task SubmitValidLoginDetails(DfeAdminUser dfeAdminUser)
    {
        await SubmitValidLoginDetails(dfeAdminUser.Username, dfeAdminUser.Password);
    }

    protected async Task SubmitValidLoginDetails(string username, string password)
    {
        await VerifyPage();

        await EnterValidLoginDetails(username, password);

        try
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync(new() { Timeout = 300000 });
        }
        catch (TimeoutException ex)
        {
            //do nothing 
            objectContext.SetDebugInformation($"{DfePageTitle} resulted in {ex.Message}");
        }

        await Assertions.Expect(DfePageIdentifier).Not.ToContainTextAsync(DfePageTitle);
    }
}
