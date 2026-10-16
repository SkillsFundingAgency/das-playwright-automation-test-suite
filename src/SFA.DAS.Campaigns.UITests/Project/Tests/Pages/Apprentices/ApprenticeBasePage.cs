using Microsoft.Playwright;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public abstract class ApprenticeBasePage(ScenarioContext context) : HubBasePage(context)
{
    public virtual async Task<IPage> NavigateToApprenticeCard(string cardName)
    {
        var cardLink = page.Locator(".fiu-stepper__link, .fiu-card, .fiu-cta-panel")
                           .Filter(new() { HasText = cardName })
                           .First;

        if (await cardLink.CountAsync() == 0)
        {
            cardLink = page.GetByRole(AriaRole.Link, new() { Name = cardName, Exact = true }).First;
        }

        await Assertions.Expect(cardLink).ToBeVisibleAsync();
        await cardLink.ScrollIntoViewIfNeededAsync();
        await cardLink.ClickAsync();

        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        return page;
    }
}