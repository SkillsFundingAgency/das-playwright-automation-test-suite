using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

public class HubBasePage(ScenarioContext context) : CampaignsHeaderBasePage(context)
{
    public async Task NavigateToCard(string cardName)
    {
        var locator = page.Locator(".fiu-stepper__link, .fiu-card, .fiu-cta-panel, a")
                          .Filter(new() { HasTextRegex = new Regex(Regex.Escape(cardName), RegexOptions.IgnoreCase) });

        if (await locator.CountAsync() == 0)
        {
            locator = page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex(Regex.Escape(cardName), RegexOptions.IgnoreCase) });
        }

        var targetElement = locator.First;

        await Assertions.Expect(targetElement).ToBeVisibleAsync();
        await targetElement.ScrollIntoViewIfNeededAsync();
        await targetElement.ClickAsync();

        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }
}