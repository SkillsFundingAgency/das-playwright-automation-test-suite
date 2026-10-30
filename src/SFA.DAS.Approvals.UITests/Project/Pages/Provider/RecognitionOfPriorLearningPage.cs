namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class RecognitionOfPriorLearningPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Recognition of prior learning (RPL)");
        }

        internal async Task<ApproveApprenticeDetailsPage> SelectNoForRPL()
        {
            await page.GetByText("No", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

            return await VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

        internal async Task<ApproveApprenticeDetailsPage> SelectNoForManualEntryRPL()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "No", Exact = true }).CheckAsync();
            await page.Locator("#main-content .govuk-button").ClickAsync();
            var review = new ApproveApprenticeDetailsPage(context);
            await review.VerifyManualEntryPage();
            return review;
        }

        internal async Task<AddRecognitionOfPriorLearningDetailsPage> SelectYesForRPL()
        {
            await page.GetByText("Yes", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

            return await VerifyPageAsync(() => new AddRecognitionOfPriorLearningDetailsPage(context));
        }
    }
}
