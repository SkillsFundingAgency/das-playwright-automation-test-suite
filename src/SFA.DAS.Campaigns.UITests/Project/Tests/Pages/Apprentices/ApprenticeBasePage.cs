namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public abstract class ApprenticeBasePage(ScenarioContext context) : HubBasePage(context)
{
    private ILocator ApprenticeTab => page.GetByLabel("Apprentices");

    public async Task<ApprenticeIsAnApprenticeshipRightForYouPage> NavigateToIsAnApprenticeshipRightForYouPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Is an apprenticeship right for you?" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeIsAnApprenticeshipRightForYouPage(context));
    }

    public async Task<ApprenticeAboutApprenticeshipsPage> NavigateToAboutApprenticeshipsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "About apprenticeships" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeAboutApprenticeshipsPage(context));
    }

    public async Task<ApprenticeCreateAccountPage> NavigateToCreateAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Create an account to search and apply for apprenticeships", Exact = false }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeCreateAccountPage(context));
    }

    public async Task<ApprenticePreparingForAnApprenticeshipPage> NavigateToPreparingForAnApprenticeshipPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Preparing for an apprenticeship", Exact = false }).First.ClickAsync();

        return await VerifyPageAsync(() => new ApprenticePreparingForAnApprenticeshipPage(context));
    }
    public async Task<BrowseApprenticeshipPage> NavigateToBrowseApprenticeshipPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Browse by interest", Exact = false }).ClickAsync();

        return await VerifyPageAsync(() => new BrowseApprenticeshipPage(context));
    }
}
