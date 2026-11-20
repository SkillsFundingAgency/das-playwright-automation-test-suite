namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class SuccessfullyWithdrawnPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "You have successfully declined a transfer of funds";

    public async Task<AccountHomePage> ReturnToMyAccount() =>
        await FollowLink("Return to my account", () => new AccountHomePage(context));
}
