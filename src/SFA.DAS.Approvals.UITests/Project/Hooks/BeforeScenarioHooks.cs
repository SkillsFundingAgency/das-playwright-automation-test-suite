using SFA.DAS.API.Framework.Configs;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Approvals.UITests.Project;
using SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Hooks
{
    [Binding]
    public class BeforeScenarioHooks(ScenarioContext context)
    {
        private readonly ObjectContext _objectcontext = context.Get<ObjectContext>();
        private readonly DbConfig _dbConfig = context.Get<DbConfig>();
        private readonly string[] _tags = context.ScenarioInfo.Tags;

        [BeforeScenario(Order = 31)]
        public void SetUpDbHelpers()
        {
            context.Set(new AccountsDbSqlHelper(_objectcontext, _dbConfig));

            context.Set(new CommitmentsDbSqlHelper(_objectcontext, _dbConfig));

            context.Set(new LearningDbSqlHelper(_objectcontext, _dbConfig));

            context.Set(new LearnerDataDbSqlHelper(_objectcontext, _dbConfig));

        }

              
        [BeforeScenario(Order = 30)]
        public void SetUpDependencyConfig()
        {
            //Get api cnnfig from approvals:
            var subscriptionKey = context.GetOuterApiAuthTokenConfig<OuterApiAuthTokenConfig>();

            //Set config for 'Outer_ApiAuthTokenConfig' in the context:
            Outer_ApiAuthTokenConfig outer_ApiAuthTokenConfig = new Outer_ApiAuthTokenConfig();
            outer_ApiAuthTokenConfig.Apim_SubscriptionKey = subscriptionKey.Apim_SubscriptionKey;
            context.Set(outer_ApiAuthTokenConfig);

        }


    }
}
