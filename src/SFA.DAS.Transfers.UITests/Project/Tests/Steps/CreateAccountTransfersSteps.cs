using SFA.DAS.Transfers.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;

namespace SFA.DAS.Transfers.UITests.Project.Tests.Steps;

[Binding]
public class CreateAccountTransfersSteps(ScenarioContext context)
{
    private readonly TransferConnectionStepsHelper connections = new(context);
    private readonly TransferCohortStepsHelper cohorts = new(context);

    [Given(@"^We have (two|three) Employer accounts$")]
    public async Task CreateAccounts(string number) => await connections.CreateAccounts(number);

    [Given(@"^(First|Second|Third) is a Sender connected to (First|Second|Third) as a Receiver$")]
    public async Task Connect(string sender, string receiver) => await connections.Connect(sender, receiver);

    [When(@"^(First|Second|Third) account creates transfer request to (First|Second|Third) account and (First|Second|Third) account accepts the request$")]
    public async Task ConnectAndAccept(string sender, string receiver, string acceptor)
    {
        if (receiver != acceptor)
            throw new ArgumentException("The acceptor must be the same as the receiver.", nameof(acceptor));
        await connections.Connect(sender, receiver);
    }

    [When(@"^(First|Second|Third) account creates a transfer connection request to (First|Second|Third) account$")]
    public async Task SendRequest(string sender, string receiver) => await connections.SendRequest(sender, receiver);

    [When(@"^(First|Second|Third) account accepts the transfer connection request from (First|Second|Third) account$")]
    public async Task AcceptRequest(string receiver, string sender) => await connections.AcceptRequest(receiver, sender);

    [Then(@"^A transfer connection is established successfully between (First|Second|Third) account as Sender and (First|Second|Third) account as Receiver$")]
    public async Task VerifyConnection(string sender, string receiver) => await connections.VerifyConnection(sender, receiver);

    [Then(@"^(First|Second|Third) account has '(\d+) connection requests? to review' task link$")]
    public async Task VerifyTaskCount(string account, int count) => await connections.VerifyConnectionRequestTaskCount(account, count);

    [Then(@"^(First|Second|Third) account has no '\.\.\. connection request\(s\) to review' task link$")]
    [Then(@"^(First|Second|Third) account has no '\.\.\. connection'$")]
    public async Task VerifyNoTasks(string account) => await connections.VerifyConnectionRequestTaskCount(account, 0);

    [When(@"^Receiver (First|Second|Third) sends empty cohort using transfer funds from Sender (First|Second|Third) to the provider for review and approval$")]
    public async Task SendEmptyCohort(string receiver, string sender)
    {
        await SwitchAccount(receiver);
        await cohorts.CreateCohort(connections.GetAccount(sender).OrgName, connections.GetAccount(receiver), 1, false);
    }

    [When(@"^Receiver (First|Second|Third) sends approved cohort using transfer funds from Sender (First|Second|Third) with (\d+) apprentices to the provider for review and approval$")]
    public async Task SendApprovedCohort(string receiver, string sender, int count)
    {
        await SwitchAccount(receiver);
        await cohorts.CreateCohort(connections.GetAccount(sender).OrgName, connections.GetAccount(receiver), count, true);
    }

    [When(@"^Provider adds an apprentice and approves the cohort$")]
    public async Task ProviderAddsAndApproves() => await cohorts.ProviderAddsAndApproves();

    [When(@"^Provider approves the cohort$")]
    [When(@"^Provider approves the cohort and sends to recevier for approval$")]
    public async Task ProviderApproves() => await cohorts.ProviderApproves();

    [When(@"^Provider edits the apprentice Name$")]
    public async Task ProviderEditsApprentice() => await cohorts.ProviderEditsApprentice();

    [When(@"^Receiver (First|Second|Third) approves the cohort$")]
    public async Task ReceiverApproves(string receiver)
    {
        await SwitchAccount(receiver);
        await cohorts.EmployerApproves();
    }

    [When(@"^Sender (First|Second|Third) approves the cohort$")]
    public async Task SenderApproves(string sender)
    {
        await SwitchAccount(sender);
        var transfers = await connections.OpenTransfers();
        var request = await transfers.OpenPendingCohortRequestAsFundingEmployer(context.Get<ObjectContext>().GetTransferReceiverOrganisationName(), cohorts.TotalCost);
        await request.ApproveTransferRequest();
    }

