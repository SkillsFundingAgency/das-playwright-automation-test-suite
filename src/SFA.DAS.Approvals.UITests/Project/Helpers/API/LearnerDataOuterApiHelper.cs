using Dynamitey;
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


        public async Task PushNewLearnersDataToAsViaNServiceBus(Apprenticeship apprenticeship, int Ukprn)
        {
            var serviceBusHelper = GlobalTestContext.ServiceBus;

            var learnerDataEvent = new LearnerDataEvent
            {
                ULN = long.Parse(apprenticeship.ApprenticeDetails.ULN),
                UKPRN = Ukprn,
                FirstName = apprenticeship.ApprenticeDetails.FirstName,
                LastName = apprenticeship.ApprenticeDetails.LastName,
                Email = apprenticeship.ApprenticeDetails.Email,
                DoB = apprenticeship.ApprenticeDetails.DateOfBirth,
                StartDate = apprenticeship.TrainingDetails.StartDate,
                PlannedEndDate = apprenticeship.TrainingDetails.EndDate,
                PercentageLearningToBeDelivered = apprenticeship.TrainingDetails.PercentageLearningToBeDelivered,
                EpaoPrice = apprenticeship.TrainingDetails.EpaoPrice,
                TrainingPrice = apprenticeship.TrainingDetails.TrainingPrice,
                AgreementId = apprenticeship.EmployerDetails.AgreementId,
                IsFlexiJob = apprenticeship.TrainingDetails.IsFlexiJob,
                StandardCode = apprenticeship.TrainingDetails.StandardCode,
                CorrelationId = Guid.NewGuid(),
                ReceivedDate = DateTime.UtcNow,
                ConsumerReference = apprenticeship.TrainingDetails.ConsumerReference,
                PlannedOTJTrainingHours = apprenticeship.TrainingDetails.PlannedOTJTrainingHours,
                AcademicYear = apprenticeship.TrainingDetails.AcademicYear
            };

            await serviceBusHelper.Publish(learnerDataEvent);
            objectContext.SetDebugInformation($"Publishing LearnerDataEvent to N-Service Bus for following learner:");
            objectContext.SetDebugInformation(JsonSerializer.Serialize(learnerDataEvent, new JsonSerializerOptions { WriteIndented = true }));

        }

        public async Task PushNewLearnersDataToASViaAPI(LearnerDataAPIDataModel learnersData, int Ukprn)
        {
            var resource = $"/providers/{Ukprn}/learners";
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

        public async Task<LearnerDataAPIDataModel> ConvertToLearnerDataAPIDataModel(Apprenticeship apprenticeship)
        {
            LearnerDataAPIDataModel learnerData = new LearnerDataAPIDataModel
            {
                Delivery = new Delivery
                {
                    OnProgramme = new List<OnProgramme>
                    {
                        new OnProgramme
                        {
                            StandardCode = apprenticeship.TrainingDetails.StandardCode,
                            StartDate = apprenticeship.TrainingDetails.StartDate.ToString("yyyy-MM-dd"),
                            ExpectedEndDate = apprenticeship.TrainingDetails.EndDate.ToString("yyyy-MM-dd"),
                            Costs = new List<Cost>
                            {
                                new Cost
                                {
                                    TrainingPrice = apprenticeship.TrainingDetails.TrainingPrice,
                                    EpaoPrice = apprenticeship.TrainingDetails.EpaoPrice,
                                    FromDate = apprenticeship.TrainingDetails.StartDate.ToString("yyyy-MM-dd")
                                }
                            },
                            LearningSupport = new List<LearningSupport>(),
                            PercentageOfTrainingLeft = apprenticeship.TrainingDetails.PercentageLearningToBeDelivered,
                            IsFlexiJob = apprenticeship.TrainingDetails.IsFlexiJob,
                            AgreementId = apprenticeship.EmployerDetails.AgreementId
                        }
                    },
                    EnglishAndMaths = new List<EnglishAndMaths>()
                },
                Learner = new Learner
                {
                    FirstName = apprenticeship.ApprenticeDetails.FirstName,
                    LastName = apprenticeship.ApprenticeDetails.LastName,
                    Email = apprenticeship.ApprenticeDetails.Email,
                    Uln = long.Parse(apprenticeship.ApprenticeDetails.ULN),
                    LearnerRef = apprenticeship.ProviderDetails.Ukprn.ToString(),
                    Dob = apprenticeship.ApprenticeDetails.DateOfBirth.ToString("yyyy-MM-dd"),
                    HasEhcp = false
                },
                ConsumerReference = apprenticeship.TrainingDetails.ConsumerReference
            };

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
        public Delivery Delivery { get; set; }
        public Learner Learner { get; set; }
        public string ConsumerReference { get; set; }
    }

    public class Delivery
    {
        public List<OnProgramme> OnProgramme { get; set; }
        public List<EnglishAndMaths> EnglishAndMaths { get; set; }
    }

    public class OnProgramme
    {
        public int StandardCode { get; set; }
        public string StartDate { get; set; }
        public string ExpectedEndDate { get; set; }
        public List<Cost> Costs { get; set; }
        public string CompletionDate { get; set; } = null;
        public string WithdrawalDate { get; set; } = null;
        public string PauseDate { get; set; } = null;
        public List<LearningSupport> LearningSupport { get; set; }
        public int PercentageOfTrainingLeft { get; set; } = 0;
        public bool IsFlexiJob { get; set; }
        public string AgreementId { get; set; }
    }

    public class EnglishAndMaths
    {
        public string Course { get; set; } = null;
        public int Amount { get; set; } = 0;
        public string StartDate { get; set; } = null;
        public string EndDate { get; set; } = null;
        public string CompletionDate { get; set; } = null;
        public string WithdrawalDate { get; set; } = null;
        public int PriorLearningPercentage { get; set; } = 0;
        public List<LearningSupport> LearningSupport { get; set; }
    }

    public class Cost
    {
        public int TrainingPrice { get; set; }
        public int EpaoPrice { get; set; }
        public string FromDate { get; set; }
    }

    public class LearningSupport
    {
        public string StartDate { get; set; } = null;
        public string EndDate { get; set; } = null;
    }

    public class Learner
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long Uln { get; set; }
        public string LearnerRef { get; set; }
        public string Dob { get; set; }
        public bool HasEhcp { get; set; } = false;
    }

    internal class LearnerResponse
    {
        [JsonPropertyName("learners")]
        public List<Learners> Learners { get; set; } = new();

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
    }

    internal class Learners
    {
        [JsonPropertyName("uln")]
        public string Uln { get; set; } = string.Empty;

        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;
    }



}
