namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers.SqlDbHelpers;

public class RegistrationSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.AccountsDbConnectionString)
{
    public async Task<string> GetAccountApprenticeshipEmployerType(string email) => await GetDataAsString($"SELECT ApprenticeshipEmployerType FROM [employer_account].[Account] WHERE Id IN {GetAccountIdQuery(email)} ORDER BY CreatedDate");

    public async Task<int> UpdateLegalEntityName(string email) => await ExecuteSqlCommand($"UPDATE [employer_account].[AccountLegalEntity] SET [Name] = 'Changed Org Name' WHERE AccountId IN {GetAccountIdQuery(email)}");

    public async Task<string> GetAccountLegalEntityPublicHashedId(string accountid, string orgname) => await GetDataAsString($"select PublicHashedId from [employer_account].[AccountLegalEntity] where AccountId = {accountid} and [Name] = '{orgname}'");

    public async Task<string> GetAccountLegalEntityId(string accountid, string orgname) => await GetDataAsString($"select Id from [employer_account].[AccountLegalEntity] where AccountId = {accountid} and [Name] = '{orgname}'");

    private static string GetAccountIdQuery(string email) => $"(SELECT AccountId FROM [employer_account].[Membership] m JOIN [employer_account].[User] u ON u.Id = m.UserId AND u.Email = '{email}')";
}
