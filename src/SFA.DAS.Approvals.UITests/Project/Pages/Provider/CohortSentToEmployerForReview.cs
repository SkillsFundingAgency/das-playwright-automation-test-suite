namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class CohortSentToEmployerForReview(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Cohort sent to employer for review");
        }       

    }
}
