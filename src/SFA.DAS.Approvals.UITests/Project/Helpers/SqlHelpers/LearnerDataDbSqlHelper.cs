using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers
{
    internal class LearnerDataDbSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.LearnerDataDbConnectionString)
    {
        private readonly DbConfig _dbConfig = dbConfig;

        internal async Task<string> GetLearnerDataId(string ULN)
        {          
            string query = $"SELECT Id FROM LearnerData WHERE ULN = '{ULN}'";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;
        }

        internal async Task<string> GetApprenticeshipIdLinkedWithLearnerData(int LearnerDataId)
        {
            string query = $"SELECT ApprenticeshipId FROM LearnerData WHERE Id = {LearnerDataId}";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;
        }

        internal async Task<Apprenticeship> GetEditableApprenticeDetails(Apprenticeship apprenticeship)
        {
            var details = await GetLearnerReadyToAdd(apprenticeship.ProviderDetails.Ukprn);

            apprenticeship.ApprenticeDetails.LearnerDataId = Convert.ToInt32(details[0]);
            apprenticeship.ApprenticeDetails.ULN = details[1].ToString();
            apprenticeship.ProviderDetails.Ukprn = Convert.ToInt32(details[2]);
            apprenticeship.ApprenticeDetails.FirstName = details[3].ToString();
            apprenticeship.ApprenticeDetails.LastName = details[4].ToString();
            apprenticeship.ApprenticeDetails.Email = details[5];
            apprenticeship.ApprenticeDetails.DateOfBirth = Convert.ToDateTime(details[6].ToString());
            apprenticeship.TrainingDetails.AcademicYear = Convert.ToInt32(details[7]);
            apprenticeship.TrainingDetails.StartDate = Convert.ToDateTime(details[8]);
            apprenticeship.TrainingDetails.EndDate = Convert.ToDateTime(details[9]);
            apprenticeship.TrainingDetails.PercentageLearningToBeDelivered = Convert.ToInt32(details[10]);
            apprenticeship.TrainingDetails.EpaoPrice = Convert.ToInt32(details[11]);
            apprenticeship.TrainingDetails.TrainingPrice = Convert.ToInt32(details[12]);
            apprenticeship.TrainingDetails.TotalPrice = apprenticeship.TrainingDetails.EpaoPrice + apprenticeship.TrainingDetails.TrainingPrice;
            apprenticeship.TrainingDetails.StandardCode = Convert.ToInt32(details[13]);
            apprenticeship.TrainingDetails.IsFlexiJob = Convert.ToBoolean(details[14]);
            apprenticeship.TrainingDetails.PlannedOTJTrainingHours = Convert.ToInt32(details[15]);
            apprenticeship.TrainingDetails.ConsumerReference = details[16];

            return apprenticeship;
        }
        private async Task<List<string>> GetLearnerReadyToAdd(int Ukprn)
        {
            string query = @$"SELECT Id, ULN, UKPRN, Firstname, Lastname, Email, Dob, AcademicYear, StartDate, PlannedEndDate, 
                                PercentageLearningToBeDelivered, EpaoPrice, TrainingPrice, StandardCode, 
                                IsFlexiJob, PlannedOTJTrainingHours, ConsumerReference, ApprenticeshipId
                            FROM LearnerData
                            WHERE UKPRN = 10022856 
                            AND ApprenticeshipId is null";
            return await GetData(query);
        }



    }

}
