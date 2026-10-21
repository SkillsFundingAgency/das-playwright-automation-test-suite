using SFA.DAS.Approvals.UITests.Project.Events;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.API;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Models;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    internal class ServiceBusToolsRelatedSteps
    {
        private readonly ScenarioContext context;
        private readonly ObjectContext objectContext;
        

        public ServiceBusToolsRelatedSteps(ScenarioContext _context)
        {
            context = _context;
            objectContext = context.Get<ObjectContext>();
            
        }


        [When(@"LearningWithdrawnEvent is received for the apprentice")]
        [When(@"LearningWithdrawnEvent is received with different stop date and reason code for the same apprentice")]
        public async Task WhenLearningWithdrawnEventIsReceivedForTheApprentice()
        {
            UpdateStopDateAndWithdrawalReasonCodeInTheContext();
            var apprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var apprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId;
            var learningWithdrawnEvent 
                = new LearningWithdrawnEvent
                {
                    LearningKey = Guid.NewGuid(),
                    ApprenticeshipId = apprenticeshipId,
                    Created = DateTime.Now.ToString("yyyy-MM-dd"),
                    WithdrawalDate = apprenticeship.TrainingDetails.StopDate.ToString("yyyy-MM-dd"),
                    withdrawalReasonCode = apprenticeship.TrainingDetails.WithdrawalReasonCode
                };

            ServiceBusToolsApiClient serviceBusToolsApiClient = new ServiceBusToolsApiClient(context);
            var response = await serviceBusToolsApiClient.PostLearningWithdrawnEvent(learningWithdrawnEvent);
            objectContext.SetDebugInformation($"Publishing learningWithdrawnEvent to N-Service Bus (via sbus-tools) for ApprenticeshipId:[{apprenticeshipId}]");

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    objectContext.SetDebugInformation($"Successfully published learningWithdrawnEvent event to N-Service Bus (via sbus-tools) for ApprenticeshipId:[{apprenticeshipId}]");
                    break;
                case System.Net.HttpStatusCode.Forbidden:
                    objectContext.SetDebugInformation($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
                    objectContext.SetDebugInformation("please run following command after replacing initials and ip address: az webapp config access-restriction add -g das-pp-sbus-tools-rg -n das-pp-sbus-tools-fa --rule-name NM --action Allow --ip-address 86.131.225.87 --priority 500");
                    throw new Exception($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
                default:
                    objectContext.SetDebugInformation($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
                    throw new Exception($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
            }          
        }

        [When(@"LearningPausedEvent is received for the apprentice")]
        public async Task WhenLearningPausedEventIsReceivedForTheApprentice()
        {
            UpdateStopDateAndWithdrawalReasonCodeInTheContext();
            var apprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var apprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId;
            var learningPausedEvent
                = new LearningPausedEvent
                {
                    LearningKey = Guid.NewGuid(),
                    ApprenticeshipId = apprenticeshipId,
                    Created = DateTime.Now.ToString("yyyy-MM-dd"),
                    PauseDate = apprenticeship.TrainingDetails.StopDate.ToString("yyyy-MM-dd")
                };

            ServiceBusToolsApiClient serviceBusToolsApiClient = new ServiceBusToolsApiClient(context);
            var response = await serviceBusToolsApiClient.PostLearningPausedEvent(learningPausedEvent);
            objectContext.SetDebugInformation($"Publishing learningPausedEvent to N-Service Bus (via sbus-tools) for ApprenticeshipId:[{apprenticeshipId}]");

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    objectContext.SetDebugInformation($"Successfully published learningPausedEvent event to N-Service Bus (via sbus-tools) for ApprenticeshipId:[{apprenticeshipId}]");
                    break;
                case System.Net.HttpStatusCode.Forbidden:
                    objectContext.SetDebugInformation($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
                    objectContext.SetDebugInformation("please run following command after replacing initials and ip address: az webapp config access-restriction add -g das-pp-sbus-tools-rg -n das-pp-sbus-tools-fa --rule-name NM --action Allow --ip-address 86.131.225.87 --priority 500");
                    throw new Exception($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
                default:
                    objectContext.SetDebugInformation($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
                    throw new Exception($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
            }
        }

        [When(@"^Learning domain sends ApprovedLearningUpdatedEvent for below ""(.*)"": ""(.*)"", ""(.*)"" and ""(.*)""$")]
        public async Task WhenLearningDomainSendsApprovedLearningUpdatedEventForBelowAnd(string category, string changeType, string oldValue, string newValue)
        {
            var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var apprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId;
            var learningType = apprenticeship.TrainingDetails.LearningType;
            var learningKey = "651903fa-f145-4568-bd4d-899c23aeed8f";   //apprenticeship.ApprenticeDetails.LearningIdKey;

            var approvedLearningUpdatedEvent = new ApprovedLearningUpdatedEvent
            {
                ApprenticeshipId = apprenticeshipId,
                LearningType = Enum.GetName(typeof(LearningType), learningType),
                LearningKey = learningKey,
                LearningUri = $"/{learningKey}",
                Changes = new List<ApprenticeshipFieldChange>
                {
                    new ApprenticeshipFieldChange
                    {
                        ChangeType = changeType,
                        Data = new ApprenticeshipData
                        {
                            Old = oldValue,
                            New = newValue
                        }
                    }
                }
            };

            ServiceBusToolsApiClient serviceBusToolsApiClient = new ServiceBusToolsApiClient(context);
            var response = await serviceBusToolsApiClient.PostApprovedLearningUpdatedEvent(approvedLearningUpdatedEvent);
            objectContext.SetDebugInformation($"Publishing approvedLearningUpdatedEvent to N-Service Bus (via sbus-tools) for ApprenticeshipId:[{apprenticeshipId}]");

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    objectContext.SetDebugInformation($"Successfully published approvedLearningUpdatedEvent event to N-Service Bus (via sbus-tools) for ApprenticeshipId:[{apprenticeshipId}]");
                    break;
                case System.Net.HttpStatusCode.Forbidden:
                    objectContext.SetDebugInformation($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
                    objectContext.SetDebugInformation("please run following command after replacing initials and ip address: az webapp config access-restriction add -g das-pp-sbus-tools-rg -n das-pp-sbus-tools-fa --rule-name NM --action Allow --ip-address 86.131.225.87 --priority 500");
                    throw new Exception($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
                default:
                    objectContext.SetDebugInformation($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
                    throw new Exception($"Failed to publish the event due to error : {response.StatusCode} + {response.ReasonPhrase}");
            }

        }






        private void UpdateStopDateAndWithdrawalReasonCodeInTheContext()
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var apprenticeship = listOfApprenticeship.FirstOrDefault();
            var startDate = apprenticeship.TrainingDetails.StartDate;
            var existingStopDate = apprenticeship.TrainingDetails.StopDate;                    
            var endDate = (existingStopDate > startDate) ? existingStopDate : (apprenticeship.TrainingDetails.EndDate > DateTime.Now) ? DateTime.Now : apprenticeship.TrainingDetails.EndDate;
            var existingWithdrawalReasonCode = apprenticeship.TrainingDetails.WithdrawalReasonCode;

            apprenticeship.TrainingDetails.StopDate = startDate.AddDays((endDate - startDate).TotalDays / 2);
            apprenticeship.TrainingDetails.WithdrawalReasonCode = (existingWithdrawalReasonCode>30) ? 98 : 29;
        }

    }
}
