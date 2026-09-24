using Newtonsoft.Json;
using NServiceBus;
using RestSharp;
using SFA.DAS.API.Framework;
using SFA.DAS.API.Framework.Configs;
using SFA.DAS.API.Framework.RestClients;
using SFA.DAS.Approvals.UITests.Project.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.API
{
    public class CommitmentsInnerApiClient(ObjectContext objectContext, Inner_ApiFrameworkConfig config) : Inner_BaseApiRestClient(objectContext, config)
    {
        protected override string ApiBaseUrl => DAS.API.Framework.UrlConfig.InnerApiUrlConfig.Inner_CommitmentsApiBaseUrl;

        protected override string AppServiceName => $"{config.config.CommitmentsAppServiceName}-ar";

        internal async Task<RestResponse> PutCoCApprovalRequest(CoCApprovalRequest payload, string learningKey)
        {
            var (tokenType, accessToken) = await GetAADAuthToken();
            return await Execute(Method.Put, $"/approvals/{learningKey}", payload, null);
        }



    }    
}
