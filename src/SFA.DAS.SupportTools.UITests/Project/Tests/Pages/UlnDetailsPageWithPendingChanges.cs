namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class UlnDetailsPageWithPendingChanges(ScenarioContext context, CohortDetails cohortDetails) : UlnDetailsPage(context, cohortDetails)
{
    internal async Task ClickPendingChangesTab() => await page.GetByRole(AriaRole.Link, new() { Name = "Pending Changes" }).ClickAsync();

    internal async Task PendingChangesAreDisplayed() => await Assertions.Expect(page.Locator("#tab-pending-changes")).ToContainTextAsync("Changes requested by training provider or employer");
}
