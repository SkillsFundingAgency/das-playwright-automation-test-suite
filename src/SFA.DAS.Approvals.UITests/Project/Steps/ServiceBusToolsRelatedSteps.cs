using Azure;
using SFA.DAS.API.Framework.Configs;
using SFA.DAS.Approvals.UITests.Project.Events;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.API;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.LearnerData.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

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
        public async Task WhenLearningWithdrawnEventIsReceivedForTheApprentice()
        {
            var apprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var apprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId;
            var learningWithdrawnEvent 
                = new LearningWithdrawnEvent
                {
                    LearningKey = Guid.NewGuid(),
                    ApprenticeshipId = apprenticeshipId,
                    Created = DateTime.Now.ToString("yyyy-MM-dd"),
                    WithdrawalDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    withdrawalReasonCode = 29
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


    }
}
