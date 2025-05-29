using SFA.DAS.Approvals.APITests.Project.Tests.StepDefinitions;
using SFA.DAS.Approvals.UITests.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Helpers.SqlHelpers;
using SFA.DAS.Approvals.UITests.Helpers.StepsHelper;
using SpecFlow.Internal.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.Approvals.UITests.Helpers.SqlHelpers;

namespace SFA.DAS.Approvals.UITests.Project.Tests.Steps
{
    [Binding]
    public class SldIlrSubmissionSteps(ScenarioContext context)
    {
        private readonly SLDDataPushHelpers sldDataPushHelpers = new(context);

        [Given("Provider successfully submits (\\d+) ILR record containing a learner record for a \"(.*)\" Employer")]
        public async Task GivenProviderSuccessfullySubmitsILRRecordContainingALearnerRecordForAEmployer(int NoOfApprentices, string employerType)
        {
            var apprenticeship = new ApprenticeDataHelper(context).CreateNewApprenticeshipDetails(10000028, EmployerType.Levy);

            context.Set(apprenticeship);
        }



        [Given("SLD push its data into AS")]
        public async Task GivenSLDPushItsDataIntoAS()
        {
            var listOfLearnerDataList = new List<LearnerDataAPIDataModel> { sldDataPushHelpers.ConvertToLearnerDataAPIDataModel(context.GetValue<Apprenticeship>()) };

            await sldDataPushHelpers.PushDataToAS(listOfLearnerDataList);
        }







  

    }
}
