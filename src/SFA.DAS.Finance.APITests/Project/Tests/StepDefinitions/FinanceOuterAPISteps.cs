using SFA.DAS.Finance.APITests.Project.Helpers;


namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions;

[Binding]
public class FinanceOuterAPISteps(ScenarioContext context)
{
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();
    private readonly Outer_EmployerFinanceApiHelper _employerFinanceOuterApiHelper = new(context);

    [Then(@"the employer finance outer api is reachable")]
    public async Task ThenTheApprenticeCommitmentsApiIsReachable() => await _employerFinanceOuterApiHelper.Ping();

    [Then(@"endpoint /Accounts/\{accountId}/minimum-signed-agreement-version can be accessed")]
    public async Task ThenEndpointAccountsAccountIdMinimum_Signed_Agreement_VersionCanBeAccessed()
    {
        var accountId = GetAccountId();
        await _employerFinanceOuterApiHelper.GetAccountMinimumSignedAgreementVersion(long.Parse(accountId));
    }

    [Then(@"endpoint /Accounts/\{accountId}/users/which-receive-notifications can be accessed")]
    public async Task ThenEndpointAccountsAccountIdUsersWhich_Receive_NotificationsCanBeAccessed()
    {
        var accountId = GetAccountId();
        await _employerFinanceOuterApiHelper.GetAccountUserWhichCanReceiveNotifications(long.Parse(accountId));
    }

    [Then(@"endpoint /Pledges\?accountId=\{accountId} can be accessed")]
    public async Task ThenEndpointPledgesAccountIdAccountIdCanBeAccessed()
    {
        var accountId = GetAccountId();
        await _employerFinanceOuterApiHelper.GetPledges(long.Parse(accountId));
    }

    [Then(@"endpoint /TrainingCourses/frameworks can be accessed")]
    public async Task ThenEndpointTrainingCoursesFrameworksCanBeAccessed()
    {
        await _employerFinanceOuterApiHelper.GetTrainingCoursesFrameworks();
    }

    [Then(@"endpoint /TrainingCourses/standards can be accessed")]
    public async Task ThenEndpointTrainingCoursesStandardsCanBeAccessed()
    {
        await _employerFinanceOuterApiHelper.GetTrainingCoursesStandards();
    }

    [Then(@"endpoint /Transfers/\{accountId}/counts can be accessed")]
    public async Task ThenEndpointTransfersAccountIdCountsCanBeAccessed()
    {
        var accountId = GetAccountId();
        await _employerFinanceOuterApiHelper.GetTransfersCounts(long.Parse(accountId));
    }

    [Then(@"endpoint /Transfers/\{accountId}/financial-breakdown can be accessed")]
    public async Task ThenEndpointTransfersAccountIdFinancial_BreakdownCanBeAccessed()
    {
        var accountId = GetAccountId();
        await _employerFinanceOuterApiHelper.GetTransfersFinancialBreakdown(long.Parse(accountId));
    }

    private string GetAccountId() => _objectContext.GetAccountId();

}
