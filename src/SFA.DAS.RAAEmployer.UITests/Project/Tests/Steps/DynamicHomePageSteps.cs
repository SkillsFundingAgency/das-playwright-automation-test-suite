using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.RAA.Service.Project.Helpers;
using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Steps;

[Binding]
public class DynamicHomePageSteps(ScenarioContext context)
{
    private RecruitmentDynamicHomePage _dynamicHomePage;
    private readonly SearchVacancyPageHelper _searchVacancyPageHelper = new(context);

    [Given(@"the Employer logs into Employer account")]
    public async Task GivenTheEmployerLogsIntoEmployerAccount() => await new EmployerHomePageStepsHelper(context).GotoEmployerHomePage();

    [Given(@"the employer reserves funding from the dynamic home page")]
    public async Task GivenTheUserReservesFundingFromTheDynamicHomePage() => await new EmployerStepsHelper(context).ReserveFundsFromDynamicHomepage();

    [Then(@"^the vacancy details is displayed on the Dynamic home page with Status '(Draft|Rejected|Live|Pending review|Closed)'$")]
    public async Task GivenTheVacancyDetailsIsDisplayedOnTheDynamicHomePageWithStatus(string status)
    {
        await new SearchVacancyPageHelper(context).NavigateToMenuItem("Home");

        switch (status)
        {
            case RecruitmentDynamicHomePage.DraftStatus:
            case RecruitmentDynamicHomePage.RejectedStatus:
                _dynamicHomePage = await new RecruitmentDynamicHomePage(context, true).ConfirmVacancyTitleAndStatus(status);
                break;

            case RecruitmentDynamicHomePage.ClosedStatus:
                await new SearchVacancyPageHelper(context).NavigateToHomeFromRaaDashboard("Home");
                _dynamicHomePage = await new RecruitmentDynamicHomePage(context, true).ConfirmClosedVacancyDetails(status);
                break;

            case RecruitmentDynamicHomePage.PendingReviewStatus:
                _dynamicHomePage = await new RecruitmentDynamicHomePage(context, true).ConfirmVacancyDetails(status, context.Get<RAADataHelper>().VacancyClosing);
                break;

            case RecruitmentDynamicHomePage.LiveStatus:
                _dynamicHomePage = await new RecruitmentDynamicHomePage(context, true).ConfirmLiveVacancyDetails(status);
                break;
        }
    }

    [Then(@"Employer can continue creating an advert")]
    public async Task ThenEmployerCanContinueCreatingAnAdvert() => await _dynamicHomePage.ContinueCreatingYourAdvert();

    [Then(@"Employer can go to Manage vacancy page")]
    public async Task ThenEmployerCanGoToManageVacancyPage() => await _dynamicHomePage.GoToManageVacancyPage();

    [Then(@"Employer can go to vacancy dashboard")]
    public async Task ThenEmployerCanGoToVacancyDashboard() => await _dynamicHomePage.GoToVacancyDashboard();

    [Then(@"the Employer can review and resubmit the vacancy")]
    public async Task ThenTheEmployerCanReviewAndResubmitTheVacancy()
    {
        var page = await _dynamicHomePage.ReviewYourVacancy();
        await page.ResubmitVacancy();
    }

}
