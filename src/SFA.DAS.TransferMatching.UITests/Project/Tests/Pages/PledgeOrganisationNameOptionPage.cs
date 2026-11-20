namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class PledgeOrganisationNameOptionPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Would you like your pledge to show your organisation’s name publicly?";
    protected override ILocator ContinueButton => page.Locator("#pledge-criteria-continue");

    public async Task<CreateATransferPledgePage> EnterValidOrgNameChoice(bool showOrg)
    {
        await SelectRadioOptionByText(showOrg ? "Yes, show my organisation's name" : "No, hide my organisation's name");
        await Continue();
        return await VerifyPageAsync(() => new CreateATransferPledgePage(context));
    }
}
