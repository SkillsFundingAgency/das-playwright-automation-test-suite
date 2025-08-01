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

        internal async Task<List<string>> GetEditableApprenticeDetails(int ukprn, int accountLegalEntityId)
        {
            string query =
                @$"SELECT TOP(1) a.ULN, a.FirstName, a.LastName, a.DateOfBirth
                FROM [dbo].[Commitment] c
                INNER JOIN [dbo].[Apprenticeship] a
                ON c.id = a.CommitmentId
                Where ProviderId = {ukprn}
                AND c.CreatedOn > DATEADD(month, -12, GETDATE())
                AND c.AccountLegalEntityId = {accountLegalEntityId}
                AND c.IsDeleted = 0
                And c.Approvals = 3
                AND c.ChangeOfPartyRequestId is null
                AND a.TrainingCode IN ('803','804','805','806','807','808','809', '810', '811')                
                And c.ChangeOfPartyRequestId is null
                AND c.PledgeApplicationId is null
                AND a.PaymentStatus = 1
                AND a.HasHadDataLockSuccess = 0
                AND a.PendingUpdateOriginator is null
                AND a.CloneOf is null
                AND a.ContinuationOfId is null
                AND a.DeliveryModel = 0
                AND a.IsOnFlexiPaymentPilot = 0
                Order by c.CreatedOn DESC";            
            
            return await GetData(query);            
        }

    }
}
