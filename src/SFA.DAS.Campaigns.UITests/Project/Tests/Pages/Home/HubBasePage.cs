using System.Text.RegularExpressions;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

public class HubBasePage(ScenarioContext context) : CampaignsHeaderBasePage(context)
{
    public async Task NavigateToCard(string cardName)
    {
        var locator = page.Locator(".fiu-stepper__link, .fiu-card, .fiu-cta-panel").Filter(new() { HasTextRegex = new Regex(Regex.Escape(cardName)) });

        if (await locator.CountAsync() == 0)
        {
            locator = page.Locator($"a:has-text('{cardName}')");
        }

        var targetElement = locator.First;

        await targetElement.ScrollIntoViewIfNeededAsync();
        await targetElement.ClickAsync();
    }
}