using SFA.DAS.RAAProvider.UITests.Project.Helpers;
using SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.StepDefinitions;

[Binding]
public class ProviderCreateVacancySteps(ScenarioContext context)
{
    private readonly ProviderCreateVacancyStepsHelper _providerStepsHelper = new(context);
    private readonly ProviderCreateDraftVacancyStepsHelper _stepsHelper = new(context);

    private RecruitmentHomePage _recruitmentHomePage;

    [Then(@"the Provider creates anonymous vacancy through View all your vacancies page")]
    public async Task ThenTheProviderCreatesAnonymousVacancyThroughViewAllYourVacanciesPage() => await _providerStepsHelper.CreateAnonymousVacancy();

    [When(@"the Provider creates a vacancy with ""(.*)"" work locations and ""(.*)"" wage type")]
    public async Task GivenTheProviderCreatesAVacancyWithWorkLocationsAndWageTypeAndAdditionalQuestions(string locationType, string wageType)
            => await _providerStepsHelper.CreateVacancyForLocationAndWageTypes(locationType, wageType);

    [Given(@"the Provider creates a vacancy by using a registered name")]
    public async Task GivenTheProviderCreatesAVacancyByUsingARegisteredName() => await _providerStepsHelper.CreateANewVacancyForRandomEmployer();

    [Given(@"the Provider creates a vacancy with ""(.*)"" work locations by entering all the Optional fields and ""(.*)"" additional questions")]
    public async Task GivenTheProviderCreatesAVacancyWithWorkLocationsByEnteringAllTheOptionalFields(string locationType, string additionalQuestions)
    {
        _providerStepsHelper.optionalFields = true;

        bool enterQuestion1, enterQuestion2;

        switch (additionalQuestions)
        {
            case "first":
                enterQuestion1 = true;
                enterQuestion2 = false;
                break;
            case "second":
                enterQuestion1 = false;
                enterQuestion2 = true;
                break;
            default:
                enterQuestion1 = true;
                enterQuestion2 = true;
                break;
        }

        await _providerStepsHelper.CreateVacancyForLocationTypes(locationType, enterQuestion1, enterQuestion2);
    }

    [Given(@"the Provider creates a foundation vacancy by using a registered name")]
    public async Task GivenTheProviderCreatesAFoundationVacancyByUsingARegisteredName()
    {
        context["isFoundationAdvert"] = true;
        await _providerStepsHelper.CreateANewVacancyForRandomEmployer();
    }

    [When(@"the Provider creates an Offline vacancy")]
    public async Task WhenTheProviderCreatesAnOfflineVacancy() => await _providerStepsHelper.CreateOfflineVacancy();

    [When(@"Provider selects '(.*)' in the first part of the journey")]
    public async Task WhenProviderSelectsInTheFirstPartOfTheJourney(string wageType) => await _providerStepsHelper.CreateVacancyForWageType(wageType);

    [Given("Provider cancels after saving the title of the advert")]
    public async Task ProviderCancelsAfterSavingTheTitleOfTheAdvert() => _recruitmentHomePage = await _providerStepsHelper.CancelAdvert();

    [Then(@"the vacancy is saved as a draft")]
    public async Task ThenTheVacancyIsSavedAsADraft() => await GoToYourAdvertFromDraftAdverts();

    [Given(@"the Provider creates Draft advert")]
    public async Task TheProviderCreatesDraftAdvert() => await ReturnToApplications(await _stepsHelper.CreateDraftAdvert());

    [Then(@"^the Provider can open the draft and submits the advert$")]
    public async Task TheProviderCanOpenTheDraftAndSubmitsTheAdvert() => await _stepsHelper.SubmitDraftAdvert(await GoToYourAdvertFromDraftAdverts());

    [When(@"^the Provider completes the Draft advert to cancel deleting the draft$")]
    public async Task TheProviderCreatesCompleteDraftAdvert() => await _stepsHelper.CompleteDraftAdvert(await GoToYourAdvertFromDraftAdverts());

    [Then(@"^the Provider is able to delete the draft vacancy$")]
    public async Task ThenTheProviderIsAbleToDeleteTheDraftVacancy() => await _stepsHelper.CompleteDeleteOfDraftVacancy();

    private async Task ReturnToApplications(CreateAnApprenticeshipAdvertOrVacancyPage page) { await page.ReturnToApplications(); _recruitmentHomePage = new RecruitmentHomePage(context); }

    private async Task<CreateAnApprenticeshipAdvertOrVacancyPage> GoToYourAdvertFromDraftAdverts()
    {
        var page = await _recruitmentHomePage.GoToYourAdvertFromDraftAdverts();

        return await page.CreateAnApprenticeshipVacancyPage();
    }
}
