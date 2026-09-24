namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;

public class SetTheFundingEndDate_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Set the end date for funding extension" })).ToBeVisibleAsync();
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    private DateTime GetNextAcademicYearEndDate()
    {
        var today = DateTime.Today;
        int currentAcademicYearEndYear = today.Month >= 8
            ? today.Year + 1
            : today.Year;
        return new DateTime(currentAcademicYearEndYear + 1, 7, 31);
    }
    public async Task EnterMaxApprovalEndDate()
    {
        var endDate = GetNextAcademicYearEndDate();

        await page.Locator("#maxApprovalEndDateDay")
            .FillAsync(endDate.Day.ToString());

        await page.Locator("#maxApprovalEndDateMonth")
            .FillAsync(endDate.Month.ToString());

        await page.Locator("#maxApprovalEndDateYear")
            .FillAsync(endDate.Year.ToString());
    }
}
