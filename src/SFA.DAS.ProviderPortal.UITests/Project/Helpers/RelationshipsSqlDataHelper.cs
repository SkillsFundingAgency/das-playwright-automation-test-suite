namespace SFA.DAS.ProviderPortal.UITests.Project.Helpers;

public class RelationshipsSqlDataHelper(ObjectContext objectContext, DbConfig config) : SqlDbHelper(objectContext, config.PermissionsDbConnectionString)
{
    public async Task<int> DeleteProviderRelation(string ukprn, string accountid, string empemail)
    {
        var sqlQuery = $"DECLARE @ukprn INT = {ukprn}, @accountid INT = {accountid}, @empemail NVARCHAR(255) = '{empemail}';" + FileHelper.GetSql("DeleteProviderRelation");

        return await ReTryExecuteSqlCommand(sqlQuery);
    }

    public async Task<int> DeleteProviderRequest(List<string> requestId)
    {
        var requestIdquery = string.Join(',', requestId.ToHashSet().Select(x => $"'{x}'"));

        return await ReTryExecuteSqlCommand($"delete from notifications where RequestId in ({requestIdquery}); delete from PermissionRequests where RequestId in ({requestIdquery}); delete from Requests where id in ({requestIdquery});");
    }

    public async Task<(string requestId, string requestStatus)> GetRequestId(string query)
    {
        var data = await GetData(query);

        return (data[0], data[1]);
    }
}
