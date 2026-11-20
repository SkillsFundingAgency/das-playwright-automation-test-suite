namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class Pledge100PercentMatchPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Do you want to automatically approve 100% matches?";
    protected override ILocator ContinueButton => page.Locator("#pledge-criteria-continue");

    public async Task<CreateATransferPledgePage> EnterValidMatchChoice(bool immediateMatch)
    {
        await SelectRadioOptionByText(immediateMatch ? "Yes, automatically approve"
            : "No, I would like 6 weeks to approve or reject 100% matches");
        await Continue();
        return await VerifyPageAsync(() => new CreateATransferPledgePage(context));
    }
}
