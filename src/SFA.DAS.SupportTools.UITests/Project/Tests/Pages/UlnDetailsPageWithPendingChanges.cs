namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class UlnDetailsPageWithPendingChanges(ScenarioContext context, CohortDetails cohortDetails) : UlnDetailsPage(context, cohortDetails)
{
    internal async Task ClickPendingChangesTab() => await page.Locator("#tab_pending-changes").ClickAsync();

    internal async Task PendingChangesAreDisplayed() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Changes requested by training provider or employer");
}
