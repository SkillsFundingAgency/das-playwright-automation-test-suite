using NUnit.Framework;
using SFA.DAS.EmployerPortal.UITests.Project;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;
using SFA.DAS.ProviderPortal.UITests.Project.Helpers;
using SFA.DAS.RAAEmployer.UITests.Project.Helpers;
using SFA.DAS.RAAProvider.UITests.Project.Helpers;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "raatransfer")]

    public class TransferVacancyFromProviderToEmployerSteps
    {
        private readonly ScenarioContext _context;
        private readonly ObjectContext _objectContext;
        private readonly RAAEmployerLoginStepsHelper _rAAEmployerLoginHelper;
        private readonly EmployerPermissionsStepsHelper _employerPermissionsStepsHelper;
        private readonly EmployerHomePageStepsHelper _employerHomePageStepsHelper;
        private EasAccountUser _loginUser;
        protected readonly ProviderNoPermissionsConfig _providerConfig;

        public TransferVacancyFromProviderToEmployerSteps(ScenarioContext context)
        {
            _context = context;
            _objectContext = context.Get<ObjectContext>();
            _rAAEmployerLoginHelper = new RAAEmployerLoginStepsHelper(_context);
            _employerPermissionsStepsHelper = new EmployerPermissionsStepsHelper(_context);
            _providerConfig = _context.GetProviderNoPermissionConfig<ProviderNoPermissionsConfig>();
            _employerHomePageStepsHelper = new EmployerHomePageStepsHelper(_context);
        }

        [Given(@"^the Employer grants permission to the provider to create advert with review option$")]
        public async Task GivenTheEmployerGrantsPermissionToTheProviderToCreateAdvertWithReviewOption()
        {
            _loginUser = _context.GetUser<RAAEmployerProviderNoPermissionUser>();
                       
            await _rAAEmployerLoginHelper.GoToHomePage(_loginUser);

            await _employerPermissionsStepsHelper.UpdateProviderRecruitPermission(_providerConfig, (AddApprenticePermissions.NoToAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprenticesButEmployerWillReview));
        }

        [Given(@"^the Employer grants permission to the provider to create advert with review option set as Yes$")]
        public async Task GivenTheEmployerGrantsPermissionToTheProviderToCreateAdvertWithReviewOptionSetAsYes()
        {
            _loginUser = _context.GetUser<RAAEmployerProviderNoPermissionUser>();

            await _rAAEmployerLoginHelper.GoToHomePage(_loginUser);

            await _employerPermissionsStepsHelper.UpdateProviderRecruitPermission(_providerConfig, (AddApprenticePermissions.NoToAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprentices));
        }

        [When(@"^the Employer revokes permission to the provider to create advert$")]
        public async Task WhenTheEmployerRevokesPermissionToTheProviderToCreateAdvertWithReviewOptionSetAsYes()
        {
            _loginUser = _context.GetUser<RAAEmployerProviderNoPermissionUser>();

            await _employerHomePageStepsHelper.NavigateToEmployerApprenticeshipService(true);

            await _employerPermissionsStepsHelper.UpdateProviderRecruitPermission(_providerConfig, (AddApprenticePermissions.NoToAddApprenticeRecords, RecruitApprenticePermissions.NoToRecruitApprentices));
        }

        [When(@"^the Provider submits a vacancy to the employer for review$")]
        public async Task WhenTheProviderSubmitsAVacancyToTheEmployerForReview()
        {
            var vacancyReferencePage = await new ProviderCreateVacancyStepsHelper(_context, true).CreateANewVacancyForSpecificEmployer(_loginUser.OrganisationName, _objectContext.GetHashedAccountId());

            await ConfirmationMessage(vacancyReferencePage, "Vacancy submitted to employer");
        }

        [When(@"^the Provider submits a vacancy to the DfE for review$")]
        public async Task WhenTheProviderSubmitsAVacancyToTheDfEForReview()
        {
            var vacancyReferencePage = await new ProviderCreateVacancyStepsHelper(_context, true).CreateANewVacancyForSpecificEmployer(_loginUser.OrganisationName, _objectContext.GetHashedAccountId());

            await ConfirmationMessage(vacancyReferencePage, "Vacancy submitted for approval");

            await vacancyReferencePage.ClickSignout();
        }

        private static async Task ConfirmationMessage(VacancyReferencePage vacancyReferencePage, string expected) => await AssertMessage(expected, await vacancyReferencePage.GetConfirmationMessage());

        private static async Task AssertMessage(string expected, string actual) => StringAssert.Contains(expected, actual);

    }
}
