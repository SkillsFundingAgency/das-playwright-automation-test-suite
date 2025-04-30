namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class ToolSupportHomePage(ScenarioContext context) : ToolSupportBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("DAS Tools Support", new() { Timeout = LandingPageTimeout });

    private static string PageTitle => "DAS Tools Support";

    private ILocator PageIdentifier => page.GetByRole(AriaRole.Heading);

    public async Task<bool> IsPageDisplayed()
    {
        if (await PageIdentifier.IsVisibleAsync())
        {
            var text = await PageIdentifier.TextContentAsync();

            return text.ContainsCompareCaseInsensitive(PageTitle);
        }

        return false;
    }

    public async Task<SearchHomePage> ClickEmployerSupportToolLink()
    {
        await page.Locator("#employerSupport").ClickAsync();

        return await VerifyPageAsync(() => new SearchHomePage(context));
    }

    public async Task<SearchForApprenticeshipPage> ClickPauseApprenticeshipsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Pause apprenticeship" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchForApprenticeshipPage(context));
    }

    public async Task<SearchForApprenticeshipPage> ClickResumeApprenticeshipsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Resume apprenticeship" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchForApprenticeshipPage(context));
    }

    public async Task<SearchForApprenticeshipPage> ClickStopApprenticeshipsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Stop apprenticeship" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchForApprenticeshipPage(context));
    }

    public async Task<SearchForAnEmployerPage> ClickSuspendUserAccountsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Suspend user account" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchForAnEmployerPage(context));
    }

    public async Task<SearchForAnEmployerPage> ClickReinstateUserAccountsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Reinstate user account" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchForAnEmployerPage(context));
    }

    public async Task<bool> IsPauseApprenticeshipLinkVisible() => await page.GetByRole(AriaRole.Link, new() { Name = "Pause apprenticeship" }).IsVisibleAsync();

    public async Task<bool> IsResumeApprenticeshipLinkVisible() => await page.GetByRole(AriaRole.Link, new() { Name = "Resume apprenticeship" }).IsVisibleAsync();

    public async Task<bool> IsStopApprenticeshipLinkVisible() => await page.GetByRole(AriaRole.Link, new() { Name = "Stop apprenticeship" }).IsVisibleAsync();

    public async Task<bool> IsReinstateApprenticeshipLinkVisible() => await page.GetByRole(AriaRole.Link, new() { Name = "Suspend user account" }).IsVisibleAsync();

    public async Task<bool> IsSuspendApprenticeshipLinkVisible() => await page.GetByRole(AriaRole.Link, new() { Name = "Reinstate user account" }).IsVisibleAsync();
}
