using SFA.DAS.Framework;
using SFA.DAS.ProviderLogin.Service.Project;
using System.Threading.Tasks;

namespace SFA.DAS.ManagingStandards.UITests.Project.Hooks
{
    [Binding]
    public class ManagingStandardsHooks(ScenarioContext context)
    {
        private readonly string[] _tags = context.ScenarioInfo.Tags;
        private ManagingStandardsSqlDataHelper _managingStandardsSqlDataHelper;
        protected readonly DbConfig _dbConfig = context.Get<DbConfig>();
        private readonly ProviderConfig _config = context.GetProviderConfig<ProviderConfig>();
        private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

        [BeforeScenario(Order = 31)]
        public void SetUpDataHelpers()
        {
            context.Set(_managingStandardsSqlDataHelper = new ManagingStandardsSqlDataHelper(_objectContext, _dbConfig));

            context.Set(new ManagingStandardsDataHelpers());
        }

        [BeforeScenario(Order = 32)]
        public async Task SetApprovedByRegulatorToNull()
        {
            if (_tags.Any(x => x == "managingstandards02")) await _managingStandardsSqlDataHelper.ClearRegulation(_config.Ukprn, ManagingStandardsDataHelpers.StandardsTestData.LarsCode);
        }

        [BeforeScenario(Order = 33)]
        public async Task ClearDownProviderCourseLocationData()
        {
            if (_tags.Any(x => x == "managingstandards03")) await _managingStandardsSqlDataHelper.AddSingleProviderCourseLocation(_config.Ukprn, ManagingStandardsDataHelpers.StandardsTestData.LarsCode);
        }

        [BeforeScenario(Order = 34)]
        public async Task ClearDownProviderLocationData()
        {
            if (_tags.Any(x => x == "managingstandards04")) await _managingStandardsSqlDataHelper.ClearProviderLocation(_config.Ukprn, ManagingStandardsDataHelpers.StandardsTestData.Venue);
        }
    }
}
