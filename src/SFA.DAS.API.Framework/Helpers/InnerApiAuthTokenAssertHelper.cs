using System.Threading.Tasks;

namespace SFA.DAS.API.Framework.Helpers;

public class InnerApiAuthTokenAssertHelper(ObjectContext objectContext)
{
    public async Task<RestResponse> ExecuteInnerApiAuthTokenAndAssertResponse(RestClient client, RestRequest request)
    {
        var response = await client.ExecuteAsync(request);

        var apidataCollector = new InnerApiAuthDataCollectionHelper(client, request, response);

        SetDebugInformation(apidataCollector);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Failed to get auth token.{Environment.NewLine} {apidataCollector.GetErrorResponseData()}");

        return response;
    }

    private void SetDebugInformation(RequestAndResponseCollectionHelper apidataCollector)
       => objectContext.SetDebugInformation($"{apidataCollector.GetRequestData()}{Environment.NewLine}{apidataCollector.GetResponseData()}");
}
