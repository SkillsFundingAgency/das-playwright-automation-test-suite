using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers
{
    internal class LearnerDataDbSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.LearnerDataDbConnectionString)
    {
        private readonly DbConfig _dbConfig = dbConfig;

        internal async Task<string> GetLearnerDataId(long ULN)
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
            var additionalWhereFilter = "AND ApprenticeshipId is null";
            return await GetLearnerReadyToAdd(apprenticeship, additionalWhereFilter);
        }

        internal async Task<Apprenticeship> GetLearnerDetailsFromLearnerDataId(Apprenticeship apprenticeship)
        {
            int learnerDataId = apprenticeship.ApprenticeDetails.LearnerDataId;
            var additionalWhereFilter = $"AND Id = {learnerDataId}";
            return await GetLearnerReadyToAdd(apprenticeship, additionalWhereFilter);
        }
        private async Task<Apprenticeship> GetLearnerReadyToAdd(Apprenticeship apprenticeship, string additionalWhereFilter = null)
        {
            string query = @$"SELECT Id, ULN, UKPRN, Firstname, Lastname, Email, Dob, AcademicYear, StartDate, PlannedEndDate, 
                                PercentageLearningToBeDelivered, EpaoPrice, TrainingPrice, StandardCode, 
                                IsFlexiJob, PlannedOTJTrainingHours, ConsumerReference, ApprenticeshipId
                            FROM LearnerData
                            WHERE UKPRN = {apprenticeship.ProviderDetails.Ukprn}                             
                            {additionalWhereFilter}";
            
            var details = await GetData(query);

            apprenticeship.ApprenticeDetails.LearnerDataId = Convert.ToInt32(details[0]);
            apprenticeship.ApprenticeDetails.ULN = long.Parse(details[1]);
            apprenticeship.ProviderDetails.Ukprn = Convert.ToInt32(details[2]);
            apprenticeship.ApprenticeDetails.FirstName ??= details[3].ToString();
            apprenticeship.ApprenticeDetails.LastName ??= details[4].ToString();
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

            var courseTitle = await new CoursesDataHelper().GetCourse(apprenticeship.TrainingDetails.StandardCode);
            apprenticeship.TrainingDetails.CourseTitle = courseTitle.Title;

            return apprenticeship;
        }



    }

}
