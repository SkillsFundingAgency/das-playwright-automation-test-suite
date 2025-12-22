namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers.SqlDbHelpers;

public class TransferMatchingSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.TMDbConnectionString)
{
    public async Task<int> GetNumberTransferPledgeApplicationsByApplicationStatus(string employerAccountId, string applicationStatusId)
    {
        Dictionary<string, string> sqlParameters = new()
        {
            { "@EmployerAccountId", employerAccountId },
            { "@ApplicationStatus" , applicationStatusId }
        };

        string query = @"select COUNT(app.Id) from [dbo].[Application] app
                                inner join [dbo].[Pledge] pledge
                                on pledge.Id = app.PledgeId
                                where app.Status = @ApplicationStatus
                                and pledge.EmployerAccountId = @EmployerAccountId;";

        var result = await GetDataAsObject(query, sqlParameters);

        return (int)result;
    }
    public async Task<List<int>> GetTransferPledgeApplicationsByApplicationStatus(string employerAccountId, string applicationStatusId)
    {
        Dictionary<string, string> sqlParameters = new()
        {
            { "@EmployerAccountId", employerAccountId },
            { "@ApplicationStatus", applicationStatusId }
        };

        string query = @"
             SELECT Id 
                FROM [dbo].[Application] 
             WHERE [Status] = @ApplicationStatus
                AND EmployerAccountId = @EmployerAccountId;";

        List<string[]> rawResults = await GetMultipleData(query, sqlParameters);
        List<int> applicationIds = [];

        foreach (var row in rawResults)
        {
            if (row.Length > 0 && int.TryParse(row[0], out int id))
            {
                applicationIds.Add(id);
            }
        }

        return applicationIds;
    }
}
