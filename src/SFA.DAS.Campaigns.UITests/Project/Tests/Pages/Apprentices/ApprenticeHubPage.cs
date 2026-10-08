using Microsoft.Playwright;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class ApprenticeHubPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    protected ILocator SetUpService => page.GetByRole(AriaRole.Link, new() { Name = "Getting an apprenticeship", Exact = false });

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Become an apprentice");

    public async Task VerifySubHeadings() => await VerifyLinks();

    public override async Task<IPage> NavigateToApprenticeCard(string cardName)
    {
        ILocator cardLink;

        if (cardName.Equals("Find an apprenticeship", StringComparison.OrdinalIgnoreCase))
        {
            cardLink = page.Locator("a.fiu-cta-panel").Filter(new() { HasText = "Find an apprenticeship" }).First;
        }
        else
        {
            cardLink = page.Locator(".fiu-stepper__link, .fiu-card, .fiu-cta-panel")
                           .Filter(new() { HasText = cardName })
                           .First;
        }

        await Assertions.Expect(cardLink).ToBeVisibleAsync();
        await cardLink.ScrollIntoViewIfNeededAsync();
        await cardLink.ClickAsync();

        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        return page;
    }

    public async Task<BrowseApprenticeshipPage> NavigateToBrowseByTypeOfWork()
    {
        await NavigateToApprenticeCard("Browse by the type of work you’re interested in");
        return await VerifyPageAsync(() => new BrowseApprenticeshipPage(context));
    }
}