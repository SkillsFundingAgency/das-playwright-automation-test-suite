namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class MyAccountTransferFundingPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Choose the account you want to use to apply for funding";

    public async Task<CreateATransfersApplicationPage> GoToCreateATransfersApplicationPage(string orgName) =>
        await FollowLink(orgName, () => new CreateATransfersApplicationPage(context));
}
