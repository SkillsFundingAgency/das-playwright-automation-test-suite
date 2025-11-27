using SFA.DAS.RAA.Service.Project.Helpers;
using SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;
using SFA.DAS.RAAEmployer.UITests.Project.Helpers;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Steps;

[Binding]
public class EmployerCreateAdvertSteps(ScenarioContext context)
{
    private readonly EmployerCreateAdvertStepsHelper _employerCreateVacancyStepsHelper = new(context);

    private CreateAnApprenticeshipAdvertOrVacancyPage _createAnApprenticeshipAdvertPage;

    [Given(@"the Employer can create an advert by entering all the Optional fields")]
    public async Task TheEmployerCanCreateAnAdvertByEnteringAllTheOptionalFields()
    {
        _employerCreateVacancyStepsHelper.optionalFields = true;

        await _employerCreateVacancyStepsHelper.CreateANewAdvert(RAAConst.Anonymous);
    }

    [When(@"the Employer creates first submitted advert")]
    public async Task TheEmployerCreatesFirstSubmittedAdvert() => await  _employerCreateVacancyStepsHelper.CreateFirstAdvertAndSubmit(_createAnApprenticeshipAdvertPage);

    //[Given(@"the employer continue to add advert in the Recruitment")]
    //public async Task TheEmployerContinueToAddAdvertInTheRecruitment()
    //{
    //    _createAnApprenticeshipAdvertPage = await _employerCreateVacancyStepsHelper.AddAnAdvert();
    //}

    [When(@"Employer selects '(National Minimum Wage|National Minimum Wage For Apprentices|Fixed Wage Type|Set As Competitive)' in the first part of the journey")]
    public async Task EmployerSelectsInTheFirstPartOfTheJourney(string wageType) => await _employerCreateVacancyStepsHelper.CreateANewAdvert_WageType(wageType);

    [Given(@"the Employer creates an offline advert with disability confidence")]
    public async Task TheEmployerCreatesAnOfflineAdvertWithDisabilityConfidence() => await _employerCreateVacancyStepsHelper.CreateOfflineVacancy();

    [Given(@"the Employer clones and creates an advert")]
    public async Task TheEmployerClonesAndCreatesAnAdvert() => await _employerCreateVacancyStepsHelper.CloneAnAdvert();

    [Given(@"the Employer creates an advert by selecting different work location")]
    public async Task TheEmployerCreatesAnAdvertBySelectingDifferentWorkLocation() => await _employerCreateVacancyStepsHelper.CreateANewAdvert(RAAConst.LegalEntityName, "different");

    [Given(@"the Employer creates an anonymous advert")]
    public async Task TheEmployerCreatesAnAnonymousAdvert() => await _employerCreateVacancyStepsHelper.CreateANewAdvert(RAAConst.Anonymous);

    [Given(@"the Employer creates an advert by using a registered name")]
    public async Task TheEmployerCreatesAnanAdvertByUsingARegisteredName() => await _employerCreateVacancyStepsHelper.CreateANewAdvert();

    [Given(@"the Employer creates a foundation advert by using a registered name")]
    public async Task TheEmployerCreatesAfoundationAdvertByUsingARegisteredName()
    {
        context["isFoundationAdvert"] = true;
        await _employerCreateVacancyStepsHelper.CreateANewAdvert();
    }

    [Given(@"the Employer creates an advert by using a trading name")]
    public async Task TheEmployerCreatesAnAdvertByUsingATradingName() => await _employerCreateVacancyStepsHelper.CreateANewAdvert(RAAConst.ExistingTradingName);

    [Given(@"the Employer creates a foundation advert by using a trading name")]
    public async Task TheEmployerCreatesAFoundationAdvertByUsingATradingName()
    {
        context["isFoundationAdvert"] = true;
        await _employerCreateVacancyStepsHelper.CreateANewAdvert(RAAConst.ExistingTradingName);
    }

    [Given(@"the Employer creates an advert with ""(.*)"" work location")]
    public async Task GivenTheEmployerCreatesAnAdvertWithWorkLocation(string locationType) => await _employerCreateVacancyStepsHelper.CreateANewAdvert(locationType, locationType);

    [Given(@"the Employer creates an advert with ""(.*)"" work location and '(.*)' wage type")]
    public async Task GivenTheEmployerCreatesAnAdvertWithWorkLocationAndWageType(string locationType, string wageType) => await _employerCreateVacancyStepsHelper.CreateANewAdvert_LocationAndWageType(locationType, wageType);

}
