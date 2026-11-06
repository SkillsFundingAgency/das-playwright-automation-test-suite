using SFA.DAS.Transfers.UITests.Project.Tests.Pages;

namespace SFA.DAS.Transfers.UITests.Project.Helpers;

public class TransferConnectionStepsHelper(ScenarioContext context)
{
    private readonly ObjectContext objectContext = context.Get<ObjectContext>();
    private readonly AccountCreationStepsHelper accountCreationStepsHelper = new(context);
    private readonly Dictionary<string, AccountDetails> accounts = [];
    private HomePage homePage;

    public async Task CreateAccounts(string number)
    {
        var count = number switch
        {
            "two" => 2,
            "three" => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(number), number, "Only two or three accounts are supported.")
        };
        var data = context.Get<EmployerPortalDataHelper>();
        var names = new[] { data.CompanyTypeOrg, data.CompanyTypeOrg2, data.CompanyTypeOrg3 };
        var ordinals = new[] { "First", "Second", "Third" };

        homePage = await accountCreationStepsHelper.CreateUserAccount();
        for (var index = 1; index < count; index++)
        {
            objectContext.SetOrganisationName(names[index]);
            homePage = await accountCreationStepsHelper.AddNewAccount(homePage, index);
        }

        var users = await context.GetAccountLegalEntities([objectContext.GetRegisteredEmail()]);
        var details = users.Single().AccountDetails;
        for (var index = 0; index < count; index++)
        {
            // Match by organisation rather than relying on database row ordering.
            accounts.Add(ordinals[index], details.Single(x => x.OrgName.Equals(names[index], StringComparison.OrdinalIgnoreCase)));
        }
    }

    public AccountDetails GetAccount(string ordinal) => accounts.TryGetValue(ordinal, out var account)
        ? account
        : throw new InvalidOperationException($"Employer account '{ordinal}' has not been created.");

    public async Task Connect(string sender, string receiver)
    {
        await SendRequest(sender, receiver);
        await AcceptRequest(receiver, sender);
    }

    public async Task SendRequest(string sender, string receiver)
    {
        var sendingAccount = GetAccount(sender);
        var receivingAccount = GetAccount(receiver);
        objectContext.ReplaceTransferSenderOrganisationName(sendingAccount.OrgName);
        await SwitchAccount(sender);
        var transfers = await OpenTransfers();
        var connect = await transfers.ConnectWithReceivingEmployer();
        var whichEmployer = await connect.ContinueToConnectWithReceiver();
        var confirm = await whichEmployer.ConnectWithReceivingEmployer(receivingAccount.PublicHashedid);
        var sent = await confirm.SendTransferConnectionRequest();
        homePage = await sent.GoToHomePage();
    }

    public async Task AcceptRequest(string receiver, string sender)
    {
        objectContext.ReplaceTransferReceiverOrganisationName(GetAccount(receiver).OrgName);
        await SwitchAccount(receiver);
        var transfers = await OpenTransfers();
        var request = await transfers.ViewTransferConnectionRequestDetails(GetAccount(sender).OrgName);
        var confirmed = await request.AcceptTransferConnectionRequest();
        homePage = await confirmed.GoToHomePage();
    }

    public async Task VerifyConnection(string sender, string receiver)
    {
        await SwitchAccount(sender);
        await (await OpenTransfers()).VerifyTransferConnectionStatus(GetAccount(receiver).OrgName, "send");
        await SwitchAccount(receiver);
        await (await OpenTransfers()).VerifyTransferConnectionStatus(GetAccount(sender).OrgName, "receive");
    }

    public async Task VerifyConnectionRequestTaskCount(string account, int count)
    {
        await SwitchAccount(account);
        await new TasksHomePage(context).AssertTaskCount(TasksHomePage.ReviewConnectionRequest, count);
    }

    public async Task<HomePage> SwitchAccount(string ordinal)
    {
        var account = GetAccount(ordinal);
        // The last action may have ended on a confirmation rather than the homepage.
        var yourAccounts = await new HomePage(context).GoToYourAccountsPage();
        homePage = await yourAccounts.ClickAccountLink(account.OrgName);
        return homePage;
    }

    public async Task<TransfersPage> OpenTransfers()
    {
        var finance = await new FinancePage(context).NavigateFromAccount();
        return await finance.OpenTransfers();
    }
}
