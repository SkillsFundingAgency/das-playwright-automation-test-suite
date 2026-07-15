namespace SFA.DAS.ProviderLogin.Service.Project.Pages;

public abstract class NavigateBase : BasePage
{
    protected NavigateBase(ScenarioContext context, string url) : base(context)
    {
        if (!(string.IsNullOrEmpty(url))) NavigateTo(url);
    }

    private async void NavigateTo(string url)
    {
        objectContext.SetDebugInformation($"Navigated to '{url}' via NavigateBase");

        await Navigate(url);
    }
}

public abstract class Navigate : NavigateBase
{
    protected abstract string Linktext { get; }

    protected Navigate(ScenarioContext context, bool navigate) : this(context, navigate, string.Empty) { }

    protected Navigate(ScenarioContext context, bool navigate, string url) : base(context, url) => NavigateTo(navigate);

    protected Navigate(ScenarioContext context, Action navigate, string url) : base(context, url) => NavigateTo(navigate);

    private static void NavigateTo(Action navigate) => navigate.Invoke();

    private async void NavigateTo(bool navigate)
    {
        if (navigate)
        {
            objectContext.SetDebugInformation($"Clicked menu item - {Linktext}");

            await NavigateToMenuItem(Linktext);
        }
    }

    protected async Task NavigateToMenuItem(string name)
    {
        await page.GetByLabel("Service information").GetByRole(AriaRole.Link, new() { Name = name }).ClickAsync();
    }
}

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
        await Assertions.Expect(ProviderLandingPageIdentifier).ToContainTextAsync(ProviderLandingPageTitle, new LocatorAssertionsToContainTextOptions { Timeout = 15000});
    }

    public async Task ClickStartNow() => await page.GetByRole(AriaRole.Link, new() { Name = "Start now" }).ClickAsync();
}

