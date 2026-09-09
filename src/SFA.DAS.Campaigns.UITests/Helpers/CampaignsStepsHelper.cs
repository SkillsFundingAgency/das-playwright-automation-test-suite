using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

namespace SFA.DAS.Campaigns.UITests.Helpers;

public class CampaignsStepsHelper(ScenarioContext context)
{
    public async Task<CampaignsHomePage> GoToCampaignsHomePage() =>
        await new CampaignsHomePage(context).AcceptCookieAndAlert();

    public Task<ApprenticeHubPage> GoToApprenticeshipHubPage() =>
        new CampaignsHomePage(context).NavigateToApprenticeshipHubPage();

    public Task<EmployerHubPage> GoToEmployerHubPage() =>
        new CampaignsHomePage(context).NavigateToEmployerHubPage();
}