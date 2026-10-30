namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ApprenticeshipTrainingPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Training details";
    protected override ILocator ContinueButton => page.Locator("#opportunity-criteria-continue");

    public async Task<CreateATransfersApplicationPage> EnterAppTrainingDetailsAndContinue()
    {
        await EnterDetails();
        await Continue();
        return await VerifyPageAsync(() => new CreateATransfersApplicationPage(context));
    }

    public async Task<ApprenticeshipTrainingPage> EnterAmountMoreThanAvailableFunding()
    {
        tMDataHelper.NoOfApprentice = 1;
        await EnterDetails();
        tMDataHelper.Cost = ReadAmount(await page.Locator("#panel-estimate #field-estimate").InnerTextAsync());
        tMDataHelper.NoOfApprentice = objectContext.GetPledgeDetail().Amount / tMDataHelper.Cost + 1;
        await page.Locator("#opportunity-application-apprentices").FillAsync(tMDataHelper.NoOfApprentice.ToString());
        await Continue();
        return await VerifyPageAsync(() => this);
    }

    private async Task EnterDetails()
    {
        await page.Locator("#SelectedStandardId").FillAsync(tMDataHelper.Course);
        await VerifyTrainingCost();
        await SelectRadioOptionByText("No, show me training providers");
    }

    private async Task VerifyTrainingCost()
    {
        var startDate = tMDataHelper.CourseStartDate;
        await page.Locator("#opportunity-application-apprentices").FillAsync(tMDataHelper.NoOfApprentice.ToString());
        await page.Locator("#opportunity-application-start-month").FillAsync(startDate.ToString("MM"));
        await page.Locator("#opportunity-application-start-year").FillAsync(startDate.ToString("yyyy"));
        await page.Locator("#opportunity-application-apprentices").ClickAsync();
        await Assertions.Expect(page.Locator("#panel-estimate")).ToContainTextAsync("Estimated yearly cost for training:");
        await Assertions.Expect(page.Locator("#panel-estimate #field-estimate"))
            .ToContainTextAsync($"£{tMDataHelper.GetEstimatedCostOfTrainingForApplicationDetail()}");
    }
}
