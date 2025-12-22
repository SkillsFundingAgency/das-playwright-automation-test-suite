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


        [Given("test123")]
        public async Task GivenTest123()
        {
            await PublishRecordedAct1CompletionPaymentEvent(204782);
        }

        public async Task PublishRecordedAct1CompletionPaymentEvent(int apprenticeshipId)
        {
            var pymtCompletionEvent = new RecordedAct1CompletionPayment { ApprenticeshipId = apprenticeshipId, EventTime = DateTimeOffset.UtcNow };
            var serviceBusHelper = GlobalTestContext.ServiceBus;
            await serviceBusHelper.Publish(pymtCompletionEvent);

            objectContext.SetDebugInformation($"Publishing Payment Completion Event (RecordedAct1CompletionPayment) to N-Service Bus for following apprentice:");
            objectContext.SetDebugInformation(JsonSerializer.Serialize(pymtCompletionEvent, new JsonSerializerOptions { WriteIndented = true }));
        }


    }
}
