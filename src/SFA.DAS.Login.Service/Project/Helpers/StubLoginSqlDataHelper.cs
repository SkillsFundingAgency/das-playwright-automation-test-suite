namespace SFA.DAS.Login.Service.Project.Helpers;


public class ApprenticeAccountsStubLoginSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.ApprenticeCommitmentAccountsDbConnectionString)
{
    internal async Task<List<(string signInId, string firstName, string lastName, string id)>> GetSignInIds(List<string> emails)
    {
        var query = emails.Select(GetSqlQuery).ToList();

        var accountdetails = new List<(string, string, string, string)>();

        var results = await GetListOfMultipleData(query);

        foreach (var data in results) accountdetails.Add((Func(data.ListOfArrayToList(0)), Func(data.ListOfArrayToList(1)), Func(data.ListOfArrayToList(2)), Func(data.ListOfArrayToList(3))));

        return accountdetails;
    }

    private static string GetSqlQuery(string email) => $"select GovUkIdentifier, FirstName, LastName, id from dbo.Apprentice where email = '{email}'";
}

public class AssessorStubLoginSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.AssessorDbConnectionString)
{
    internal async Task<List<(string signInId, string displayName)>> GetSignInIds(List<string> emails)
    {
        var query = emails.Select(GetSqlQuery).ToList();

        var accountdetails = new List<(string, string)>();

        var results = await GetListOfMultipleData(query);

        foreach (var data in results) accountdetails.Add((Func(data.ListOfArrayToList(0)), Func(data.ListOfArrayToList(1))));

        return accountdetails;
    }

    private static string GetSqlQuery(string email) => $"select SignInId, DisplayName from dbo.Contacts where email = '{email}'";
}

public class CandidateAccountStubLoginSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.CanAccDbConnectionString)
{
    internal async Task<List<(string signInId, string firstName, string lastName, string mobilePhone)>> GetSignInIds(List<string> emails)
    {
        var query = emails.Select(GetSqlQuery).ToList();

        var accountdetails = new List<(string, string, string, string)>();

        var results = await GetListOfMultipleData(query);

        foreach (var data in results) accountdetails.Add((Func(data.ListOfArrayToList(0)), Func(data.ListOfArrayToList(1)), Func(data.ListOfArrayToList(2)), Func(data.ListOfArrayToList(3))));

        return accountdetails;
    }

    private static string GetSqlQuery(string email) => $"select GovUkIdentifier, FirstName, LastName, PhoneNumber from dbo.Candidate where email = '{email}'";
}
