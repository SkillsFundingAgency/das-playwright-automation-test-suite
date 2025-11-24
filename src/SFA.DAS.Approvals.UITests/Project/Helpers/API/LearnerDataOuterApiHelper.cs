using RestSharp;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.LearnerData.Events;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public async Task PushNewLearnersDataToAsViaNServiceBus(List<LearnerDataAPIDataModel> learnersData, int academicYear)
        {
            var serviceBusHelper = GlobalTestContext.ServiceBus;

            foreach (var learner in learnersData)
            {
                var learnerDataEvent = new LearnerDataEvent
                {
                    ULN = long.Parse(learner.ULN),
                    UKPRN = long.Parse(learner.UKPRN),
                    FirstName = learner.FirstName,
                    LastName = learner.LastName,
                    Email = learner.LearnerEmail,
                    DoB = DateTime.Parse(learner.DateOfBirth),
                    StartDate = DateTime.Parse(learner.StartDate),
                    PlannedEndDate = DateTime.Parse(learner.PlannedEndDate),
                    PercentageLearningToBeDelivered = learner.PercentageLearningToBeDelivered,
                    EpaoPrice = learner.EPAOPrice,
                    TrainingPrice = learner.TrainingPrice,
                    AgreementId = learner.AgreementId,
                    IsFlexiJob = learner.IsFlexiJob,
                    StandardCode = learner.StandardCode,
                    CorrelationId = Guid.NewGuid(),
                    ReceivedDate = DateTime.UtcNow,
                    ConsumerReference = learner.ConsumerReference,
                    PlannedOTJTrainingHours = learner.PlannedOTJTrainingHours,
                    AcademicYear = academicYear
                };

                await serviceBusHelper.Publish(learnerDataEvent);
                objectContext.SetDebugInformation($"Publishing LearnerDataEvent to N-Service Bus for following learner:");
                objectContext.SetDebugInformation(JsonSerializer.Serialize(learnerDataEvent, new JsonSerializerOptions { WriteIndented = true })
);


            }
        }

        public async Task PushNewLearnersDataToASViaAPI(List<LearnerDataAPIDataModel> learnersData, int academicYear)
        {
            var resource = $"/provider/{learnersData.First().UKPRN}/academicyears/{academicYear}/learners";
            var payload = JsonHelper.Serialize(learnersData).ToString();
            await learnerDataOuterApiClient.PostNewLearners(resource, payload);
        }

        public async Task CheckApprenticeIsAvailableInApprovedLearnersList(Apprenticeship apprenticeship)
        {
            var resource = $"/Learners/providers/{apprenticeship.ProviderDetails.Ukprn}/academicyears/{apprenticeship.TrainingDetails.AcademicYear}/learners";
            var learnerKey = await GetLearnerKeyByUlnAsync(resource, apprenticeship.ApprenticeDetails.ULN);
            var expectedLearningIdKey = apprenticeship.ApprenticeDetails.LearningIdKey.Trim();
            Assert.AreEqual(learnerKey.Trim(), expectedLearningIdKey, $"LearningIdKey key extracted from db [{expectedLearningIdKey}] differs from api response: [{learnerKey.Trim()}]");

        }

        public async Task<List<LearnerDataAPIDataModel>> ConvertToLearnerDataAPIDataModel(List<Apprenticeship> listOfApprenticeships)
        {
            List<LearnerDataAPIDataModel> listOfLearnerData = new List<LearnerDataAPIDataModel>();
            await Task.Delay(100);

            foreach (var apprenticeship in listOfApprenticeships)
            {
                listOfLearnerData.Add(await ConvertToLearnerDataAPIDataModel(apprenticeship));
            }

            return listOfLearnerData;
        }


        private async Task<LearnerDataAPIDataModel> ConvertToLearnerDataAPIDataModel(Apprenticeship apprenticeship)
        {
            LearnerDataAPIDataModel learnerData = new LearnerDataAPIDataModel();

            learnerData.UKPRN = apprenticeship.ProviderDetails.Ukprn.ToString();
            learnerData.ULN = apprenticeship.ApprenticeDetails.ULN;
            learnerData.FirstName = apprenticeship.ApprenticeDetails.FirstName;
            learnerData.LastName = apprenticeship.ApprenticeDetails.LastName;
            learnerData.LearnerEmail = apprenticeship.ApprenticeDetails.Email;
            learnerData.DateOfBirth = apprenticeship.ApprenticeDetails.DateOfBirth.ToString("yyyy-MM-dd");
            learnerData.StartDate = apprenticeship.TrainingDetails.StartDate.ToString("yyyy-MM-dd");
            learnerData.PlannedEndDate = apprenticeship.TrainingDetails.EndDate.ToString("yyyy-MM-dd");
            learnerData.PercentageLearningToBeDelivered = apprenticeship.TrainingDetails.PercentageLearningToBeDelivered;
            learnerData.EPAOPrice = apprenticeship.TrainingDetails.EpaoPrice;
            learnerData.TrainingPrice = apprenticeship.TrainingDetails.TrainingPrice;
            learnerData.AgreementId = apprenticeship.EmployerDetails.AgreementId;
            learnerData.IsFlexiJob = apprenticeship.TrainingDetails.IsFlexiJob;
            learnerData.PlannedOTJTrainingHours = apprenticeship.TrainingDetails.PlannedOTJTrainingHours;
            learnerData.StandardCode = apprenticeship.TrainingDetails.StandardCode;
            learnerData.ConsumerReference = apprenticeship.TrainingDetails.ConsumerReference;

            await Task.Delay(100);
            return learnerData;

        }


        private async Task<string?> GetLearnerKeyByUlnAsync(string resource, string targetUln)
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

    public class LearnerDataAPIDataModel
    {
        public string ULN { get; set; }
        public string UKPRN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LearnerEmail { get; set; }
        public string DateOfBirth { get; set; }
        public string StartDate { get; set; }
        public string PlannedEndDate { get; set; }
        public int PercentageLearningToBeDelivered { get; set; }
        public int EPAOPrice { get; set; }
        public int TrainingPrice { get; set; }
        public string AgreementId { get; set; }
        public bool IsFlexiJob { get; set; }
        public int PlannedOTJTrainingHours { get; set; }
        public int StandardCode { get; set; }
        public string ConsumerReference { get; set; }
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
