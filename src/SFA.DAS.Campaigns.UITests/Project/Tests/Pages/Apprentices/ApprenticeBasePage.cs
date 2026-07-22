using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public abstract class ApprenticeBasePage(ScenarioContext context) : HubBasePage(context)
{
    private ILocator ApprenticeTab => page.GetByLabel("Apprentices");

    public async Task<IPage> NavigateToApprenticeCard(string cardName)
    {
        var cardLink = page.GetByRole(AriaRole.Link, new() { Name = cardName, Exact = false }).First;

        await cardLink.ClickAsync();
        return page;
    }
}