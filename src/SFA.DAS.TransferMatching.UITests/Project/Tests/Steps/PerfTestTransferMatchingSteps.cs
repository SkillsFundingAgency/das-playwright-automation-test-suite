using SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Steps;

[Binding]
public class PerfTestTransferMatchingSteps(ScenarioContext context)
{
    [Then("an employer can apply for '(.*)' pledge")]
    public async Task ApplyForExistingPledge(string pledgeId)
    {
        await new CreateAccountEmployerPortalLoginHelper(context).Login(context.GetUser<NonLevyUser>(), false);
        await context.Get<Driver>().Page.GotoAsync(UrlConfig.TransferMacthingApplyUrl(pledgeId));
        var details = await VerifyPageHelper.VerifyPageAsync(context, () => new TransferFundDetailsPage(context));
        await SubmitApplicationHelper.SubmitApplication(await details.ApplyForTransferFundsAfterLogin());
    }
}
