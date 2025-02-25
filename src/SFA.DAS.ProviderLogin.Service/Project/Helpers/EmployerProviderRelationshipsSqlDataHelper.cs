namespace SFA.DAS.ProviderLogin.Service.Project.Helpers;

internal class EmployerProviderRelationshipsSqlDataHelper(ObjectContext objectContext, DbConfig config) : SqlDbHelper(objectContext, config.PermissionsDbConnectionString)
{
    public async Task<List<ProviderDetails>> GetProviderName(List<int> ukprn)
    {
        var query = ukprn.Select(GetSqlQuery).ToList();

        var multiresult = await GetListOfMultipleData(query);

        var dfeProviderDetailsList = new FrameworkList<ProviderDetails>();

        foreach (var result in multiresult)
        {
            dfeProviderDetailsList.Add(new ProviderDetails($"{result[0][0]}", $"{result[0][1]}"));
        }

        return dfeProviderDetailsList;
    }

    private static string GetSqlQuery(int ukprn) => $"select Ukprn, [Name] from  Providers WHERE Ukprn = {ukprn}";
}
