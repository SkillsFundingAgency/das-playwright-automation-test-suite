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
    internal class LearnerDataOuterApiHelper : Outer_BaseApiRestClient
    {
        protected override string ApiName => "/learnerdata";
        private readonly ScenarioContext _context;
        private readonly ObjectContext _objectContext;  
        private readonly Outer_ApprovalsAPIClient _restClient;
        private RestResponse _restResponse = null;

        public LearnerDataOuterApiHelper(ScenarioContext context, Outer_ApiAuthTokenConfig config) : base(context.Get<ObjectContext>(), config.Apim_SubscriptionKey)
        {
            _context = context;
            _objectContext = context.Get<ObjectContext>();
            _restClient = _context.GetRestClient<Outer_ApprovalsAPIClient>();
        }

        public async Task<RestResponse> PostNewLearners(string resource, string payload)
        {
            await _restClient.CreateRestRequest(Method.Put, resource, payload);
            _restResponse = await Execute(HttpStatusCode.Accepted);
            return _restResponse;
        }

        public async Task<RestResponse> GetLearners(string resource)
        {
            await _restClient.CreateRestRequest(Method.Get, resource);
            _restResponse = await Execute(HttpStatusCode.OK);
            return _restResponse;
        }

        internal async Task<string?> FindLearnerKeyByUlnAsync(string resource, string targetUln)
        {
            int page = 1;
            const int pageSize = 100;

            _restResponse = await GetLearners($"{resource}?page={page}&pageSize={pageSize}");
            var content = JsonSerializer.Deserialize<LearnerResponse>(_restResponse.Content!);
            var totalPages = content?.TotalPages ?? 1;

            for (int i = totalPages; i > 0; i--)
            {
                var url = $"{resource}?page={i}&pageSize={pageSize}";
                _restResponse = await GetLearners(url);
                content = JsonSerializer.Deserialize<LearnerResponse>(_restResponse.Content!);

                if (content?.Learners != null)
                {
                    var match = content.Learners.FirstOrDefault(l => l.Uln == targetUln);
                    if (match != null)
                    {
                        _objectContext.SetDebugInformation($"ULN {targetUln} found on page# {i} with key:[{match.Key}]");
                        return match.Key;                        
                    }
                    else
                    {
                        _objectContext.SetDebugInformation($"ULN {targetUln} not found on page# {i}.");
                    }
                }
            }

            return null; // ULN not found


        }

        private new async Task<RestResponse> Execute(HttpStatusCode responseCode) => await _restClient.Execute(responseCode);

    }

    internal class LearnerResponse
    {
        [JsonPropertyName("learners")]
        public List<Learner> Learners { get; set; } = new();
        
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
    }

    internal class Learner
    {
        [JsonPropertyName("uln")]
        public string Uln { get; set; } = string.Empty;

        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;
    }
}
