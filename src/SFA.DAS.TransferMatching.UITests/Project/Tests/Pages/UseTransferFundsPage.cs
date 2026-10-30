using SFA.DAS.Approvals.UITests.Project.Pages.Employer;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class UseTransferFundsPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Use transfer funds from";

    internal async Task<ChooseYourMainTrainingProviderPage> ClickOnStartNowButton() =>
        await FollowLink("Start now", () => new ChooseYourMainTrainingProviderPage(context));
}
