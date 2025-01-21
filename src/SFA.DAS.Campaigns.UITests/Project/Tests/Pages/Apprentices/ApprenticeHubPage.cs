namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class ApprenticeHubPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Become an apprentice");

    protected ILocator SetUpService => page.GetByRole(AriaRole.Link, new() { Name = "Create an account", Exact = true });

    public async Task VerifySubHeadings() => await VerifyFiuCards(() => NavigateToApprenticeshipHubPage());

    public async Task<SetUpServicePage> NavigateToSetUpServiceAccountPage()
    {
        await SetUpService.ClickAsync();

        return await VerifyPageAsync(() => new SetUpServicePage(context));
    }

    public async Task<CampaingnsDynamicFiuPage> NavigateToApprenticeStories()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Apprentice stories" }).ClickAsync();

        return new CampaingnsDynamicFiuPage(context, "Apprentice stories");
    }
}
