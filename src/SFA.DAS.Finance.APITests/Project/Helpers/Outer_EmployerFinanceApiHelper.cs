
namespace SFA.DAS.Finance.APITests.Project.Helpers;

public class Outer_EmployerFinanceApiHelper
{
    private readonly Outer_EmployerFinanceApiRestClient _outerEmployerFinanceApiRestClient;
    private readonly Outer_EmployerFinanceHealthApiRestClient _outerEmployerFinanceHealthApiRestClient;
    private readonly ObjectContext _objectContext;
    protected readonly FrameworkHelpers.RetryHelper _assertHelper;

    internal Outer_EmployerFinanceApiHelper(ScenarioContext context)
    {
        _objectContext = context.Get<ObjectContext>();
        _assertHelper = context.Get<FrameworkHelpers.RetryHelper>();
        _outerEmployerFinanceApiRestClient = new Outer_EmployerFinanceApiRestClient(_objectContext, context.GetOuter_ApiAuthTokenConfig());
        _outerEmployerFinanceHealthApiRestClient = new Outer_EmployerFinanceHealthApiRestClient(_objectContext);
    }

    public async Task<RestResponse> Ping() => await _outerEmployerFinanceHealthApiRestClient.Ping(HttpStatusCode.OK);

    public async Task<RestResponse> CheckHealth() => await _outerEmployerFinanceHealthApiRestClient.CheckHealth(HttpStatusCode.OK);

    public async Task<RestResponse> GetAccountMinimumSignedAgreementVersion(long accountId)
    {
        return await _outerEmployerFinanceApiRestClient.GetAccountMinimumSignedAgreementVersion(accountId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetAccountUserWhichCanReceiveNotifications(long accountId)
    {
        return await _outerEmployerFinanceApiRestClient.GetAccountUserWhichCanReceiveNotifications(accountId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetAccountUserByUserRefAndEmail(string userRef, string email)
    {
        return await _outerEmployerFinanceApiRestClient.GetAccountUserByUserRefAndEmail(userRef, email, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetAccountUserAccountsByUserRef(string userRef)
    {
        return await _outerEmployerFinanceApiRestClient.GetAccountUserAccountsByUserRef(userRef, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetAccountUserWhichCanReceiveNotificationsByHashedId(string hashedAccountId)
    {
        return await _outerEmployerFinanceApiRestClient.GetAccountUserWhichCanReceiveNotificationsByHashedId(hashedAccountId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetPledges(long accountId)
    {
        return await _outerEmployerFinanceApiRestClient.GetPledges(accountId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetTrainingCoursesFrameworks()
    {
        return await _outerEmployerFinanceApiRestClient.GetTrainingCoursesFrameworks(HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetTrainingCoursesStandards()
    {
        return await _outerEmployerFinanceApiRestClient.GetTrainingCoursesStandards(HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetTransfersCounts(long accountId)
    {
        return await _outerEmployerFinanceApiRestClient.GetTransfersCounts(accountId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetTransfersFinancialBreakdown(long accountId)
    {
        return await _outerEmployerFinanceApiRestClient.GetTransfersFinancialBreakdown(accountId, HttpStatusCode.OK);
    }
}