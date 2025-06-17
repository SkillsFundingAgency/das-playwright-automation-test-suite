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
        //private CommitmentsSqlDataHelper commitmentsdatahelper;
        //private RoatpV2SqlDataHelper roatpV2SqlDataHelper;
        private readonly DbConfig _dbConfig = context.Get<DbConfig>();
        private readonly string[] _tags = context.ScenarioInfo.Tags;

        [BeforeScenario(Order = 31)]
        public void SetUpDbHelpers()
        {
            // commitmentsdatahelper = new CommitmentsSqlDataHelper(_objectcontext, _dbConfig);

            //context.Set(commitmentsdatahelper);

            //context.Set(new RofjaaDbSqlHelper(_objectcontext, _dbConfig));

            context.Set(new AccountsDbSqlHelper(_objectcontext, _dbConfig));

            //context.Set(roatpV2SqlDataHelper = new RoatpV2SqlDataHelper(_objectcontext, _dbConfig));

            //context.Set(new PublicSectorReportingDataHelper());

            //context.Set(new PublicSectorReportingSqlDataHelper(_objectcontext, _dbConfig));

            //context.Set(new ManageFundingEmployerStepsHelper(context));
        }

        [BeforeScenario(Order = 32)]
        public void SetUpHelpers()
        {
            //if (new TestDataSetUpConfigurationHelper(context).NoNeedToSetUpConfiguration()) return;

            //var apprenticeStatus = _tags.Contains("liveapprentice") ? ApprenticeStatus.Live :
            //                       _tags.Contains("onemonthbeforecurrentacademicyearstartdate") ? ApprenticeStatus.OneMonthBeforeCurrentAcademicYearStartDate :
            //                       _tags.Contains("currentacademicyearstartdate") ? ApprenticeStatus.CurrentAcademicYearStartDate :
            //                       _tags.Contains("waitingtostartapprentice") ? ApprenticeStatus.WaitingToStart :
            //                       _tags.Contains("non-levy") ? ApprenticeStatus.StartedInCurrentMonth :
            //                       _tags.Contains("startdateisfewmonthsbeforenow") ? ApprenticeStatus.StartDateIsFewMonthsBeforeNow : ApprenticeStatus.Random;

            //List<(ApprenticeDataHelper apprenticeDataHelper, ApprenticeCourseDataHelper apprenticeCourseDataHelper)> listOfApprentices = SetProviderSpecificCourse(apprenticeStatus);

            //context.Set<List<(ApprenticeDataHelper, ApprenticeCourseDataHelper)>>(listOfApprentices);

            //var apprenticeDataHelper = listOfApprentices.FirstOrDefault().apprenticeDataHelper;

            //context.Set(apprenticeDataHelper);

            //context.Set(apprenticeDataHelper.apprenticePPIDataHelper);

            //context.Set(new EditedApprenticeDataHelper(apprenticeDataHelper));

            //var apprenticeCourseDataHelper = listOfApprentices.FirstOrDefault().apprenticeCourseDataHelper;

            //context.Set(apprenticeCourseDataHelper);

            //context.Set(new DataLockSqlHelper(_objectcontext, _dbConfig, apprenticeDataHelper, apprenticeCourseDataHelper));

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
