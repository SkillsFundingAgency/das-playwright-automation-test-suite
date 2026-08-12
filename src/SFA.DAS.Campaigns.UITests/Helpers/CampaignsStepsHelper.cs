using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

namespace SFA.DAS.Campaigns.UITests.Helpers;

public class CampaignsStepsHelper(ScenarioContext context)
{
    public async Task<CampaignsHomePage> GoToCampaingnsHomePage() =>
        await new CampaignsHomePage(context).AcceptCookieAndAlert();

    public async Task<ApprenticeHubPage> GoToApprenticeshipHubPage()
    {
        var homePage = new CampaignsHomePage(context);

        return await homePage.NavigateToApprenticeshipHubPage();
    }

    public async Task<EmployerHubPage> GoToEmployerHubPage()
    {
        var homePage = new CampaignsHomePage(context);

        return await homePage.NavigateToEmployerHubPage();
    }
}