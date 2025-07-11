using SFA.DAS.FindEPAO.UITests.Project.Helpers;
using SFA.DAS.FindEPAO.UITests.Project.Tests.Pages;

namespace SFA.DAS.FindEPAO.UITests.Project.Tests.Steps
{
    [Binding]

    public class FindEPAOSteps
    {
        private readonly ScenarioContext _context;
        private readonly FindEPAOStepsHelper _findEPAOStepsHelper;
        private EPAOOrganisationsPage _ePAOOrganisationsPage;
        private EPAOOrganisationDetailsPage _ePAOOrganisationDetailsPage;
        private ZeroAssessmentOrganisationsPage _zeroAssessmentOrganisationsPage;

        public FindEPAOSteps(ScenarioContext context)
        {
            _context = context;
            _findEPAOStepsHelper = new FindEPAOStepsHelper(_context);
        }

        [Given(@"the user searches a standard with '(.*)' term")]
        public async Task GivenTheUserSearchesAStandardWithTerm(string searchTerm) => _ePAOOrganisationsPage = await _findEPAOStepsHelper.SearchForApprenticeshipStandard(searchTerm);

        [Given(@"the user searches a standard '(.*)' term with no EPAO")]
        public async Task GivenTheUserSearchesAStandardTermWithNoEPAO(string searchTerm) => _zeroAssessmentOrganisationsPage = await _findEPAOStepsHelper.SearchForApprenticeshipStandardWithNoEPAO(searchTerm);

        [Given(@"the user searches a standard '(.*)' term with single EPAO")]
        public async Task GivenTheUserSearchesAStandardTermWithSingleEPAO(string searchTerm) => _ePAOOrganisationDetailsPage = await _findEPAOStepsHelper.SearchForApprenticeshipStandardWithSingleEPAO(searchTerm);

        [Given(@"the user searches an integrated standard '(.*)' term")]
        public async Task GivenTheUserSearchesAnIntegratedStandardTerm(string searchTerm) => _ePAOOrganisationsPage = await _findEPAOStepsHelper.SearchForIntegratedApprenticeshipStandard(searchTerm);

        [When(@"the user clicks on view other end point organisations")]
        [Then(@"the user clicks on view other end point organisations")]
        public async Task WhenTheUserClicksOnViewOtherEndPointOrganisations() => _ePAOOrganisationsPage = await _ePAOOrganisationDetailsPage.SelectViewOtherEndPointOrganisations();

        [Then(@"the user is able to click back to the search apprenticeship page")]
        public async Task WhenTheUserClicksBack() => await _ePAOOrganisationDetailsPage.NavigateBackFromSingleEPAOOrganisationDetailsPage();

        [When(@"the user selects an EPAO from the list")]
        [Then(@"the user selects an EPAO from the list")]
        public async Task WhenTheUserSelectsAnEPAOFromTheList() => _ePAOOrganisationDetailsPage = await _ePAOOrganisationsPage.SelectFirstEPAOOrganisationFromList();

        [Then(@"the user is able to contact ESFA")]
        public async Task ThenTheUserIsAbleToContactESFA() => await _zeroAssessmentOrganisationsPage.IsContactESFAButtonDisplayed();

        [Then(@"the user is able to click back to homepage")]
        public async Task ThenTheUserIsAbleToClickBackToHomepage()
        {
            var page = await _ePAOOrganisationsPage.NavigateBackFromEPAOOrgansationPageToDetailsPage();

            var page1 = await page.NavigateBackFromEPAOOrgansationDetailsPageToOrganisationPage();

            var page2 = await page1.NavigateBackFromEPAOOrganisationsPageToSearchApprenticeshipTrainingPage();

            await page2.NavigateBackToHomePage();
        }
    }
}