    [When(@"^Sender (First|Second|Third) rejects the cohort$")]
    public async Task SenderRejects(string sender)
    {
        await SwitchAccount(sender);
        var transfers = await connections.OpenTransfers();
        var request = await transfers.OpenPendingCohortRequestAsFundingEmployer(context.Get<ObjectContext>().GetTransferReceiverOrganisationName(), cohorts.TotalCost);
        await request.RejectTransferRequest();
    }

    [Then(@"^Receiver (First|Second|Third) sees the cohort in With transfer sending employers with status of (.*)$")]
    public async Task VerifyWithTransferSenderStatus(string receiver, string status)
    {
        await SwitchAccount(receiver);
        await cohorts.VerifyCohortStatus(status, true);
    }

    [Then(@"^Receiver (First|Second|Third) sees the cohort in Ready to review with status of (.*)$")]
    public async Task VerifyReadyToReviewStatus(string receiver, string status)
    {
        await SwitchAccount(receiver);
        await cohorts.VerifyCohortStatus(status, false);
    }

    [When(@"^Receiver (First|Second|Third) edits and sends an approved cohort to the provider$")]
    public async Task EditAndSendCohort(string receiver)
    {
        await SwitchAccount(receiver);
        await cohorts.EmployerEditsAndSends();
    }

    [When(@"^Receiver (First|Second|Third) sends a cohort to the provider for review and approval$")]
    public async Task SendCohortForReview(string receiver)
    {
        await SwitchAccount(receiver);
        await cohorts.EmployerSendsForReview();
    }

    [Then(@"^Receiver (First|Second|Third) has a new live apprenticeship record created$")]
    public async Task VerifyLiveApprentice(string receiver)
    {
        await SwitchAccount(receiver);
        await cohorts.VerifyLiveApprentices();
    }

    [Then(@"^Receiver (First|Second|Third) has '(\d+) apprentice changes? to review' task link$")]
    public async Task VerifyApprenticeChangeTasks(string receiver, int count) =>
        await VerifyTasks(receiver, TasksHomePage.ApprenticeChangeToReview, count);

    [Then(@"^Receiver (First|Second|Third) has no '\.\.\. apprentice change\(s\) to review' task link$")]
    public async Task VerifyNoApprenticeChangeTasks(string receiver) =>
        await VerifyTasks(receiver, TasksHomePage.ApprenticeChangeToReview, 0);

    [Then(@"^Receiver (First|Second|Third) has '(\d+) apprentice requests? ready for review' task link$")]
    [Then(@"^Receiver (First|Second|Third) has '(\d+) cohort requests? ready for approval' task link$")]
    public async Task VerifyReadyForReviewTasks(string receiver, int count) =>
        await VerifyTasks(receiver, TasksHomePage.CohortReadyForApproval, count);

    [Then(@"^Receiver (First|Second|Third) has no '\.\.\. apprentice request\(s\) ready for review' task link$")]
    [Then(@"^Receiver (First|Second|Third) has no '\.\.\. cohort request\(s\) ready for approval' task link$")]
    public async Task VerifyNoReadyForReviewTasks(string receiver) =>
        await VerifyTasks(receiver, TasksHomePage.CohortReadyForApproval, 0);

    [Then(@"^'Transfer request received' task link is displayed under Tasks pane for the Sender (First|Second|Third) account$")]
    public async Task VerifyTransferReceivedTask(string sender) =>
        await VerifyTasks(sender, TasksHomePage.TransferRequestReceived, 1);

    [Then(@"^No 'Transfer request received' task link is displayed under Tasks pane for the Sender (First|Second|Third) account$")]
    public async Task VerifyNoTransferReceivedTask(string sender) =>
        await VerifyTasks(sender, TasksHomePage.TransferRequestReceived, 0);

    private async Task SwitchAccount(string ordinal)
    {
        cohorts.UseEmployerPage();
        await connections.SwitchAccount(ordinal);
    }

    private async Task VerifyTasks(string ordinal, string taskType, int count)
    {
        await SwitchAccount(ordinal);
        await new TasksHomePage(context).AssertTaskCount(taskType, count);
    }
}
