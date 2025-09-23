using RestSharp;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.API
{
    internal class LearnerDataOuterApiHelper
    {
        private readonly ScenarioContext context;
        private readonly ObjectContext objectContext;
        private readonly LearnerDataOuterApiClient learnerDataOuterApiClient;
        private RestResponse restResponse = null;

        public LearnerDataOuterApiHelper(ScenarioContext _context)
        {
            context = _context;
            objectContext = context.Get<ObjectContext>();
            learnerDataOuterApiClient = context.Get<LearnerDataOuterApiClient>();
        }

        internal async Task<string?> FindLearnerKeyByUlnAsync(string resource, string targetUln)
        {
            int page = 1;
            const int pageSize = 100;

            restResponse = await learnerDataOuterApiClient.GetLearners($"{resource}?page={page}&pageSize={pageSize}");
            var content = JsonSerializer.Deserialize<LearnerResponse>(restResponse.Content!);
            var totalPages = content?.TotalPages ?? 1;

            for (int i = totalPages; i > 0; i--)
            {
                var url = $"{resource}?page={i}&pageSize={pageSize}";
                restResponse = await learnerDataOuterApiClient.GetLearners(url);
                content = JsonSerializer.Deserialize<LearnerResponse>(restResponse.Content!);

                if (content?.Learners != null)
                {
                    var match = content.Learners.FirstOrDefault(l => l.Uln == targetUln);
                    if (match != null)
                    {
                        objectContext.SetDebugInformation($"ULN {targetUln} found on page# {i} with key:[{match.Key}]");
                        return match.Key;
                    }
                    else
                    {
                        objectContext.SetDebugInformation($"ULN {targetUln} not found on page# {i}.");
                    }
                }
            }

            return null; // ULN not found


        }




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
