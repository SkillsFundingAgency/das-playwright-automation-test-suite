namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class CohortSummaryPage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#commitmentsHeader")).ToContainTextAsync("Cohort Summary");

    public async Task<CohortDetailsPage> ClickViewThisCohortButton()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View this cohort" }).ClickAsync();

        return await VerifyPageAsync(() => new CohortDetailsPage(context));
    }
}