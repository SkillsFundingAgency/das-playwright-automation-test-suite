namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ChoosetheSectorsPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Choose the sectors you'd like to fund";
    protected override ILocator ContinueButton => page.Locator("#pledge-criteria-continue");
    public async Task<CreateATransferPledgePage> SelectSetorAndContinue() => await SelectAndContinue();
}
