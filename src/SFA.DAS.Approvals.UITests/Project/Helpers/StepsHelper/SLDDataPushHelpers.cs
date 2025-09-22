using Mailosaur.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Polly;
using SFA.DAS.Approvals.APITests.Project;
using SFA.DAS.Approvals.APITests.Project.Tests.StepDefinitions;
using SFA.DAS.Approvals.UITests.Project.Helpers.API;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SpecFlow.Internal.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper
{
    public class SLDDataPushHelpers
    {
        private readonly ScenarioContext _context;
        private readonly ApprovalsAPISteps _approvalsAPISteps;
        private readonly LearnerDataOuterApiHelper _learnerDataOuterApiHelper;

        public SLDDataPushHelpers(ScenarioContext context)
        {
            _context = context;
            _approvalsAPISteps = new ApprovalsAPISteps(_context);
            _learnerDataOuterApiHelper = context.Get<LearnerDataOuterApiHelper>();
        }

        public async Task PushDataToAS(List<LearnerDataAPIDataModel> learnersData, int academicYear)
        {
            var resource = $"/provider/{learnersData.First().ukprn}/academicyears/{academicYear}/learners";
            var payload = JsonHelper.Serialize(learnersData).ToString();

            //await _approvalsAPISteps.SLDPushDataToAS(resource, payload);
            await _learnerDataOuterApiHelper.PostNewLearners(resource, payload);
        }


        public async Task<List<LearnerDataAPIDataModel>> ConvertToLearnerDataAPIDataModel(List<Apprenticeship> listOfApprenticeships)
        {
            List<LearnerDataAPIDataModel> listOfLearnerData = new List<LearnerDataAPIDataModel>();
            await Task.Delay(100);

            foreach (var apprenticeship in listOfApprenticeships)
            {
                listOfLearnerData.Add(await ConvertToLearnerDataAPIDataModel(apprenticeship));
            }

            return listOfLearnerData;
        }


        public async Task<LearnerDataAPIDataModel> ConvertToLearnerDataAPIDataModel(Apprenticeship apprenticeship)
        {
            LearnerDataAPIDataModel learnerData = new LearnerDataAPIDataModel();

            learnerData.ukprn = apprenticeship.UKPRN.ToString();
            learnerData.uln = apprenticeship.ApprenticeDetails.ULN;
            learnerData.firstname = apprenticeship.ApprenticeDetails.FirstName;
            learnerData.lastname = apprenticeship.ApprenticeDetails.LastName;
            learnerData.learnerEmail = apprenticeship.ApprenticeDetails.Email;
            learnerData.dateOfBirth = apprenticeship.ApprenticeDetails.DateOfBirth.ToString("yyyy-MM-dd");
            learnerData.startDate = apprenticeship.TrainingDetails.StartDate.ToString("yyyy-MM-dd");
            learnerData.plannedEndDate = apprenticeship.TrainingDetails.EndDate.ToString("yyyy-MM-dd");
            learnerData.percentageLearningToBeDelivered = apprenticeship.TrainingDetails.PercentageLearningToBeDelivered;
            learnerData.epaoPrice = apprenticeship.TrainingDetails.EpaoPrice;
            learnerData.trainingPrice = apprenticeship.TrainingDetails.TrainingPrice;
            learnerData.agreementId = apprenticeship.EmployerDetails.AgreementId;
            learnerData.isFlexiJob = apprenticeship.TrainingDetails.IsFlexiJob;
            learnerData.plannedOTJTrainingHours = apprenticeship.TrainingDetails.PlannedOTJTrainingHours;
            learnerData.standardCode = apprenticeship.TrainingDetails.StandardCode;
            learnerData.consumerReference = apprenticeship.TrainingDetails.ConsumerReference;

            await Task.Delay(100); 
            return learnerData;

        }


        internal async Task CheckApprenticeIsAvailableInApprovedLearnersList(Apprenticeship apprenticeship)
        {
            var resource = $"/Learners/providers/{apprenticeship.UKPRN}/academicyears/{apprenticeship.TrainingDetails.AcademicYear}/learners";
            var result = await _learnerDataOuterApiHelper.GetLearners(resource, apprenticeship.ApprenticeDetails.ULN);
            string content = result.Content;
        }


    }

    public class LearnerDataAPIDataModel
    {
        public string uln { get; set; }
        public string ukprn { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string learnerEmail { get; set; }
        public string dateOfBirth { get; set; }
        public string startDate { get; set; }
        public string plannedEndDate { get; set; }
        public int percentageLearningToBeDelivered { get; set; }
        public int epaoPrice { get; set; }
        public int trainingPrice { get; set; }
        public string agreementId { get; set; }
        public bool isFlexiJob { get; set; }
        public int plannedOTJTrainingHours { get; set; }
        public int standardCode { get; set; }
        public string consumerReference { get; set; }


    }



}
