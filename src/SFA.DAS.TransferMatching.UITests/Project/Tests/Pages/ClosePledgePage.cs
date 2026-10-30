namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ClosePledgePage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => $"Close pledge {GetPledgeId()}";
    protected override ILocator ContinueButton => page.Locator("#main-content .govuk-button");

    public async Task<TransferPledgePage> DontClose()
    {
        await page.Locator("#close-pledge-no").CheckAsync();
        await Continue();
        return await VerifyPageAsync(() => new TransferPledgePage(context));
    }

    public async Task<MyTransferPledgesPage> ConfirmClose()
    {
        await page.Locator("#close-pledge-yes").CheckAsync();
        await Continue();
        return await VerifyPageAsync(() => new MyTransferPledgesPage(context));
    }
}
