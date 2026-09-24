using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public abstract class EmployerBasePage(ScenarioContext context) : HubBasePage(context)
{
    public async Task<IPage> NavigateToEmployerCard(string cardName)
    {
        var cardLink = page.GetByRole(AriaRole.Link, new() { Name = cardName, Exact = false }).First;

        await cardLink.ClickAsync();
        return page;
    }
}