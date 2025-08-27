using SFA.DAS.ProvideFeedback.UITests.Project.Helpers;
using SFA.DAS.ProvideFeedback.UITests.Project.Tests.Pages;
using SFA.DAS.ProviderLogin.Service.Project;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using TechTalk.SpecFlow.Assist;

namespace SFA.DAS.ProvideFeedback.UITests.Project.Tests.StepDefinitions;

[Binding]
public class ProviderFeedbackSteps(ScenarioContext context)
{
    [When(@"The provider logs in to the provider portal")]
    public async Task GivenTheProviderLogsInToTheProviderPortal()
    {
        var providerCommonStepsHelper = new ProviderHomePageStepsHelper(context);
        
        await providerCommonStepsHelper.GoToProviderHomePage(false);
    }

    [When(@"the provider opts to view their feedback")]
    public async Task WhenTheProviderOptsToViewTheirFeedback()
    {
        var homePage = new FeedbackProviderHomePage(context);

        await homePage.SelectYourFeedback();
    }

    [Given(@"the provider has been rated by apprentices as follows")]
    public async Task GivenTheProviderHasBeenRatedByApprenticesAsFollows(Table table)
    {
        var objectContext = context.Get<ObjectContext>();
        var dbConfig = context.Get<DbConfig>();
        var providerConfig = context.GetProviderConfig<ProviderConfig>();
        var ukprn = providerConfig.Ukprn;
        var providerName = providerConfig.Name;

        var sqlHelper = new ApprenticeFeedbackSqlHelper(objectContext, dbConfig);

        var data = table.CreateSet<ProviderRating>().ToList();

        await sqlHelper.CreateApprenticeProviderFeedback(data, ukprn, providerName);

        await sqlHelper.GenerateFeedbackSummaries();
    }

    [Given(@"the provider has been rated by employers as follows")]
    public async Task GivenTheProviderHasBeenRatedByEmployersAsFollows(Table table)
    {
        var objectContext = context.Get<ObjectContext>();
        var dbConfig = context.Get<DbConfig>();
        var providerConfig = context.GetProviderConfig<ProviderConfig>();
        var ukprn = providerConfig.Ukprn;

        var sqlHelper = new EmployerFeedbackSqlHelper(objectContext, dbConfig);

        var data = table.CreateSet<ProviderRating>().ToList();

        await sqlHelper.CreateEmployerFeedback(ukprn, data);

        await sqlHelper.CreateEmployerFeedbackResults(ukprn, data);

        await sqlHelper.GenerateFeedbackSummaries();
    }

    [Then(@"their overall apprentice feedback score is '([^']*)'")]
    public async Task ThenTheirOverallScoreIs(string rating)
    {
        var summaryPage = new FeedbackOverviewPage(context);

        await summaryPage.VerifyApprenticeFeedbackRating("All", rating);
    }

    [When(@"they select the apprentice feedback tab for the current academic year")]
    public async Task WhenTheySelectTheTabForTheCurrentAcademicYear()
    {
        var academicYearHelper = new AcademicYearHelper();
        var academicYear = academicYearHelper.GetCurrentAcademicYear();

        var objectContext = context.Get<ObjectContext>();
        objectContext.Set("academic-year", academicYear);

        var summaryPage = new FeedbackOverviewPage(context);

        await summaryPage.SelectApprenticeTabForAcademicYear(academicYear);
    }

    [When(@"they select the apprentice feedback tab for the previous academic year")]
    public async Task WhenTheySelectTheTabForThePreviousAcademicYear()
    {
        var academicYearHelper = new AcademicYearHelper();
        var academicYear = academicYearHelper.GetPreviousAcademicYear();

        var objectContext = context.Get<ObjectContext>();
        objectContext.Replace("academic-year", academicYear);

        var summaryPage = new FeedbackOverviewPage(context);

        await summaryPage.SelectApprenticeTabForAcademicYear(academicYear);
    }

    [Then(@"their apprentice feedback score for that year is '([^']*)'")]
    public async Task ThenTheirScoreForThatYearIs(string rating)
    {
        var objectContext = context.Get<ObjectContext>();
        var academicYear = objectContext.Get("academic-year");

        var summaryPage = new FeedbackOverviewPage(context);

        await summaryPage.VerifyApprenticeFeedbackRating(academicYear, rating);
    }

    [Then(@"their overall employer feedback score is '([^']*)'")]
    public async Task ThenTheirOverallEmployerFeedbackScoreIs(string rating)
    {
        var summaryPage = new FeedbackOverviewPage(context);

        await summaryPage.VerifyEmployerFeedbackRating("All", rating);

    }

    [When(@"they select the employer feedback tab for the current academic year")]
    public async Task WhenTheySelectTheEmployerFeedbackTabForTheCurrentAcademicYear()
    {
        var academicYearHelper = new AcademicYearHelper();
        var academicYear = academicYearHelper.GetCurrentAcademicYear();

        var objectContext = context.Get<ObjectContext>();
        objectContext.Set("academic-year", academicYear);

        var summaryPage = new FeedbackOverviewPage(context);

        await summaryPage.SelectEmployerTabForAcademicYear(academicYear);
    }

    [Then(@"their employer feedback score for that year is '([^']*)'")]
    public async Task ThenTheirEmployerFeedbackScoreForThatYearIs(string rating)
    {
        var objectContext = context.Get<ObjectContext>();
        var academicYear = objectContext.Get("academic-year");

        var summaryPage = new FeedbackOverviewPage(context);

        await summaryPage.VerifyEmployerFeedbackRating(academicYear, rating);
    }

    [When(@"they select the employer feedback tab for the previous academic year")]
    public async Task WhenTheySelectTheEmployerFeedbackTabForThePreviousAcademicYear()
    {
        var academicYearHelper = new AcademicYearHelper();
        var academicYear = academicYearHelper.GetPreviousAcademicYear();

        var objectContext = context.Get<ObjectContext>();
        objectContext.Replace("academic-year", academicYear);

        var summaryPage = new FeedbackOverviewPage(context);

        await summaryPage.SelectEmployerTabForAcademicYear(academicYear);
    }

    [Then(@"they see the following text: ""([^""]*)""")]
    public async Task ThenTheySeeTheFollowingText(string expectedText)
    {
        var summaryPage = new FeedbackOverviewPage(context);

        await summaryPage.VerifyText(expectedText);
    }
}
