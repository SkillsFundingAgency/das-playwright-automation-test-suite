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

        public async Task PushNewLearnersDataToASViaAPI(LearnerDataAPIDataModel learnerData, int Ukprn)
        {
            var resource = $"/providers/{Ukprn}/learners";
            var payload = JsonHelper.Serialize(learnerData).ToString();
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
                            AgreementId = apprenticeship.EmployerDetails.AgreementId,
                            StartDate = apprenticeship.TrainingDetails.StartDate.ToString("yyyy-MM-dd"),
                            ExpectedEndDate = apprenticeship.TrainingDetails.EndDate.ToString("yyyy-MM-dd"),
                            PercentageOfTrainingLeft = apprenticeship.TrainingDetails.PercentageLearningToBeDelivered,
                            Costs = new List<Cost>
                            {
                                new Cost
                                {
                                    TrainingPrice = apprenticeship.TrainingDetails.TrainingPrice,
                                    EpaoPrice = apprenticeship.TrainingDetails.EpaoPrice,
                                    FromDate = apprenticeship.TrainingDetails.StartDate.ToString("yyyy-MM-dd")
                                }
                            },
                            Care = new Care
                            {
                                Careleaver = false,
                                employerConsent = false
                            },
                            LearningSupport = new List<LearningSupport>(),                            
                            IsFlexiJob = apprenticeship.TrainingDetails.IsFlexiJob                            
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
                    var match = content.Learners.FirstOrDefault(l => l.Uln == long.Parse(targetUln));
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
        public string LearnAimRef { get; set; } = "ZPROG001";
        public string AgreementId { get; set; }
        public string StartDate { get; set; }
        public string ExpectedEndDate { get; set; }
        public int OffTheJobHours { get; set; }
        public int PercentageOfTrainingLeft { get; set; } = 80;
        public List<Cost> Costs { get; set; }
        public string CompletionDate { get; set; } = null;
        public string WithdrawalDate { get; set; } = null;
        public string PauseDate { get; set; } = null;
        public Care Care { get; set; }
        public List<LearningSupport> LearningSupport { get; set; }
        public bool IsFlexiJob { get; set; }
        
    }

    public class EnglishAndMaths
    {
        public string LearnAimRef { get; set; } = "60346474";
        public int CourseCode { get; set; }
        public string StartDate { get; set; } = null;
        public string EndDate { get; set; } = null;
        public string WithdrawalDate { get; set; } = null;
        public string CompletionDate { get; set; } = null;
        public List<LearningSupport> LearningSupport { get; set; }
    }

    public class Cost
    {
        public int TrainingPrice { get; set; }
        public int EpaoPrice { get; set; }
        public string FromDate { get; set; }
    }

    public class Care
    {
        public bool Careleaver { get; set; } = false;
        public bool employerConsent { get; set; } = false;
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
        //public long Uln { get; set; }
        public string LearnerRef { get; set; }
        public string Dob { get; set; }
        public bool HasEhcp { get; set; } = false;
        
        [JsonPropertyName("uln")]
        public long? Uln { get; set; } = null;

        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;
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





}
