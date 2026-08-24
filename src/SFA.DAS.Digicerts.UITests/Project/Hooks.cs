using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.Framework;
using SFA.DAS.Framework.Hooks;
using System.Threading.Tasks;

namespace SFA.DAS.DigiCerts.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    private static ServiceProvider _serviceProvider;

    [BeforeTestRun]
    public static void ConfigureRedis()
    {
        var configSection = new ConfigSection(Configurator.GetConfig());

        var config = configSection.GetConfigSection<DigiCertConfig>();

        var services = new ServiceCollection();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = config.RedisConnectionString;
        });

        _serviceProvider = services.BuildServiceProvider();
    }

    [BeforeScenario(Order = 4)]
    public async Task SetUp()
    {
        var distributedCache =
            _serviceProvider.GetRequiredService<IDistributedCache>();

        context.Set(distributedCache);

        await Navigate(UrlConfig.DigiCerts_BaseUrl);
    }

    [AfterTestRun]
    public static async Task DisposeRedis()
    {
        if (_serviceProvider is not null)
        {
            await _serviceProvider.DisposeAsync();
        }
    }
}