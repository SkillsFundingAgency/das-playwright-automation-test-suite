using System;


namespace SFA.DAS.Approvals.UITests.Helpers.SqlHelpers
{
    public class AccountsDbSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.AccountsDbConnectionString)
    {
        private readonly DbConfig _dbConfig = dbConfig;

        public async Task<string> GetAgreementId(string email, string name) => await ReadDataFromDataBase(FileHelper.GetSql("GetAgreementId"), new Dictionary<string, string> { { "@email", email }, { "@name", name } });

        public async Task<string> GetAgreementIdByCohortRef(string cohortRef) => await ReadDataFromCommtDataBase($"Select PublicHashedId from [AccountLegalEntities] ALE Inner Join Commitment C on C.AccountLegalEntityId = ALE.Id Where C.Reference = '{cohortRef}'");

        public async Task<string> GetEmployerNameByAgreementId(string agreementId) => await ReadDataFromCommtDataBase($"Select Ac.Name from AccountLegalEntities ale inner join Accounts Ac on ale.AccountId = ac.Id where ale.PublicHashedId = '{agreementId}'");

        public async Task<string> GetIsLevyByAgreementId(string agreementId) => await ReadDataFromCommtDataBase($"Select Ac.LevyStatus from AccountLegalEntities ale inner join Accounts Ac on ale.AccountId = ac.Id where ale.PublicHashedId = '{agreementId}'");

        public int GetEmployerAccountId(string email, string organisationName)
        {
            string query = @$"SELECT TOP 1 acc.Id FROM[employer_account].[Membership] msp 
                                INNER JOIN[employer_account].[User] usr
                                ON msp.UserId = usr.Id
                                INNER JOIN[employer_account].[Account] acc
                                ON acc.Id = msp.AccountId
                                WHERE usr.Email = '{email}'
                                AND Name Like '{organisationName}%'";

            return Convert.ToInt32(GetDataAsObject(query));
        }

        public string GetPublicHashedAccountIdByHashedId(string hashedId)
        {
            string query = @$"SELECT 
	                            PublicHashedId 
                            FROM 
	                            [employer_account].[Account]
                            where 
	                            HashedId = '{hashedId}';";

            return GetDataAsObject(query).ToString();
        }

        private async Task<string> ReadDataFromDataBase(string queryToExecute, Dictionary<string, string> parameters) => await ReadDataFromDataBase(queryToExecute, connectionString, parameters);

        private async Task<string> ReadDataFromCommtDataBase(string queryToExecute) => await ReadDataFromDataBase(queryToExecute, _dbConfig.CommitmentsDbConnectionString, null);

        private async Task<string> ReadDataFromDataBase(string queryToExecute, string connectionString, Dictionary<string, string> parameters)
        {
            var (data, _) = await GetListOfData(queryToExecute, connectionString, parameters);

            if (data.Count == 0)
                return null;
            else
                return data[0][0].ToString();
        }
    }
}
