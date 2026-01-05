namespace SFA.DAS.Finance.APITests.Project.Helpers;

public class Outer_EmployerFinanceApiRestClient(ObjectContext objectContext, Outer_ApiAuthTokenConfig config) : Outer_BaseApiRestClient(objectContext, config)
{
    protected override string ApiName => "/employerfinance";

    public async Task<RestResponse> GetAccountMinimumSignedAgreementVersion(long accountId, HttpStatusCode expectedResponse)
    {
        return await Execute($"/Accounts/{accountId}/minimum-signed-agreement-version", expectedResponse);
    }

    public async Task<RestResponse> GetAccountUserWhichCanReceiveNotifications(long accountId, HttpStatusCode expectedResponse)
    {
        return await Execute($"/Accounts/{accountId}/users/which-receive-notifications", expectedResponse);
    }

    public async Task<RestResponse> GetPledges(long accountId, HttpStatusCode expectedResponse)
    {
        return await Execute($"/Pledges?accountId={accountId}", expectedResponse);
    }

    public async Task<RestResponse> GetTrainingCoursesFrameworks(HttpStatusCode expectedResponse)
    {
        return await Execute($"/TrainingCourses/frameworks", expectedResponse);
    }

    public async Task<RestResponse> GetTrainingCoursesStandards(HttpStatusCode expectedResponse)
    {
        return await Execute($"/TrainingCourses/standards", expectedResponse);
    }

    public async Task<RestResponse> GetTransfersCounts(long accountId, HttpStatusCode expectedResponse)
    {
        return await Execute($"/Transfers/{accountId}/counts", expectedResponse);
    }

    public async Task<RestResponse> GetTransfersFinancialBreakdown(long accountId, HttpStatusCode expectedResponse)
    {
        return await Execute($"/Transfers/{accountId}/financial-breakdown", expectedResponse);
    }
}
