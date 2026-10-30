namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class AcceptedTransferPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "You have successfully accepted a transfer";

    public async Task<MyApplicationsPage> ViewMyApplications() =>
        await FollowLink("View other applications", () => new MyApplicationsPage(context));
}
