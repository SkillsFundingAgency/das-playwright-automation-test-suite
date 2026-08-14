namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;

public class EnterThresholdDates_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Enter dates to be used in calculating eligibility for rollover" })).ToBeVisibleAsync();    
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    public async Task VerifyThresholdDatesErrorMessage()
    {
        var fundingErrorLocator = page.Locator("a[href='#FundingEndDate']");
        var operationalErrorLocator = page.Locator("a[href='#OperationalEndDate']");
        var actualFundingErrorMessage = (await fundingErrorLocator.InnerTextAsync()).Trim();
        var actualOperationalErrorMessage = (await operationalErrorLocator.InnerTextAsync()).Trim();
        var expectedFundingErrorMessage = "Enter Funding end date";
        var expectedOperationalErrorMessage = "Enter Operational end date";
        if (!string.Equals(
            actualFundingErrorMessage,
            expectedFundingErrorMessage,
            StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected funding date error message to be '{expectedFundingErrorMessage}', " +
                $"but the actual message is '{actualFundingErrorMessage}'.");
        }
        if (!string.Equals(
            actualOperationalErrorMessage,
            expectedOperationalErrorMessage,
            StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected operational date error message to be '{expectedOperationalErrorMessage}', " +
                $"but the actual message is '{actualOperationalErrorMessage}'.");
        }
    }
    public async Task EnterAcademicYearAndOperationalEndDates()
    {
        var endDate = GetAcademicYearEndDate();

        await page.Locator("#FundingEndDate_Day")
            .FillAsync(endDate.Day.ToString());

        await page.Locator("#FundingEndDate_Month")
            .FillAsync(endDate.Month.ToString());

        await page.Locator("#FundingEndDate_Year")
            .FillAsync(endDate.Year.ToString());

        await page.Locator("#OperationalEndDate_Day")
            .FillAsync(endDate.Day.ToString());

        await page.Locator("#OperationalEndDate_Month")
            .FillAsync(endDate.Month.ToString());

        await page.Locator("#OperationalEndDate_Year")
            .FillAsync(endDate.Year.ToString());
    }
    private DateTime GetAcademicYearEndDate()
    {
        var today = DateTime.Today;
        return today.Month >= 8
            ? new DateTime(today.Year + 1, 7, 31)
            : new DateTime(today.Year, 7, 31);
    }
}