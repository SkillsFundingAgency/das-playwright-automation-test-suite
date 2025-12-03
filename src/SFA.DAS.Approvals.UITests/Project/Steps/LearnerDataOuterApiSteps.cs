using Polly;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.API;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class LearnerDataOuterApiSteps
    {
        private readonly ScenarioContext context;
        private readonly LearnerDataOuterApiHelper learnerDataOuterApiHelper;
        private ApprenticeDataHelper apprenticeDataHelper;

        public LearnerDataOuterApiSteps(ScenarioContext _context)
        {
            context = _context;
            learnerDataOuterApiHelper = new LearnerDataOuterApiHelper(context);
            apprenticeDataHelper = new ApprenticeDataHelper(context);
        }

        [Given("Provider successfully submits (\\d+) ILR record containing a learner record for a \"(.*)\" Employer")]
        public async Task ProviderSubmitsAnILRRecord(int NoOfApprentices, string type)
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

            var listOfApprenticeship = await new ApprenticeDataHelper(context).CreateApprenticeshipAsync(employerType, NoOfApprentices);

            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);

            await SLDPushDataIntoAS(listOfApprenticeship);
        }

        public async Task SLDPushDataIntoAS(List<Apprenticeship> listOfApprenticeship = null)
        {
            listOfApprenticeship ??= context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            var ukprn = listOfApprenticeship.FirstOrDefault().ProviderDetails.Ukprn;

            foreach (var apprenticeship in listOfApprenticeship)
            {                
                await learnerDataOuterApiHelper.PushNewLearnersDataToAsViaNServiceBus(apprenticeship, ukprn);

                //var learnerData = await learnerDataOuterApiHelper.ConvertToLearnerDataAPIDataModel(apprenticeship);
                //await learnerDataOuterApiHelper.PushNewLearnersDataToASViaAPI(learnerData, ukprn);
            }
            
        }

        [Given("Provider adds an apprentice aged (.*) years using Foundation level standard")]
        public async Task GivenProviderAddsAnApprenticeAgedYearsUsingFoundationLevelStandard(int age)
        {
            var coursesDataHelper = new CoursesDataHelper();
            var employerType = EmployerType.Levy;

            //create apprenticeships object with Foundation level standard and a learner aged > 25 years:
            var foundationTrainingDetails = new TrainingFactory(coursesDataHelper => coursesDataHelper.GetRandomFoundationCourse());
            var apprenticeDetails = new ApprenticeFactory(age+1);

            var listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipAsync(employerType, 1, null, null, apprenticeFactory: apprenticeDetails, trainingFactory: foundationTrainingDetails);
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
            await SLDPushDataIntoAS();
        }
     
        [Given("new learner details are processed in ILR for (\\d+) apprentices")]
        [Given("the employer has (\\d+) apprentice ready to start training")]
        public async Task ProcessedLearnersInILR(int NoOfApprentices)
        {
            var employerType = context.ScenarioInfo.Title.ToLower().Contains("nonlevy") ? EmployerType.NonLevy : EmployerType.Levy;
            await ProviderSubmitsAnILRRecord(NoOfApprentices, employerType.ToString());
            await SLDPushDataIntoAS();
        }

        [Then(@"apprentice\/learner record is available on Learning endpoint for SLD \(so they do not resubmit it\)")]
        public async Task ThenApprenticeLearnerRecordIsAvailableOnLearningEndpointForSLDSoTheyDoNotResubmitIt()
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var academicYear = listOfApprenticeship.FirstOrDefault().TrainingDetails.AcademicYear;
            await learnerDataOuterApiHelper.CheckApprenticeIsAvailableInApprovedLearnersList(listOfApprenticeship.FirstOrDefault());
            
        }

        [When("Provider resubmits ILR file with changes to apprentice details")]
        public async Task WhenProviderResubmitsILRFileWithChangesToApprenticeDetails()
        {
            var updatedSuffix = "_UpdatedAt_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var listOfUpdatedApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).CloneApprenticeships();
            var lastName = listOfUpdatedApprenticeship.FirstOrDefault().ApprenticeDetails.LastName;
            listOfUpdatedApprenticeship.FirstOrDefault().ApprenticeDetails.LastName = lastName.Contains('_')
                                                                                        ? lastName[..lastName.IndexOf('_')] + updatedSuffix
                                                                                        : lastName + updatedSuffix;


            await SLDPushDataIntoAS(listOfUpdatedApprenticeship);
            context.Set<List<Apprenticeship>>(listOfUpdatedApprenticeship, ScenarioKeys.ListOfUpdatedApprenticeship);
        }


    }

}
