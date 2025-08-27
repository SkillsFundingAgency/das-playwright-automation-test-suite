using SFA.DAS.ApprenticeLogin.Service.Project;
using SFA.DAS.ApprenticeLogin.Service.Project.Helpers;
using SFA.DAS.ApprenticeLogin.Service.Project.Pages;
using SFA.DAS.ProvideFeedback.UITests.Project.Helpers;
using SFA.DAS.ProvideFeedback.UITests.Project.Tests.Pages;


namespace SFA.DAS.ProvideFeedback.UITests.Project.Tests.StepDefinitions;

[Binding]
public class ApprenticeFeedbackSteps
{
    private readonly ScenarioContext _context;
    private readonly SetApprenticeDetailsHelper _setApprenticeDetailsHelper;

    public ApprenticeFeedbackSteps(ScenarioContext context)
    {
        _context = context;
        _setApprenticeDetailsHelper = new SetApprenticeDetailsHelper(_context);
    }

    [Given(@"the apprentice logs into apprentice portal")]
    public async Task GivenTheApprenticeLogsIntoApprenticePortal()
    {
        var user = _context.GetUser<ApprenticeFeedbackUser>();

        _setApprenticeDetailsHelper.SetApprenticeDetailsInObjectContext(user);

        var page = new ApprenticeSignInPage(_context);

        await page.VerifyPage();

        await page.SubmitUserDetails(user);

        //_ = new ApprenticeOverviewPage(_context, false);
    }


    [Given(@"the apprentice is eligible to give feedback on their providers")]
    public async Task GivenTheApprenticeIsEligibleToGiveFeedbackOnTheirProviders()
    {
        var objectContext = _context.Get<ObjectContext>();

        var dbConfig = _context.Get<DbConfig>();

        var apprenticeId = objectContext.GetApprenticeId();

        var sqlHelper = new ApprenticeFeedbackSqlHelper(objectContext, dbConfig);

        await sqlHelper.ResetFeedbackEligibility(apprenticeId);
    }

    [When(@"apprentice completes the feedback journey for a training provider")]
    public async Task WhenApprenticeCompletesTheFeedbackJourneyForATrainingProvider()
    {
        var page = await new ApprenticeFeedbackSelectProviderPage(_context).SelectATrainingProvider();

        var page1 = await page.StartNow();

        var page2 = await page1.ProvideFeedback();

        var page3 = await page2.ProvideRating();

        var page4 = await page3.ChangeFeedbackAttribute();

        var page5 = await page4.GoToCheckYourAnswersPage();

        var page6 = await page5.ChangeOverallRating();

        var page7 = await page6.GoToCheckYourAnswersPage();

        var page8 = await page7.SubmitAnswers();

        await page8.NavigateToGiveFeedbackOnYourTrainingProvider();
    }

    [Given(@"the apprentice has not provided feedback previously")]
    public async Task GivenTheApprenticeHasNotProvidedFeedbackPreviously()
    {
        var objectContext = _context.Get<ObjectContext>();
        var dbConfig = _context.Get<DbConfig>();
        var apprenticeId = objectContext.GetApprenticeId();

        var sqlHelper = new ApprenticeFeedbackSqlHelper(objectContext, dbConfig);

        await sqlHelper.RemoveAllFeedback(apprenticeId);
    }

    [Then(@"the feedback status shows as Pending")]
    public async Task ThenTheFeedbackStatusShowsAsPending()
    {
        var feedbackCompletePage = new ApprenticeFeedbackHomePage(_context);

        var selectProviderPage = await feedbackCompletePage.NavigateToGiveFeedbackOnYourTrainingProvider();

        await selectProviderPage.VerifyFeedbackStatusAsPending();
    }

    [Then(@"the feedback status shows as Submitted")]
    public async Task ThenTheFeedbackStatusShowsAsSubmitted()
    {
        var selectProviderPage = new ApprenticeFeedbackSelectProviderPage(_context);

        await selectProviderPage.VerifyFeedbackStatusAsSubmitted();
    }
}
