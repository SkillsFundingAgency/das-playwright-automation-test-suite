using System.Threading.Tasks;

namespace SFA.DAS.API.Framework.Helpers;

public class ApiAssertHelper(ObjectContext objectContext)
{
    public async Task<RestResponse> ExecuteAndAssertResponse(HttpStatusCode expectedResponse, RestClient client, RestRequest request)
    => await ExecuteAndAssertResponse(expectedResponse, string.Empty, client, request);

    public async Task<RestResponse> ExecuteAndAssertResponse(HttpStatusCode expectedResponse, string responseContent, RestClient client, RestRequest request)
    {
        var response = await client.ExecuteAsync(request);

        var apidataCollector = new ApiDataCollectionHelper(client, request, response);

        SetDebugInformation(apidataCollector);

        Assert.Multiple(() =>
        {
            if (expectedResponse == HttpStatusCode.OK)
                Assert.IsTrue(response.IsSuccessful, "Expected HttpStatusCode.OK, response status code does not indicate success");

            Assert.AreEqual(expectedResponse, response.StatusCode, apidataCollector.GetErrorResponseData());

            if (!string.IsNullOrEmpty(responseContent)) StringAssert.Contains(responseContent, response.Content);
        });

        return response;
    }

    private void SetDebugInformation(RequestAndResponseCollectionHelper apidataCollector)
        => objectContext.SetDebugInformation($"{apidataCollector.GetRequestData()}{Environment.NewLine}{apidataCollector.GetResponseData()}");

}