

namespace SFA.DAS.RAA.Service.Project.Helpers;

public class RAAProviderPermissionsSqlDbHelper(ObjectContext objectContext, DbConfig config) : SqlDbHelper(objectContext, config.PermissionsDbConnectionString)
{
    public async Task<int> GetNoOfValidOrganisations(string hashedid)
    {
        string query = $@"SELECT count(AccountLegalEntityId) ctr FROM dbo.AccountProviderLegalEntities apl
                                JOIN AccountLegalEntities al ON al.id = apl.AccountLegalEntityId
                                JOIN Accounts A ON a.Id = al.AccountId
                                WHERE a.HashedId in ('{hashedid}')
                                GROUP by AccountLegalEntityId";

        var result = await GetDataAsObject(query);

        return (int)result;
    }
}

public class ProviderCreateVacancySqlDbHelper(ObjectContext objectContext, DbConfig config) : SqlDbHelper(objectContext, config.AccountsDbConnectionString)
{
    public async Task<List<object[]>> GetValidHashedId(List<string> hashedid)
    {
        string query = $@"SELECT HashedId, count(HashedId) FROM 
                                (SELECT a.HashedId HashedId, count(a.HashedId) ctr from employer_account.AccountLegalEntity al JOIN employer_account.Account a 
                                ON a.id = al.AccountId 
                                GROUP by a.HashedId, al.Deleted, al.SignedAgreementId
                                HAVING al.SignedAgreementId IS NOT NULL AND al.Deleted IS NULL AND a.HashedId in ({hashedid.Select(x => $"'{x}'").ToString(",")}) ) t
                                GROUP by t.HashedId";

        return await GetListOfData(query);
    }
}
