using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;

namespace SFA.DAS.Approvals.UITests.Project.Steps
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

            var listOfApprenticeship = await new ApprenticeDataHelper(context).CreateNewApprenticeshipDetails(employerType, NoOfApprentices, null);

            context.Set(listOfApprenticeship);
        }



        [Given("SLD push its data into AS")]
        public async Task GivenSLDPushItsDataIntoAS()
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>();

            var academicYear = listOfApprenticeship.FirstOrDefault().TrainingDetails.AcademicYear;

            var listOfLearnerDataList = await sldDataPushHelpers.ConvertToLearnerDataAPIDataModel(listOfApprenticeship);

            await sldDataPushHelpers.PushDataToAS(listOfLearnerDataList, academicYear);
        }



    }
}
