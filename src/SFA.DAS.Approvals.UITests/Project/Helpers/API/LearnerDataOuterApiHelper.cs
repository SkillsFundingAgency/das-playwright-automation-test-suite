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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.API
{
    internal class LearnerDataOuterApiHelper : Outer_BaseApiRestClient
    {
        protected override string ApiName => "/learnerdata";
        private readonly ScenarioContext _context;
        private readonly Outer_ApprovalsAPIClient _restClient;
        private RestResponse _restResponse = null;

        public LearnerDataOuterApiHelper(ScenarioContext context, Outer_ApiAuthTokenConfig config) : base(context.Get<ObjectContext>(), config.Apim_SubscriptionKey)
        {
            _context = context;
            _restClient = _context.GetRestClient<Outer_ApprovalsAPIClient>();
        }

        public async Task<RestResponse> PostNewLearners(string resource, string payload)
        {
            await _restClient.CreateRestRequest(Method.Put, resource, payload);
            _restResponse = await Execute(HttpStatusCode.Accepted);
            return _restResponse;
        }

        private new async Task<RestResponse> Execute(HttpStatusCode responseCode) => await _restClient.Execute(responseCode);

    }
}
