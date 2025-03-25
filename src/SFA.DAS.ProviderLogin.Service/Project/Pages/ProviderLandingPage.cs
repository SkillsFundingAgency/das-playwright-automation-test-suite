namespace SFA.DAS.ProviderLogin.Service.Project.Pages;

public abstract class InterimProviderBasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly string ukprn = context.Get<ObjectContext>().GetUkprn();
    #endregion

    public async Task<ProviderHomePage> GoToProviderHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderHomePage(context));
    }

    public async Task NavigateBrowserBack()
    {
        await page.GoBackAsync();
    }
}

public class ProviderLandingPage(ScenarioContext context) : BasePage(context)
{
    public static string ProviderLandingPageTitle => "Apprenticeship service for training providers: sign in or register for an account";

    public ILocator ProviderLandingPageIdentifier => page.Locator(".govuk-heading-xl");

    public override async Task VerifyPage()
    {
        await Assertions.Expect(ProviderLandingPageIdentifier).ToContainTextAsync(ProviderLandingPageTitle);
    }

    public async Task ClickStartNow() => await page.GetByRole(AriaRole.Link, new() { Name = "Start now" }).ClickAsync();
}

