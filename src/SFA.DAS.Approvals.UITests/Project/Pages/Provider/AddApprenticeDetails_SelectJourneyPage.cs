namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class AddApprenticeDetails_SelectJourneyPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add apprentice details");
        }
    }
}
