using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using System.Collections.Generic;

namespace SFA.DAS.EmployerAccounts.APITests.Project.Helpers.SqlDbHelpers
{
    public class EmployerAccountsSqlDbHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.AccountsDbConnectionString)
    {
       public async Task SetHashedAccountId()
        {
            var hashedAccountId = await GetDataAsString($"SELECT TOP (1) a.HashedId" +
                $" FROM [employer_account].[Paye] paye" +
                $" INNER JOIN [employer_account].[AccountHistory] ah ON ah.PayeRef = paye.Ref" +
                $" INNER JOIN [employer_account].[Account] a ON a.Id = ah.AccountId" +
                $" WHERE paye.Ref IS NOT NULL AND a.[ApprenticeshipEmployerType] = 1 ORDER BY NEWID()");
            objectContext.SetHashedAccountId(hashedAccountId);
        }

        public async Task SetAccountId()
        {
            var accountId = await GetDataAsString($"SELECT TOP (1) Id" +
                $" FROM [employer_account].[Account] a" +
                $" WHERE a.HashedId = '{objectContext.GetHashedAccountId()}' ORDER BY Id DESC");
            objectContext.SetAccountId(accountId);
        }

        public async Task SetLegalEntityId()
        {
            var legalEntityId = await GetDataAsString($"SELECT TOP (1) LegalEntityId" +
                $" FROM [employer_account].[AccountLegalEntity] ale" +
                $" INNER JOIN [employer_account].[Account] a on ale.AccountId = a.Id" +
                $" WHERE a.HashedId = '{objectContext.GetHashedAccountId()}'");
            
            objectContext.SetLegalEntityId(legalEntityId);
        }

        public async Task SetPayeSchemeRef()
        {
            var payeScheme = await GetDataAsString($"SELECT TOP 1 paye.Ref" +
                $" FROM [employer_account].[Paye] paye" +
                $" INNER JOIN [employer_account].[AccountHistory] ah ON ah.PayeRef = paye.Ref" +
                $" INNER JOIN [employer_account].[Account] a ON a.Id = ah.AccountId" +
                $" WHERE a.HashedId = '{objectContext.GetHashedAccountId()}'");
            objectContext.SetPayeSchemeRef(payeScheme);
        }

        public async Task<List<object[]>> GetAgreementInfo()
        {
            var agreementId = await GetListOfData(
                $"	SELECT ale.PublicHashedId as AccountLegalEntityPublicHashedId, ea.Id as EmployerAgreementId" +
                $"  FROM [employer_account].[EmployerAgreement] ea" +
                $"  JOIN [employer_account].[AccountLegalEntity] ale" +
                $"  ON ale.Id = ea.AccountLegalEntityId" +
                $"  AND ale.Deleted IS NULL" +
                $"  JOIN [employer_account].[LegalEntity] le ON le.Id = ale.LegalEntityId" +
                $"  JOIN [employer_account].[EmployerAgreementTemplate] eat ON eat.Id = ea.TemplateId" +
                $"  JOIN [employer_account].[Account] a on a.Id = ale.AccountId" +
                $"  WHERE a.HashedId = '{objectContext.GetHashedAccountId()}' AND ea.ExpiredDate IS NULL");

            return agreementId;
        }

        public async Task<string> GetUserRef()
        {
            return await GetDataAsString("SELECT TOP 1 UserRef" +
                " FROM [employer_account].[User] ORDER BY Id DESC");
        }

        public async Task<string> GetUserEmail()
        {
            return await GetDataAsString("SELECT TOP 1 Email" +
                " FROM [employer_account].[User] WHERE email NOT LIKE '%+%' ORDER BY Id DESC");
        }
    }
}
