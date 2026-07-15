namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class LearnerRequestsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Learner requests");
        }

        internal async Task<EmployerApproveLearnerDetailsPage> OpenApprenticeRequestReadyForReview(string cohortRef)
        {
            await page.Locator($"tr[data-cohort='{cohortRef}'] a.govuk-link").ClickAsync();

            return await VerifyPageAsync(() => new EmployerApproveLearnerDetailsPage(context));
        }

        internal async Task<EmployerApproveLearnerDetailsPage> GoToDraftsAndOpenFirstDetailsLink()
        {
            await page.Locator("#Draft").ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Details" }).First.ClickAsync();
            return await VerifyPageAsync(() => new EmployerApproveLearnerDetailsPage(context));
        }





        }
}
