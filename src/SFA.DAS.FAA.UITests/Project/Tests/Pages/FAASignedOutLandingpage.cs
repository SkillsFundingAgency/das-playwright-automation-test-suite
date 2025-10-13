using SFA.DAS.FAA.UITests.Project.Tests.Pages.StubPages;

namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAASignedOutLandingpage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(FAASignedOutPageIdentifier).ToContainTextAsync("GOV.UK One Login Sign in or create an account");

    public ILocator FAASignedOutPageIdentifier => page.GetByRole(AriaRole.Banner);

    public static string FAASignedOutPageTitle => "Sign in or create an account";

    private static string DeleteConfirmatioBanner => ".govuk-notification-banner__heading";


    public async Task<StubSignInFAAPage> GoToSignInPage()
    {
        var x = page.GetByRole(AriaRole.Button, new() { Name = "Accept additional cookies" });

        if (await x.IsVisibleAsync()) await x.ClickAsync();

        var y = page.GetByRole(AriaRole.Button, new() { Name = "Hide cookie message" });

        if (await y.IsVisibleAsync()) await y.ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Sign in or create an account" }).ClickAsync();

        return await VerifyPageAsync(() => new StubSignInFAAPage(context));
    }

    public async Task VerifyNotification()
    {
        await Assertions.Expect(page.Locator(DeleteConfirmatioBanner)).ToContainTextAsync("Find an apprenticeship account deleted.");
    }
}

public class CheckFAASignedOutLandingPage(ScenarioContext context) : CheckPage(context)
{
    protected override string PageTitle => FAASignedOutLandingpage.FAASignedOutPageTitle;

    protected override ILocator PageLocator => new FAASignedOutLandingpage(context).FAASignedOutPageIdentifier;
}
