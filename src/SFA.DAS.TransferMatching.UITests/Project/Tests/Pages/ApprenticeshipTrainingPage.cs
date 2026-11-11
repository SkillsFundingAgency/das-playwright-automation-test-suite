using System.Text.RegularExpressions;

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
        await FillAndKeyUp("#opportunity-application-apprentices", tMDataHelper.NoOfApprentice.ToString());
        await Continue();
        return await VerifyPageAsync(() => this);
    }

    private async Task EnterDetails()
    {
        // The select is replaced by an accessible-autocomplete input once the page script runs.
        await page.Locator("input#SelectedStandardId").FillAsync(tMDataHelper.Course);
        await page.Locator("#SelectedStandardId__listbox").GetByRole(AriaRole.Option)
            .Filter(new() { HasTextRegex = new Regex($"^\\s*{Regex.Escape(tMDataHelper.Course)}\\b") }).First.ClickAsync();
        await VerifyTrainingCost();
        await SelectRadioOptionByText("No, show me training providers");
    }

    private async Task VerifyTrainingCost()
    {
        var startDate = tMDataHelper.CourseStartDate;
        await FillAndKeyUp("#opportunity-application-apprentices", tMDataHelper.NoOfApprentice.ToString());
        await FillAndKeyUp("#opportunity-application-start-month", startDate.ToString("MM"));
        await FillAndKeyUp("#opportunity-application-start-year", startDate.ToString("yyyy"));
        await Assertions.Expect(page.Locator("#panel-estimate")).ToContainTextAsync("Estimated yearly cost for training:");
        await Assertions.Expect(page.Locator("#panel-estimate #field-estimate"))
            .ToHaveTextAsync(tMDataHelper.GetEstimatedCostOfTrainingForApplicationDetail());
    }

    // The funding estimate is recalculated by onkeyup handlers, which FillAsync does not trigger.
    private async Task FillAndKeyUp(string selector, string value)
    {
        var input = page.Locator(selector);
        await input.FillAsync(value);
        await input.DispatchEventAsync("keyup");
    }
}
