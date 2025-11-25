using System;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.Finance.APITests.Project;

namespace SFA.DAS.Finance.APITests.Project.Helpers.SqlDbHelpers
{
    public class EmployerAccountsSqlHelper : SqlDbHelper
    {
        public EmployerAccountsSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : base(objectContext, dbConfig.AccountsDbConnectionString)
        {
        }

        public string SetHashedAccountId(string accountId)
        {
            var query = $"SELECT HashedId FROM [employer_account].[Account] WHERE id = '{accountId}' ";
            var id = GetDataAsString(query).GetAwaiter().GetResult();

            // use extension on ObjectContext from project namespace
            objectContext.SetHashedAccountId(id);

            return id;
        }
    }
}