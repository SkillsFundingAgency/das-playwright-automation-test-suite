using SFA.DAS.RAAProvider.UITests.Project.Helpers;
using SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.StepDefinitions;

[Binding]
public class ProviderCreateVacancySteps(ScenarioContext context)
{
    private readonly ProviderCreateVacancyStepsHelper _providerStepsHelper = new(context);
    
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
}
