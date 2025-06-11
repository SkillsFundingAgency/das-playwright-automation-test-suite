

namespace SFA.DAS.RequestApprenticeshipTraining.UITests.Project.Tests.StepDefinitions;

[Binding]
public class RatEmployerSteps(ScenarioContext context)
{
    private readonly FATeStepsHelper _fATeStepsHelper = new(context);

    private AskIfTrainingProvidersCanRunThisCoursePage landingPage;

    private TrainingRequestDetailPage trainingRequestDetailPage;

    private readonly EmployerHomePageStepsHelper _homePageStepsHelper = new(context);

    [Given("an employer user can login to EAS")]
    public async Task AnEmployerUserCanLoginToEAS()
    {
        await _homePageStepsHelper.NavigateToEmployerApprenticeshipService();

        await new EmployerPortalLoginHelper(context).Login(context.GetUser<RatEmployerUser>(), true);
    }

    [Then("the employer requests apprenticeship training")]
    public async Task TheEmployerRequestsApprenticeshipTraining()
    {
        var page = await GoToRatHomePage();

        await page.GoToApprenticeshipTrainingCourses();
    }

    [Given(@"an employer requests apprenticeship training")]
    public async Task AnEmployerRequestsApprenticeshipTraining() => await RequestTrainingProvider(false);

    [Given(@"an employer adds location to requests apprenticeship training")]
    public async Task AnEmployerAddsLocationToRequestsApprenticeshipTraining() => await RequestTrainingProvider(true);

    [When(@"the employer logs in to rat employer account")]
    public async Task WhenTheEmployerLogsInToRatEmployerAccount() => await LoginViaRat(context.GetUser<RatEmployerUser>());

    [When(@"the employer logs in to rat cancel employer account")]
    public async Task TheEmployerLogsInToRatCancelEmployerAccount() => await LoginViaRat(context.GetUser<RatCancelEmployerUser>());

    [When(@"the employer logs in to rat multi employer account")]
    public async Task TheEmployerLogsInToRatMultiEmployerAccount() => await LoginViaRat(context.GetUser<RatMultiEmployerUser>());

    [Then(@"the employer submits the request for single location")]
    public async Task TheEmployerSubmitsTheRequestForSingleLocation()
    {
        var page = await landingPage.ClickStarNow();

        var page1 = await page.EnterMoreThan1Apprentices();

        var page2 = await page1.ClickYesToChooseSingleLocation();

        var page3 = await page2.GoToTrainingOptionsPage(true);

        await SelectActiveRequest(page3);
    }

    [Then(@"the employer submits the request for multiple location")]
    public async Task TheEmployerSubmitsTheRequest()
    {
        var page = await landingPage.ClickStarNow();

        var page1 = await page.EnterMoreThan1Apprentices();

        var page2 = await page1.ClickNoToChooseMultipleLocation();

        var page3 = await page2.ChooseRegion();

        await SelectActiveRequest(page3);
    }

    [Then(@"the employer submits the request for one apprentice")]
    public async Task TheEmployerSubmitsTheRequestForOneApprentice()
    {
        var page = await landingPage.ClickStarNow();

        var page1 = await page.Enter1Apprentices();

        var page2 = await page1.GoToTrainingOptionsPage(false);

        await SelectActiveRequest(page2);
    }

    [Then(@"the employer can cancel the request")]
    public async Task TheEmployerCanCancelTheRequest()
    {
        var page = await trainingRequestDetailPage.CancelYourRequest();

        await page.SubmitCancelRequest();
    }

    [Then(@"the employer receives the response")]
    public async Task TheEmployerReceivesTheResponse()
    {
        await _homePageStepsHelper.GotoEmployerHomePage();

        var page = await GoToRatHomePage();

        var page1 = await page.SelectActiveRequest();

        await page1.VerifyProviderResponse();
    }

    private async Task LoginViaRat(RatEmployerBaseUser loginUser) => landingPage = await new EmployerPortalViaRatLoginHelper(context).LoginViaRat(loginUser);

    private async Task SelectActiveRequest(SelectTrainingOptionsPage selectTrainingOptionsPage)
    {
        var page = await selectTrainingOptionsPage.SelectTrainingOptions();

        var page1 = await page.SubmitAnswers();

        var page2 = await page1.ReturnToRequestPage();

        trainingRequestDetailPage = await page2.SelectActiveRequest();
    }

    private async Task RequestTrainingProvider(bool filterLocation)
    {
        var title = await context.Get<RoatpV2SqlDataHelper>().GetTitlethatProviderDeosNotOffer(context.GetProviderConfig<ProviderConfig>().Ukprn);

        await _fATeStepsHelper.SearchForTrainingCourse(title);

        var page = await new ApprenticeshipTrainingCoursesPage(context).SelectFirstTrainingResult(title);

        var page1 = await page.ViewProvidersForThisCourse(filterLocation, string.Empty);

        await page1.RequestTrainingProvider();
    }

    private async Task<FindApprenticeshipTrainingAndManageRequestsPage> GoToRatHomePage() => await new RatEmployerHomePage(context).GoToRatHomePage();

}