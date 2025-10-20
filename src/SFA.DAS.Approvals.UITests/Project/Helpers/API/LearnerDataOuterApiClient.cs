using Azure;
using Mailosaur.Models;
using Polly;
using RestSharp;
using SFA.DAS.API.Framework;
using SFA.DAS.API.Framework.Configs;
using SFA.DAS.API.Framework.RestClients;
using SFA.DAS.Approvals.APITests.Project;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.API
{
    internal class LearnerDataOuterApiClient : Outer_BaseApiRestClient
    {
        protected override string ApiName => "/learnerdata";
        private readonly ScenarioContext _context;
        private readonly ObjectContext _objectContext;  
        private readonly Outer_ApprovalsAPIClient _restClient;
        private RestResponse _restResponse = null;

        public LearnerDataOuterApiClient(ScenarioContext context, Outer_ApiAuthTokenConfig config) : base(context.Get<ObjectContext>(), config.Apim_SubscriptionKey)
        {
            _context = context;
            _objectContext = context.Get<ObjectContext>();
            _restClient = _context.GetRestClient<Outer_ApprovalsAPIClient>();
        }

        public async Task<RestResponse> PostNewLearners(string resource, string payload)
        {
            await _restClient.CreateRestRequest(Method.Put, resource, payload);
            _restResponse = await SendRequestAsync(HttpStatusCode.Accepted);
            return _restResponse;
        }

        public async Task<RestResponse> GetLearners(string resource)
        {
            await _restClient.CreateRestRequest(Method.Get, resource);
            _restResponse = await SendRequestAsync(HttpStatusCode.OK);
            return _restResponse;
        }

        private async Task<RestResponse> SendRequestAsync(HttpStatusCode responseCode) => await _restClient.Execute(responseCode);

    }

}
