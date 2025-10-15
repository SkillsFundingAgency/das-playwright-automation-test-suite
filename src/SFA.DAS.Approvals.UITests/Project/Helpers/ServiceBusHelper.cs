using NServiceBus;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Configuration.AzureServiceBus;
using SFA.DAS.NServiceBus.Configuration.NewtonsoftJsonSerializer;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Helpers
{
    public class ServiceBusHelper : IAsyncDisposable
    {
        private const string _endpointName = "sfa-das-approvals-ui-tests-queue";
        private IEndpointInstance _endpointInstance;
        public bool IsRunning { get; private set; }

        public async Task Start(string connectionString)
        {
            if (IsRunning) return;

            var endpointConfiguration = new EndpointConfiguration(_endpointName)
                .UseMessageConventions()
                .UseNewtonsoftJsonSerializer();

            endpointConfiguration.UseAzureServiceBusTransport(connectionString);

            _endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            IsRunning = true;
        }

        public async Task Stop()
        {
            if (!IsRunning) return;
            await _endpointInstance.Stop();
            IsRunning = false;
        }

        public async ValueTask DisposeAsync() => await Stop();

        public async Task Publish(object message) => await _endpointInstance.Publish(message);
    }
}
