using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.LearnerData.Events;
using SFA.DAS.Payments.ProviderPayments.Messages;
using System;
using System.Text.Json;


namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class ServiceBusEventsRelatedSteps
    {
        private readonly ScenarioContext context;
        private readonly ObjectContext objectContext;

        public ServiceBusEventsRelatedSteps(ScenarioContext _context)
        {
            context = _context;
            objectContext = context.Get<ObjectContext>();
        }


        [When("When payments completion event is received for the apprentice")]
        public async Task WhenWhenPaymentsCompletionEventIsReceivedForTheApprentice()
        {
            var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var pymtCompletionEvent = new RecordedAct1CompletionPayment { ApprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId, EventTime = DateTimeOffset.UtcNow };
            var serviceBusHelper = GlobalTestContext.ServiceBus;
            await serviceBusHelper.Publish(pymtCompletionEvent);

            objectContext.SetDebugInformation($"Publishing Payment Completion Event (RecordedAct1CompletionPayment) to N-Service Bus for following apprentice:");
            objectContext.SetDebugInformation(JsonSerializer.Serialize(pymtCompletionEvent, new JsonSerializerOptions { WriteIndented = true }));
        }




    }
}
