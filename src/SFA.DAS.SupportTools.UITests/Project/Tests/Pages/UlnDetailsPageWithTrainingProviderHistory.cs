namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class UlnDetailsPageWithTrainingProviderHistory(ScenarioContext context, CohortDetails cohortDetails) : UlnDetailsPage(context, cohortDetails)
{
    internal async Task ClickTrainingProviderHistoryTab() => await page.Locator("#tab_provider-history").ClickAsync();

    internal async Task TrainingProviderHistoryIsDisplayed() => await Assertions.Expect(page.Locator("#tab_provider-history")).ToContainTextAsync("Training provider history");
}