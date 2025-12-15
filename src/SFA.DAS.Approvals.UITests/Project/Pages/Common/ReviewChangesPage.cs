namespace SFA.DAS.Approvals.UITests.Project.Pages.Common
{
    internal class ReviewChangesPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Review changes");
        }

        internal async Task SelectReviewChangesOptions() => await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        internal async Task ClickOnCancelAndReturn() => await page.GetByRole(AriaRole.Link, new() { Name = "Cancel and return" }).ClickAsync();





    }
}
