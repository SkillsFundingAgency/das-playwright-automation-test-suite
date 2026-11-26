using System;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Azure.StackExchangeRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.Framework;
using SFA.DAS.Framework.Hooks;
using StackExchange.Redis;

namespace SFA.DAS.DigiCerts.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    private static ServiceProvider _serviceProvider;

    [BeforeScenario(Order = 22)]
    public async Task Navigate() => await Navigate(UrlConfig.DigiCerts_BaseUrl);

    [BeforeScenario(Order = 23)]
    public async Task SetUpRedisCache()
    {
        var configSection = context.Get<ConfigSection>();

        var config = configSection.GetConfigSection<string>("DefaultSessionRedisConnectionString");

        var services = new ServiceCollection();

        var redisConfiguration = ConfigurationOptions.Parse(config);

        redisConfiguration.AbortOnConnectFail = true;

        if (string.IsNullOrWhiteSpace(redisConfiguration.Password))
        {
            // pipeline connections include a Redis access key. If no password is
            // present, assume local development and authenticate using the developer
            // Azure CLI credentials set in 'DAS REDIS CACHE CON'
            await redisConfiguration.ConfigureForAzureWithTokenCredentialAsync(
                new AzureCliCredential());
        }

        services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = redisConfiguration;
        });

        _serviceProvider = services.BuildServiceProvider();

        var distributedCache =
            _serviceProvider.GetRequiredService<IDistributedCache>();

        context.Set(distributedCache);
    }

    [AfterScenario]
    public static async Task DisposeRedisAsync()
    {
        if (_serviceProvider is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync();
        }
    }
}