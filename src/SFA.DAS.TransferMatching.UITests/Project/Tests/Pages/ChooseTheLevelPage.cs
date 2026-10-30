namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ChooseTheLevelPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Choose the levels of apprenticeship training you'd like to fund";
    protected override ILocator ContinueButton => page.Locator("#pledge-level-continue");
    public async Task<CreateATransferPledgePage> SelectLevelAndContinue() => await SelectAndContinue();
}
