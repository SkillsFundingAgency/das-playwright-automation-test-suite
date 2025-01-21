using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Influencers;

public class InfluencersHubPage(ScenarioContext context) : InfluencersBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Inspire and Influence");

    public async Task VerifySubHeadings() => await VerifyFiuCards(() => NavigateToInfluencersHubPage());

    public async Task<BrowseApprenticeshipPage> NavigateToBrowseApprenticeshipPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Browse apprenticeships" }).ClickAsync();

        return await VerifyPageAsync(() => new BrowseApprenticeshipPage(context));
    }
}

