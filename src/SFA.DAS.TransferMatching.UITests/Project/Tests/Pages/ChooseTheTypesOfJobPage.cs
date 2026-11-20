namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ChooseTheTypesOfJobPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Choose the types of job role you'd like to fund";
    protected override ILocator ContinueButton => page.Locator("#pledge-criteria-continue");
    public async Task<CreateATransferPledgePage> SelectTypeOfJobAndContinue() => await SelectAndContinue();
}
