namespace SFA.DAS.Login.Service.Project.Helpers;

public class AccountDetails(int index, string accountId, string hashedId, string orgName, string publicHashedId, string alename, string aleid, string aleAccountid, string aleAgreementid)
{
    internal int AccountIndex { get; private set; } = index;
    public string OrgName { get; private set; } = orgName;
    public string AccountId { get; private set; } = accountId;
    public string HashedId { get; private set; } = hashedId;
    public string PublicHashedid { get; private set; } = publicHashedId;
    public string Alename { get; private set; } = alename;
    public string Aleid { get; private set; } = aleid;
    public string AleAccountid { get; private set; } = aleAccountid;
    public string AleAgreementid { get; private set; } = aleAgreementid;

    public override string ToString() => $@"AccountDetails ({AccountIndex}) : Organisation Name : '{OrgName}', AccountId : '{AccountId}', HashedId : '{HashedId}', PublicHashedId : '{PublicHashedid}', 
                                                Alename : '{Alename}', Aleid : '{Aleid}', AleAccountid : '{AleAccountid}', AleAgreementid : '{AleAgreementid}'";
}
