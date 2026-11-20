using System.Net;
using SFA.DAS.API.Framework.RestClients;
using RestSharp;

namespace SFA.DAS.TransferMatching.UITests.Project.Helpers;

public class TransferMatchingJobsConfig
{
    public string HttpAutomaticApplicationApprovalFunction_Code { get; set; }
    public string HttpAutomaticApplicationRejectionFunction_Code { get; set; }
    public string HttpAutomaticApplicationDeclineFunction_Code { get; set; }
    public string HttpAutomaticApplicationExpireFunction_Code { get; set; }
}

public class TransferMatchingJobsHelper(ScenarioContext context)
{
    private readonly TransferMatchingJobsConfig config = context.Get<TransferMatchingJobsConfig>();
    public Task<RestResponse> RunApplicationsWithAutomaticApprovalJob() =>
        Run("ApplicationsWithAutomaticApproval", config.HttpAutomaticApplicationApprovalFunction_Code);
    public Task<RestResponse> RunApplicationsWithAutomaticRejectionJob() =>
        Run("ApplicationsWithAutomaticRejection", config.HttpAutomaticApplicationRejectionFunction_Code);
    public Task<RestResponse> RunApplicationsWithAutoDeclineJob() =>
        Run("ApplicationsWithAutomaticDecline", config.HttpAutomaticApplicationDeclineFunction_Code);
    public Task<RestResponse> RunApplicationsWithAutoExpireJob() =>
        Run("ApplicationsWithAutomaticExpire", config.HttpAutomaticApplicationExpireFunction_Code);
    private async Task<RestResponse> Run(string function, string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.StartsWith("__", StringComparison.Ordinal))
            throw new InvalidOperationException($"Configure the TransferMatching function key for {function}.");
        var client = new JobClient(context.Get<ObjectContext>(), code);
        return await client.Run(function);
    }

    private sealed class JobClient(ObjectContext objectContext, string key) : OuterJobs_BaseApiRestClient(objectContext, key)
    {
        protected override string ApiBaseUrl => SFA.DAS.API.Framework.UrlConfig.InnerApiUrlConfig.LevyTransferMatchingJobs_BaseUrl;
        public async Task<RestResponse> Run(string function) => await Execute($"/{function}", HttpStatusCode.OK);
    }
}
