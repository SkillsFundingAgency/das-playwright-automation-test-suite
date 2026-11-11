namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class AddtheLocationPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Add the locations you'd like to fund";
    protected override ILocator ContinueButton => page.Locator("#pledge-criteria-continue");

    public async Task<CreateATransferPledgePage> EnterLocation()
    {
        await page.Locator("#Locations_0_").FillAsync(TMDataHelper.GetRandomLocation());
        await PageHeader.ClickAsync();
        await Continue();
        return await VerifyPageAsync(() => new CreateATransferPledgePage(context));
    }
}
