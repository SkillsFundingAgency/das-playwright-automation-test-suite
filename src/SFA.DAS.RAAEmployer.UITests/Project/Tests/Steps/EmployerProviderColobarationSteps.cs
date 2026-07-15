using NUnit.Framework;
using SFA.DAS.EmployerPortal.UITests.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using SFA.DAS.RAAEmployer.UITests.Project.Helpers;
using SFA.DAS.RAAProvider.UITests.Project.Helpers;
using EmployerStepsHelper = SFA.DAS.RAAEmployer.UITests.Project.Helpers.EmployerStepsHelper;
using ProviderStepsHelper = SFA.DAS.RAAProvider.UITests.Project.Helpers.ProviderStepHelper;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class EmployerProviderColobarationSteps
    {
        private readonly ScenarioContext _context;
        private readonly ObjectContext _objectContext;
        private readonly EmployerStepsHelper _employerStepsHelper;
        private readonly RAAEmployerLoginStepsHelper _rAAEmployerLoginHelper;
        private readonly ProviderStepsHelper _providerStepsHelper;
        private EasAccountUser _loginUser;
        private ProviderVacancySearchResultPage _resultPage;

        public EmployerProviderColobarationSteps(ScenarioContext context)
        {
            _context = context;
            _objectContext = context.Get<ObjectContext>();
            _employerStepsHelper = new EmployerStepsHelper(context);
            _providerStepsHelper = new ProviderStepsHelper(context);
            _rAAEmployerLoginHelper = new RAAEmployerLoginStepsHelper(_context);
        }

        [Given(@"^the Employer grants permission to the provider to create advert with review option$")]
        public async Task GivenTheEmployerGrantsPermissionToTheProviderToCreateAdvertWithReviewOption()
        {
            _loginUser = _context.GetUser<RAAEmployerProviderPermissionUser>();

            await _rAAEmployerLoginHelper.GoToHomePage(_loginUser);

            /*
             * these steps are executed as part of test data preparation.
             * 
            _employerPermissionsStepsHelper.RemovePermissisons(_providerPermissionConfig);

            _employerPermissionsStepsHelper.SetRecruitApprenticesPermission(_providerPermissionConfig.Ukprn, loginUser.PermissionOrganisationName);
            */
        }

        [Given(@"^the Employer grants permission to the provider to create advert with review option set as Yes$")]
        public async Task GivenTheEmployerGrantsPermissionToTheProviderToCreateAdvertWithReviewOptionSetAsYes()
        {
            _loginUser = _context.GetUser<RAAEmployerProviderYesPermissionUser>();

            var homePage = await _rAAEmployerLoginHelper.GoToHomePage(_loginUser);
        }

        [When(@"^the Provider submits a vacancy to the employer for review$")]
        public async Task WhenTheProviderSubmitsAVacancyToTheEmployerForReview()
        {
            var vacancyReferencePage = await new ProviderCreateVacancyStepsHelper(_context, true).CreateANewVacancyForSpecificEmployer(_loginUser.OrganisationName, _objectContext.GetHashedAccountId());

            await ConfirmationMessage(vacancyReferencePage, "Vacancy submitted to employer");
        }

        [When(@"^the Employer rejects the advert$")]
        public async Task WhenTheEmployerRejectsTheAdvert()
        {
            var page = await GoToVacancyCompletedPage();
            var page1 = await page.RejectAdvert();
            var page2 = await page1.ConfirmReject();

            await ConfirmationMessage(page2, "You've rejected this job advert");
        }

        [Then(@"^the Provider should see the advert with status: '(.*)'$")]
        public async Task ThenTheProviderShouldSeeTheAdvertWithStatus(string expectedStatus)
        {
            _resultPage = await _providerStepsHelper.SearchVacancyByVacancyReferenceInNewTab();

            await _resultPage.VerifyAdvertStatus(expectedStatus);
        }

        [When(@"^Provider re-submits the advert$")]
        public async Task WhenProviderRe_SubmitsTheAdvert()
        {
            var page = await _resultPage.GoToRejectedVacancyCompletedPage();

            await AssertMessage("has rejected this vacancy for the following reason", await page.GetNotificationBanner());

            var vacancyReferencePage = await page.ResubmitVacancyToEmployer();

            await ConfirmationMessage(vacancyReferencePage, "Vacancy resubmitted to employer");
        }

        [When(@"^the Employer approves the advert$")]
        public async Task WhenTheEmployerApprovesTheAdvert()
        {
            var page = await GoToVacancyCompletedPage();
            var page1 = await page.SubmitAdvert();
            var page2 = await page1.ConfirmSubmit();

            await ConfirmationMessage(page2, "You've submitted this job advert");
        }

        private async Task<VacancyCompletedAllSectionsPage> GoToVacancyCompletedPage()
        {
            var page = await _employerStepsHelper.YourAdvert();

            return await page.GoToVacancyCompletedPage();
        }

        private static async Task ConfirmationMessage(VacancyReferencePage vacancyReferencePage, string expected) => await AssertMessage(expected, await vacancyReferencePage.GetConfirmationMessage());

        private static async Task AssertMessage(string expected, string actual) => StringAssert.Contains(expected, actual);

    }
}
