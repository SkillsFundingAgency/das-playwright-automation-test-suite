using SFA.DAS.Approvals.UITests.Project.Events;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static SFA.DAS.API.Framework.UrlConfig;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.API
{
    /*      
     *                      **IMPORTANT NOTE**
     *  Service Bus Tools is just a proxy to send messages to the Service Bus. 
     *  It is not a real API and does not have any validation or business logic. 
     *  It is just a tool to post messages to the Service Bus for testing purposes only!
     *  
     */

    internal class ServiceBusToolsApiClient
    {
        private readonly ScenarioContext context;
        private readonly ObjectContext objectContext;
        private readonly string url;

        public ServiceBusToolsApiClient(ScenarioContext context) 
        {
            this.context = context;
            objectContext = context.Get<ObjectContext>();
            url = InnerApiUrlConfig.ServiceBusToolsBaseUrl + "api/";
        }


        internal async Task<HttpResponseMessage> PostLearningWithdrawnEvent(LearningWithdrawnEvent payload)
        {
            var functionKey = context.GetApprovalsConfig<ApprovalsConfig>().LearningWithdrawnEventFunctionKey;
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-functions-key", functionKey);
            var apiRoute = "LearningWithdrawn";

            return await client.PostAsJsonAsync(url+apiRoute, payload);      
        }
    

}
}
