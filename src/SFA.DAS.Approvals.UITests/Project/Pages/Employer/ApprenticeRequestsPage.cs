namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ApprenticeRequestsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentice requests");
        }

        internal async Task<EmployerApproveApprenticeDetailsPage> OpenApprenticeRequestReadyForReview(string cohortRef)
        {
            await page.Locator($"tr[data-cohort='{cohortRef}'] a.govuk-link").ClickAsync();

            return await VerifyPageAsync(() => new EmployerApproveApprenticeDetailsPage(context));
        }




    }
}
