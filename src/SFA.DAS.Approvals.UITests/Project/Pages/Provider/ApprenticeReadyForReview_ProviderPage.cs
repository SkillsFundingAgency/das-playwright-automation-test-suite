namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ApprenticeReadyForReview_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentice requests");
        }


    }
}
