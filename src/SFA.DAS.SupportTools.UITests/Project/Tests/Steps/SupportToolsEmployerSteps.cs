using SFA.DAS.Login.Service.Project;
using SFA.DAS.Registration.UITests.Project.Helpers;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Steps
{
    [Binding]
    public class SupportToolsEmployerSteps
    {
        private readonly ScenarioContext _context;
        private readonly EmployerPortalLoginHelper _employerPortalLoginHelper;
        private readonly EmployerHomePageStepsHelper _employerHomePageStepsHelper;
        private readonly UsersSqlDataHelper _usersSqlDataHelper;

        public SupportToolsEmployerSteps(ScenarioContext context)
        {
            _context = context;
            _employerPortalLoginHelper = new EmployerPortalLoginHelper(context);
            _employerHomePageStepsHelper = new EmployerHomePageStepsHelper(_context);
            _usersSqlDataHelper = new UsersSqlDataHelper(_context.Get<ObjectContext>(), _context.Get<DbConfig>());
        }

        [Given(@"the employer user can login to EAS")]
        public async Task GivenTheEmployerUserCanLoginToEAS()
        {
            await _employerHomePageStepsHelper.NavigateToEmployerApprenticeshipService();

            var user = _context.GetUser<LevyUser>();

            await _usersSqlDataHelper.ReinstateAccountInDb(user.Username);

            var homePage = await _employerPortalLoginHelper.Login(user, true);

            AccountSignOutHelper.SignOut(homePage);
        }

        [Then(@"the employer user can login to EAS")]
        public async Task ThenTheEmployerUserCanLoginToEAS() => await _employerHomePageStepsHelper.GotoEmployerHomePage();

        [Then(@"the employer user cannot login to EAS")]
        public async Task ThenTheEmployerUserCannotLoginToEAS()
        {
            var accountUnavailablePage = await _employerHomePageStepsHelper.ValidateUnsuccessfulLogon();

            AccountSignOutHelper.SignOut(accountUnavailablePage);
        }
    }
}
