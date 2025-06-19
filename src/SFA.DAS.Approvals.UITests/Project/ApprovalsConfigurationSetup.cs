using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project
{
    [Binding]
    public class ApprovalsConfigurationSetup(ScenarioContext context)
    {
        private readonly ConfigSection _configSection = context.Get<ConfigSection>();

        //[BeforeScenario(Order = 2)]
        //public void SetUpApprovalsConfiguration()
        //{
        //if (NoNeedToSetUpConfiguration()) return;

        //context.SetApprovalsConfig(_configSection.GetConfigSection<ApprovalsConfig>());

        //context.SetEasLoginUser(
        //[
        //    _configSection.GetConfigSection<ProviderPermissionLevyUser>(),
        //    _configSection.GetConfigSection<EmployerWithMultipleAccountsUser>(),
        //    _configSection.GetConfigSection<FlexiJobUser>(),
        //    _configSection.GetConfigSection<NonLevyUserAtMaxReservationLimit>(),
        //    _configSection.GetConfigSection<EmployerConnectedToPortableFlexiJobProvider>()
        //]);
        //}

        [BeforeScenario(Order = 2)]
        public void SetUpOuterApiAuthTokenConfiguration()
        {
            context.SetOuterApiAuthTokenConfig(_configSection.GetConfigSection<OuterApiAuthTokenConfig>());

        }

        //private bool NoNeedToSetUpConfiguration()
        //{
        //    if (context.ScenarioInfo.Tags.Contains("deletecohortviaemployerportal"))
        //    {
        //        context.SetEasLoginUser([_configSection.GetConfigSection<DeleteCohortLevyUser>()]);
        //    }

        //    return new TestDataSetUpConfigurationHelper(context).NoNeedToSetUpConfiguration();
        //}
    }
}
