using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.ProvideFeedback.UITests.Project.Helpers;
using SFA.DAS.ProvideFeedback.UITests.Project.Tests.Pages;

namespace SFA.DAS.ProvideFeedback.UITests.Project.Tests.StepDefinitions;


[Binding]
public class EmployerFeedbackSteps(ScenarioContext context)
{
    private EmployerFeedbackCheckYourAnswersPage _providerFeedbackCheckYourAnswers;
    private readonly EmployerPortalLoginHelper _employerPortalLoginHelper = new(context);
    private readonly EmployerFeedbackSqlHelper _provideFeedbackSqlHelper = context.Get<EmployerFeedbackSqlHelper>();
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    [Given(@"the Employer logins into Employer Portal")]
    public async Task WhenTheEmployerLoginsIntoEmployerPortal()
    {
        var user = context.GetUser<EmployerFeedbackUser>();

        await _employerPortalLoginHelper.Login(user, true);

      // _objectContext.SetTestData(await _provideFeedbackSqlHelper.GetTestData(user.Username));
    }

    [Given("the Second Employer View only User logins into Employer Portal")]
    public async void GivenTheSecondEmployerViewOnlyUserLoginsIntoEmployerPortal()
    {
        var user = context.GetUser<EmployerViewOnlyUser>();

        await _employerPortalLoginHelper.Login(user, true);

      //  _objectContext.SetTestData(await _provideFeedbackSqlHelper.GetTestData(user.Username));
    }
       

    [Given(@"completes the feedback journey for a training provider")]
    public async Task GivenCompletesTheFeedbackJourneyForATrainingProvider()
    {
        var page = await new EmployerDashboardPage(context).ClickFeedbackLink();

        var page1 = await page.SelectTrainingProvider();

        var page2 = await page1.ConfirmTrainingProvider();

        _providerFeedbackCheckYourAnswers = await GoToCheckYourAnswersPage(page2);

        await _providerFeedbackCheckYourAnswers.SubmitAnswersNow();
    }

    [Given(@"completes the feedback journey for a training provider via survey code")]
    public async Task GivenCompletesTheFeedbackJourneyForATrainingProviderViaSurveyCode()
    {
        var page = await new EmployerDashboardPage(context).OpenFeedbackLinkWithSurveyCode();

        _providerFeedbackCheckYourAnswers = await GoToCheckYourAnswersPage(page);
    }

    [Then(@"the user can change the answers and submits")]
    public async Task ThenTheUserCanChangeTheAnswersAndSubmits()
    {
        var page = await _providerFeedbackCheckYourAnswers.ChangeQuestionOne();

        var page1 = await page.ContinueToCheckYourAnswers();

        var page2 = await page1.ChangeQuestionTwo();

        var page3 = await page2.ContinueToCheckYourAnswers();

        var page4 = await page3.ChangeQuestionThree();

        var page5 = await page4.SelectGoodAndContinue();

        await page5.SubmitAnswersNow();
    }

    [Then(@"the user can not resubmit the feedback")]
    public async Task ThenTheUserCanNotResubmitTheFeedback() => await new EmployerFeedbackAlreadySubmittedPage(context).VerifyPage();

    private static async Task<EmployerFeedbackCheckYourAnswersPage> GoToCheckYourAnswersPage(EmployerFeedbackHomePage emppage)
    {
        var page = await emppage.StartNow();

        var page1 = await page.SelectOptionsForDoingWell();

        var page2 = await page1.ContinueToOverallRating();

        return await page2.SelectVPoorAndContinue();
    }
}
