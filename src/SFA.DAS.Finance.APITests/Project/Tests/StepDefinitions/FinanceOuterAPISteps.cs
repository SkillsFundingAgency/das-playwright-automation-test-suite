using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlDbHelpers;


namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class FinanceOuterAPISteps
    {
        private readonly ObjectContext _objectContext;
        private readonly Outer_EmployerFinanceApiHelper _employerFinanceOuterApiHelper;
        private readonly EmployerFinanceSqlHelper _employerFinanceSqlDbHelper;
        private readonly EmployerAccountsSqlHelper _employerAccountsSqlDbHelper;

        public FinanceOuterAPISteps(ScenarioContext context)
        {
            _objectContext = context.Get<ObjectContext>();
            _employerFinanceOuterApiHelper = new Outer_EmployerFinanceApiHelper(context);
            _employerFinanceSqlDbHelper = context.Get<EmployerFinanceSqlHelper>();
            _employerAccountsSqlDbHelper = context.Get<EmployerAccountsSqlHelper>();
            var accountid = _employerFinanceSqlDbHelper.SetAccountId();
            _employerAccountsSqlDbHelper.SetHashedAccountId(accountid);
            _employerFinanceSqlDbHelper.SetEmpRef();
        }


        [Then(@"the employer finance outer api is reachable")]
        public void ThenTheApprenticeCommitmentsApiIsReachable() => _employerFinanceOuterApiHelper.Ping();

        [Then(@"endpoint /Accounts/\{accountId}/minimum-signed-agreement-version can be accessed")]
        public void ThenEndpointAccountsAccountIdMinimum_Signed_Agreement_VersionCanBeAccessed()
        {
            var accountId = GetAccountId();
            _employerFinanceOuterApiHelper.GetAccountMinimumSignedAgreementVersion(long.Parse(accountId));
        }


        [Then(@"endpoint /Accounts/\{accountId}/users/which-receive-notifications can be accessed")]
        public void ThenEndpointAccountsAccountIdUsersWhich_Receive_NotificationsCanBeAccessed()
        {
            var accountId = GetAccountId();
            _employerFinanceOuterApiHelper.GetAccountUserWhichCanReceiveNotifications(long.Parse(accountId));
        }

        [Then(@"endpoint /Pledges\?accountId=\{accountId} can be accessed")]
        public void ThenEndpointPledgesAccountIdAccountIdCanBeAccessed()
        {
            var accountId = GetAccountId();
            _employerFinanceOuterApiHelper.GetPledges(long.Parse(accountId));
        }

        [Then(@"endpoint /TrainingCourses/frameworks can be accessed")]
        public void ThenEndpointTrainingCoursesFrameworksCanBeAccessed()
        {
            _employerFinanceOuterApiHelper.GetTrainingCoursesFrameworks();
        }

        [Then(@"endpoint /TrainingCourses/standards can be accessed")]
        public void ThenEndpointTrainingCoursesStandardsCanBeAccessed()
        {
            _employerFinanceOuterApiHelper.GetTrainingCoursesStandards();
        }

        [Then(@"endpoint /Transfers/\{accountId}/counts can be accessed")]
        public void ThenEndpointTransfersAccountIdCountsCanBeAccessed()
        {
            var accountId = GetAccountId();
            _employerFinanceOuterApiHelper.GetTransfersCounts(long.Parse(accountId));
        }

        [Then(@"endpoint /Transfers/\{accountId}/financial-breakdown can be accessed")]
        public void ThenEndpointTransfersAccountIdFinancial_BreakdownCanBeAccessed()
        {
            var accountId = GetAccountId();
            _employerFinanceOuterApiHelper.GetTransfersFinancialBreakdown(long.Parse(accountId));
        }

        private string GetAccountId() => _objectContext.GetAccountId();

    }
}
