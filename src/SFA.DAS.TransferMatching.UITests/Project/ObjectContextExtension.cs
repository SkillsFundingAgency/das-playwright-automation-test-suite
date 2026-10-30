namespace SFA.DAS.TransferMatching.UITests.Project;

public static class ObjectContextExtension
{
    internal static void SetPledgeDetailList(this ObjectContext context) => context.Set("tmpledges", new List<Pledge>());
    internal static List<Pledge> GetPledgeDetailList(this ObjectContext context) => context.Get<List<Pledge>>("tmpledges");
    internal static void SetEmployerTotalPledgeAmount(this ObjectContext context, int value) => context.Replace("employertotalpledgeamount", value);
    public static int GetEmployerTotalPledgeAmount(this ObjectContext context) => context.Get<int>("employertotalpledgeamount");
    internal static void SetPledgeAmount(this ObjectContext context, int value) => context.Replace("pledgeamount", value);
    internal static void SetPledgeCreatedOn(this ObjectContext context, DateTime value) => context.Replace("pledgecreatedon", value);
    internal static void SetPledgeApplication(this ObjectContext context, string pledgeId, string value) =>
        context.GetPledgeDetailList().Single(x => x.PledgeId == pledgeId).Applications.Add(value);
    internal static void SetPledgeDetail(this ObjectContext context, string pledgeId, string senderHashedAccountId = null)
    {
        var account = context.GetAll<UserCreds>().SelectMany(x => x.AccountDetails)
            .Where(x => x.Alename == context.GetOrganisationName()).DistinctBy(x => x.AccountId).Single();
        context.GetPledgeDetailList().Add(new Pledge
        {
            PledgeId = pledgeId,
            Amount = context.Get<int>("pledgeamount"),
            CreatedOn = context.Get<DateTime>("pledgecreatedon"),
            EmployerAccountId = account.AccountId,
            SenderHashedAccountId = account.HashedId
        });
    }
    internal static string GetPledgeApplication(this ObjectContext context, string pledgeId) =>
        context.GetPledgeDetailList().Single(x => x.PledgeId == pledgeId).Applications.Last();
    internal static Pledge GetPledgeDetail(this ObjectContext context) => context.GetPledgeDetailList().Last();
    internal static Pledge GetPledgeDetail(this ObjectContext context, int index) => context.GetPledgeDetailList()[index];
}
