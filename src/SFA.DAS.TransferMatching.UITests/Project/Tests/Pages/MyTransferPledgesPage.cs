namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class MyTransferPledgesPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "My transfer pledges";
    private ILocator PledgeSelector => page.Locator($"a[href='pledges/{GetPledgeId()}/applications']");

    public async Task<TransferPledgePage> GoToTransferPledgePage()
    {
        await FindOnPaginatedList(PledgeSelector);
        await PledgeSelector.ClickAsync();
        return await VerifyPageAsync(() => new TransferPledgePage(context));
    }

    public async Task<PledgeAndTransferYourLevyFundsPage> CreatePledge()
    {
        await page.Locator("[href*='/pledges/create/inform']").ClickAsync();
        return await VerifyPageAsync(() => new PledgeAndTransferYourLevyFundsPage(context));
    }

    public async Task<MyTransferPledgesPage> ConfirmStatus(string expectedStatus)
    {
        await FindOnPaginatedList(PledgeSelector);
        await Assertions.Expect(page.GetByRole(AriaRole.Row).Filter(new() { Has = PledgeSelector }).Locator(".govuk-tag"))
            .ToHaveTextAsync(expectedStatus);
        return this;
    }
}
