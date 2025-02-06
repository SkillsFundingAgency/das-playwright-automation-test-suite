namespace SFA.DAS.Login.Service.Project.Helpers;

internal class EasAccountsSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.AccountsDbConnectionString)
{
    internal async Task<List<UserCreds>> GetAccountDetails(List<string> emails)
    {
        var query = emails.Select(GetSqlQuery1).ToList();

        var usercredslist = new List<UserCreds>();

        var results = await GetListOfMultipleData(query);

        foreach (var usercreds in results)
        {
            var list = TransformAccountDetails(usercreds);

            var usercreditem = new UserCreds(list.FirstOrDefault().email, list.FirstOrDefault().userref);

            foreach (var (index, email, userref, accountId, hashedId, orgName, publicHashedId, alename, aleid, aleAccountid, aleAgreementid) in list)
            {
                usercreditem.AccountDetails.Add(new AccountDetails(index, accountId, hashedId, orgName, publicHashedId, alename, aleid, aleAccountid, aleAgreementid));
            }

            usercredslist.Add(usercreditem);
        }

        return usercredslist;
    }

    public static List<(int index, string email, string userref, string accountId, string hashedId, string orgName, string publicHashedId, string alename, string aleid, string aleAccountid, string aleAgreementid)> TransformAccountDetails(List<string[]> id)
    {
        var list = new List<(int index, string email, string userref, string accountId, string hashedId, string orgName, string publicHashedId, string alename, string aleid, string aleAccountid, string aleAgreementid)>();

        for (int i = 0; i < id.Count; i++) list.Add((i, id[i][0], id[i][1], id[i][2], id[i][3], id[i][4], id[i][5], RegexHelper.ReplaceMultipleSpace(id[i][6]), id[i][7], id[i][8], id[i][9]));

        return list;
    }

    private static string GetSqlQuery1(string email) => $@"select u.Email, u.Userref, a.id, a.HashedId, a.[Name], a.PublicHashedId, ale.[name], ale.id as aleid, ale.AccountId, ale.PublicHashedId as agreementid from employer_account.[User] u
        join employer_account.Membership m on u.id = m.UserId join employer_account.Account a on a.id = m.AccountId join employer_account.AccountLegalEntity ale on ale.AccountId = a.Id
        where u.Email = '{email}' and ale.deleted is null order by a.CreatedDate";
}
