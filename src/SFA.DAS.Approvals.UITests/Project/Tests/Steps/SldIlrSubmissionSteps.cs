using SFA.DAS.Approvals.APITests.Project.Tests.StepDefinitions;
using SFA.DAS.Approvals.UITests.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Helpers.StepsHelper;
using SpecFlow.Internal.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Tests.Steps
{
    [Binding]
    public class SldIlrSubmissionSteps(ScenarioContext context)
    {
        private readonly SLDDataPushHelpers sldDataPushHelpers = new(context);


        [Given("Provider submit ILR successfully for a new apprentice")]
        public async Task GivenProviderSubmitILRSuccessfullyForANewApprentice()
        {
            var apprenticeship = new ApprenticeDataHelper().CreateNewApprenticeshipDetails(10000028, "AG12345678");

            var listOfLearnerDataList = new List<LearnerDataAPIDataModel> { sldDataPushHelpers.ConvertToLearnerDataAPIDataModel(apprenticeship) };

            await sldDataPushHelpers.PushDataToAS(listOfLearnerDataList);
        }
    }
}
