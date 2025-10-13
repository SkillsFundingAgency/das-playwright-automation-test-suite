using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers
{
    internal class CommitmentsDbSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.CommitmentsDbConnectionString)
    {
        private readonly DbConfig _dbConfig = dbConfig;

        internal async Task UpdateCohortLastUpdatedDate(string cohortRef, DateTime lastUpdatedDate)
        {
            string query = $"UPDATE Commitment SET LastUpdatedOn = '{lastUpdatedDate.ToString("yyyy-MM-dd")}' WHERE Reference = '{cohortRef}'";
            await ExecuteSqlCommand(query);
        }

        internal async Task<string> GetWithPartyValueFromCommitmentsDb(string cohortRef)
        {
            string query = $"SELECT WithParty FROM Commitment WHERE Reference = '{cohortRef}'";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;
        }

        internal async Task<Apprenticeship> GetApprenticeDetailsFromCommitmentsDb(Apprenticeship apprenticeship, string additionalWhereFilter = null)
        {
            var details = await GetApprenticeDetails(apprenticeship.ProviderDetails.Ukprn, apprenticeship.EmployerDetails.AccountLegalEntityId, additionalWhereFilter);

            apprenticeship.ApprenticeDetails.ULN = details[0].ToString();
            apprenticeship.ApprenticeDetails.FirstName = details[1].ToString();
            apprenticeship.ApprenticeDetails.LastName = details[2].ToString();
            apprenticeship.ApprenticeDetails.DateOfBirth = Convert.ToDateTime(details[3].ToString());
            apprenticeship.TrainingDetails.StandardCode = Convert.ToInt32(details[4]);
            apprenticeship.ReservationID = details[5];
            apprenticeship.Cohort.Reference = details[6];
            apprenticeship.ApprenticeDetails.Email = details[7];
            apprenticeship.TrainingDetails.StartDate = Convert.ToDateTime(details[8]);
            apprenticeship.TrainingDetails.EndDate = Convert.ToDateTime(details[9]);
            apprenticeship.TrainingDetails.TotalPrice = Convert.ToInt32(details[10]);
            apprenticeship.TrainingDetails.TrainingPrice = Convert.ToInt32(details[10]);
            apprenticeship.TrainingDetails.AcademicYear = AcademicYearDatesHelper.GetCurrentAcademicYear();
            apprenticeship.TrainingDetails.ConsumerReference = details[11];

            return apprenticeship;
        }

        private async Task<List<string>> GetApprenticeDetails(int ukprn, int accountLegalEntityId, string additionalWhereFilter = null )
        {
            string query =
                @$"SELECT TOP(1) a.ULN, a.FirstName, a.LastName, a.DateOfBirth, a.TrainingCode, a.ReservationId, c.Reference, a.Email, a.StartDate, a.EndDate, a.Cost, a.ProviderRef
                    FROM [dbo].[Commitment] c
                    INNER JOIN [dbo].[Apprenticeship] a
                    ON c.id = a.CommitmentId
                    Where ProviderId = {ukprn}                
                    AND c.AccountLegalEntityId = {accountLegalEntityId}
                    {additionalWhereFilter}
                    Order by c.CreatedOn DESC";

            return await GetData(query);            
        }

        internal async Task<string> GetValueFromApprenticeshipTable(string columnName, string ULN)
        {
            string query = $"SELECT TOP(1) {columnName} FROM [dbo].[Apprenticeship] WHERE ULN = {ULN}";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;

        }
    }
}
