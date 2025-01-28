namespace SFA.DAS.SupportConsole.UITests.Project.Tests.Pages;

public class UlnDetailsPage(ScenarioContext context, CohortDetails cohortDetails) : SupportConsoleBasePage(context)
{
    private readonly CohortDetails cohortDetails = cohortDetails;

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#content")).ToContainTextAsync(cohortDetails.UlnName);

    public async Task VerifyUlnDetailsPageHeaders()
    {
        await Assertions.Expect(page.Locator("#content")).ToContainTextAsync(cohortDetails.Uln);

        await Assertions.Expect(page.Locator("#tab-summary")).ToContainTextAsync(cohortDetails.CohortRef);
    }
}

public class UlnDetailsPageWithPendingChanges(ScenarioContext context, CohortDetails cohortDetails) : UlnDetailsPage(context, cohortDetails)
{
    internal async Task ClickPendingChangesTab() => await page.GetByRole(AriaRole.Link, new() { Name = "Pending Changes" }).ClickAsync();

    internal async Task PendingChangesAreDisplayed() => await Assertions.Expect(page.Locator("#tab-pending-changes")).ToContainTextAsync("Changes requested by training provider or employer");
}

public class UlnDetailsPageWithTrainingProviderHistory(ScenarioContext context, CohortDetails cohortDetails) : UlnDetailsPage(context, cohortDetails)
{
    internal async Task ClickTrainingProviderHistoryTab() => await page.GetByRole(AriaRole.Link, new() { Name = "Training provider history" }).ClickAsync();

    internal async Task TrainingProviderHistoryIsDisplayed() => await Assertions.Expect(page.Locator("#tab-provider-history")).ToContainTextAsync("Training provider history");
}