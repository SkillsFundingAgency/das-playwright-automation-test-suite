using System.Net;
using RestSharp;

namespace SFA.DAS.Finance.APITests.Project.Helpers
{
    public class Inner_EmployerFinanceApiRestClient(ObjectContext objectContext, Inner_ApiFrameworkConfig config) : Inner_BaseApiRestClient(objectContext, config)
    {
        protected override string ApiBaseUrl => UrlConfig.InnerApiUrlConfig.Inner_EmployerFinanceApiBaseUrl;

        protected override string AppServiceName => $"{config.config.EmployerFinanceAppServiceName}-ar";

        public void ExecuteEndpoint(string endpoint, HttpStatusCode expectedResponse)
        {
            Execute(Method.Get, endpoint, string.Empty, expectedResponse).GetAwaiter().GetResult();
        }

        public RestResponse ExecuteEndpoint(string endpoint)
        {
            return Execute(Method.Get, endpoint, string.Empty, HttpStatusCode.OK).GetAwaiter().GetResult();
        }
    }
}