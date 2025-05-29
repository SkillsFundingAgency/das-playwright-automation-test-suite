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
        public async Task GivenProviderSuccessfullySubmitsILRRecordContainingALearnerRecordForAEmployer(int NoOfApprentices, string type)
        {
            EmployerType employerType;
            
            switch (type.ToLower())
            {
                case "levy":
                    employerType = EmployerType.Levy;
                    break;
                case "nonlevy":
                    employerType = EmployerType.NonLevy;
                    break;
                case "nonlevyuseratmaxreservationlimit":
                    employerType = EmployerType.NonLevyUserAtMaxReservationLimit;
                    break;
                default:
                    throw new ArgumentException($"Unknown employer type: {type}");
            }
            
            var apprenticeship = new ApprenticeDataHelper(context).CreateNewApprenticeshipDetails(10000028, employerType);

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
