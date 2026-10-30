namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class PledgeAmountPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Pledge amount";
    protected override ILocator ContinueButton => page.Locator("#pledge-criteria-continue");

    public async Task<PledgeAmountPage> CaptureAvailablePledgeAmount()
    {
        objectContext.SetEmployerTotalPledgeAmount(ReadAmount(await page.Locator("#remaining-transfer-balance").InnerTextAsync()));
        return this;
    }

    public async Task<PledgeAmountPage> EnterInValidAmount()
    {
        await EnterAmount(true);
        return await VerifyPageAsync(() => this);
    }

    public async Task<CreateATransferPledgePage> EnterInValidAmountForCreateAPledge(
        bool useMinimalFunding = false, bool useFundingEqualToEstimatedCost = false)
    {
        await EnterAmount(false, useMinimalFunding, useFundingEqualToEstimatedCost);
        return await VerifyPageAsync(() => new CreateATransferPledgePage(context));
    }

    private async Task EnterAmount(bool exceedMaxFunding, bool useMinimalFunding = false, bool useFundingEqualToEstimatedCost = false)
    {
        var amount = objectContext.GetEmployerTotalPledgeAmount();
        if (!exceedMaxFunding && useMinimalFunding && amount > tMDataHelper.MinimalPledgeAmount)
            amount = tMDataHelper.MinimalPledgeAmount;

        var randomAmount = exceedMaxFunding
            ? TMDataHelper.GenerateRandomNumberMoreThanMaxAmount(amount) : ValidatePledgeAmount(amount);
        var pledgeAmount = useFundingEqualToEstimatedCost
            ? ReadAmount(tMDataHelper.GetEstimatedCostOfTrainingForApplicationDetail()) + 1 : randomAmount;
        await page.Locator("#Amount").FillAsync(pledgeAmount.ToString());
        await Continue();
        objectContext.SetPledgeAmount(pledgeAmount);
    }

    private int ValidatePledgeAmount(int availableAmount)
    {
        NUnit.Framework.Assert.That(availableAmount, NUnit.Framework.Is.GreaterThanOrEqualTo(tMDataHelper.MinAmount),
            $"Available pledge amount {availableAmount} is less than the minimum amount needed {tMDataHelper.MinAmount}");
        return tMDataHelper.GenerateRandomNumberMoreThanMinAmount(availableAmount);
    }
}
