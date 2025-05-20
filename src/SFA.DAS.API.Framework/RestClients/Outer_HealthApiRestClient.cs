using SFA.DAS.API.Framework.Helpers;
using System.Threading.Tasks;

namespace SFA.DAS.API.Framework.RestClients;

public class Outer_HealthApiRestClient(ObjectContext objectContext, string baseurl)
{
    public async Task<RestResponse> Ping(HttpStatusCode expectedResponse) => await Execute($"/ping", expectedResponse);

    public async Task<RestResponse> CheckHealth(HttpStatusCode expectedResponse) => await Execute($"/health", expectedResponse);

    private async Task<RestResponse> Execute(string resource, HttpStatusCode expectedResponse) => await new ApiAssertHelper(objectContext).ExecuteAndAssertResponse(expectedResponse, new RestClient(baseurl), new RestRequest { Method = Method.Get, Resource = resource });
}
