using SFA.DAS.AparAdmin.UITests.Project.Helpers;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;


namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;

[Binding]
public class AparAdminLoginSteps(ScenarioContext context) : FrameworkBaseHooks(context)
{
    private AparAssessorApplicationsHomePage _aparApplicationsHomePage;

    private readonly DfeAdminLoginStepsHelper _dfeAdminLoginStepsHelper = new(context);

    private readonly AssessorLoginStepsHelper _assessorLoginStepsHelper = new(context);

    [Given(@"the admin lands on the Dashboard")]
    public async Task GivenTheAdminLandsOnTheDashboard() => await _dfeAdminLoginStepsHelper.NavigateAndLoginToASAdmin();

    [When(@"the (Assessor1|Assessor2) is on the Apar assessor applications dashboard")]
    public async Task WhenTheAssessorIsOnTheRoATPAssessorApplicationsDashboard(string assessorUser)
    {
        await Navigate(UrlConfig.RoATPAssessor_BaseUrl);

        if (assessorUser.Equals("Assessor1"))
        {
            _aparApplicationsHomePage = await _assessorLoginStepsHelper.Assessor1Login();
        }
        else if (assessorUser.Equals("Assessor2"))
        {
            _aparApplicationsHomePage = await _assessorLoginStepsHelper.Assessor2Login();
        }
    }
}