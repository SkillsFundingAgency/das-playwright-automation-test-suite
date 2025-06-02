﻿namespace SFA.DAS.API.Framework.Helpers;

public class InnerApiAuthDataCollectionHelper(RestClient client, RestRequest request, RestResponse response) : RequestAndResponseCollectionHelper(client, request, response)
{
    protected override string GetRequestBody()
    {
        var list = GetRequestParameters();

        if (list.Count == 0) return string.Empty;

        var request = list.ToString().Split("&").ToList();

        var model = new InnerApiOAuthModel(string.Empty, string.Empty, string.Empty);

        var clientid = request.Single(x => x.StartsWith(model.ClientId.Key)).Replace($"{model.ClientId.Key}=", string.Empty);

        var resource = request.Single(x => x.StartsWith(model.Resource.Key)).Replace($"{model.Resource.Key}=", string.Empty);

        model = new InnerApiOAuthModel(clientid, HashedValue, resource);

        return GetBody(model.ToString());
    }

    protected override string GetResponseBody() => GetBody(EmptyJson);

    private static string EmptyJson => "{" + HashedValue + "}";
}
