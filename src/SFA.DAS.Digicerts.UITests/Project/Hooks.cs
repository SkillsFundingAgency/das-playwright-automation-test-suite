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

    [BeforeTestRun]
    public static async Task ConfigureRedisAsync()
    {
        var configSection = new ConfigSection(Configurator.GetConfig());
        var config = configSection.GetConfigSection<DigiCertConfig>();

        var services = new ServiceCollection();

        var redisConfiguration = ConfigurationOptions.Parse(
            config.RedisConnectionString);

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
    }

    [BeforeScenario()]
    public async Task SetUp()
    {
        var distributedCache =
            _serviceProvider.GetRequiredService<IDistributedCache>();

        context.Set(distributedCache);

        await Navigate(UrlConfig.DigiCerts_BaseUrl);
    }

    [AfterTestRun]
    public static async Task DisposeRedisAsync()
    {
        if (_serviceProvider is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync();
        }
    }
}