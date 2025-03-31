namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class UlnDetailsPageWithTrainingProviderHistory(ScenarioContext context, CohortDetails cohortDetails) : UlnDetailsPage(context, cohortDetails)
{
    internal async Task ClickTrainingProviderHistoryTab() => await page.GetByRole(AriaRole.Link, new() { Name = "Training provider history" }).ClickAsync();

    internal async Task TrainingProviderHistoryIsDisplayed() => await Assertions.Expect(page.Locator("#tab-provider-history")).ToContainTextAsync("Training provider history");
}