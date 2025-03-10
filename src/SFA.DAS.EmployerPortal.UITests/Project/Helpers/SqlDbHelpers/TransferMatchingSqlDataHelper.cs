namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers.SqlDbHelpers;

public class TransferMatchingSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.TMDbConnectionString)
{
    public async Task<int> GetNumberTransferPledgeApplicationsToReview(string employerAccountId)
    {
        Dictionary<string, string> sqlParameters = new()
        {
            { "@EmployerAccountId", employerAccountId }
        };

        string query = @"select COUNT(app.Id) from [dbo].[Application] app
                                inner join [dbo].[Pledge] pledge
                                on pledge.Id = app.PledgeId
                                where app.Status = 0
                                and pledge.EmployerAccountId = @EmployerAccountId;";

        var result = await GetDataAsObject(query, sqlParameters);

        return (int)result;
    }

}
