namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class SuccessfullyWithdrawnYourApplicationPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "You have successfully withdrawn your application";

    public async Task<AccountHomePage> ReturnToMyAccount() =>
        await FollowLink("Continue to my account", () => new AccountHomePage(context));
}
