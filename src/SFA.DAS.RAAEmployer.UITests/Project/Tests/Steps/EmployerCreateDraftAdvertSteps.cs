using SFA.DAS.RAA.APITests.Project.Tests.StepDefinitions;
using SFA.DAS.RAAEmployer.UITests.Project.Helpers;
using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;
using System.Net;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Steps;

[Binding]
public class EmployerCreateDraftAdvertSteps(ScenarioContext context)
{
    private readonly EmployerCreateDraftAdvertStepsHelper _stepsHelper = new(context);

    private YourApprenticeshipAdvertsHomePage _yourApprenticeshipAdvertsHomePage;

    [When(@"the Employer creates first Draft advert")]
    public async Task TheEmployerCreatesFirstDraftAdvert() => await ReturnToApplications(await _stepsHelper.CreateFirstDraftAdvert(new CreateAnApprenticeshipAdvertOrVacancyPage(context)));

    [Then(@"the Employer is able to delete the draft vacancy")]
    public async Task ThenTheEmployerIsAbleToDeleteTheDraftVacancy() => await _stepsHelper.CompleteDeleteOfDraftVacancy();

    [When(@"the Employer completes the Draft advert to cancel deleting the draft")]
    public async Task TheEmployerCreatesCompleteDraftAdvert() => await _stepsHelper.CompleteDraftAdvert(await GoToYourAdvertFromDraftAdverts());

    [Then(@"the Employer can open the draft and submits the advert")]
    public async Task TheEmployerCanOpenTheDraftAndSubmitsTheAdvert() => await _stepsHelper.SubmitDraftAdvert(await GoToYourAdvertFromDraftAdverts());

    [Given(@"the Employer creates Draft advert")]
    public async Task TheEmployerCreatesDraftAdvert() => await ReturnToApplications(await _stepsHelper.CreateDraftAdvert());

    [Then(@"the advert is saved as a draft")]
    public async Task ThenTheVacancyIsSavedAsADraft() => await GoToYourAdvertFromDraftAdverts();

    [When(@"Employer cancels after saving the title of the advert")]
    public async Task EmployerCancelsAfterSavingTheTitleOfTheAdvert() => _yourApprenticeshipAdvertsHomePage = await _stepsHelper.CancelAdvert();

    private async Task ReturnToApplications(CreateAnApprenticeshipAdvertOrVacancyPage page) { await page.ReturnToApplications(); _yourApprenticeshipAdvertsHomePage = new YourApprenticeshipAdvertsHomePage(context); }

    private async Task<CreateAnApprenticeshipAdvertOrVacancyPage> GoToYourAdvertFromDraftAdverts()
    {
        var page = await _yourApprenticeshipAdvertsHomePage.GoToYourAdvertFromDraftAdverts();

        return await page.CreateAnApprenticeshipAdvertPage();
    }
}

[Binding]
public class ApiStepWrapper
{
    private readonly ManageVacanciesSteps _apiSteps;

    public ApiStepWrapper(ManageVacanciesSteps apiSteps) { _apiSteps = apiSteps; }

    [When(@"the user sends POST request to vacancy with payload (.*)")]
    public async Task When_User_Sends_Post(string payload) => await _apiSteps.TheUserSendsRequestTo(RestSharp.Method.Post, "vacancy", payload);

    [Then(@"a (Created|Unauthorized) response is received")]
    public async Task ThenAOKResponseIsReceived(HttpStatusCode responseCode) => await _apiSteps.ThenAOKResponseIsReceived(responseCode);
    
}
