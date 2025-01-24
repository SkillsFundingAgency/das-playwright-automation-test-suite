namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public abstract class ApprenticeBasePage(ScenarioContext context) : HubBasePage(context)
{
    private ILocator ApprenticeTab => page.GetByLabel("Apprentices");

    public async Task<ApprenticeAreTheyRightForYouPage> NavigateToAreApprenticeShipRightForMe()
    {
        await ApprenticeTab.GetByRole(AriaRole.Link, new() { Name = "Are they right for you?" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeAreTheyRightForYouPage(context));
    }

    public async Task<ApprenticeHowDoTheyWorkPage> NavigateToHowDoTheyWorkPage()
    {
        await ApprenticeTab.GetByRole(AriaRole.Link, new() { Name = "How do they work?" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeHowDoTheyWorkPage(context));
    }

    public async Task<ApprenticeGetStartedPage> NavigateToGettingStarted()
    {
        await ApprenticeTab.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeGetStartedPage(context));
    }

    public async Task<BrowseApprenticeshipPage> NavigateToBrowseApprenticeshipPage()
    {
        await ApprenticeTab.GetByRole(AriaRole.Link, new() { Name = "Browse apprenticeships" }).ClickAsync();

        return await VerifyPageAsync(() => new BrowseApprenticeshipPage(context));
    }
}
