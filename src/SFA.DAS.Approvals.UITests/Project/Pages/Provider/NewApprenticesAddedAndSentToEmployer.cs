namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class NewApprenticesAddedAndSentToEmployer(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("New apprentices added and sent to employer(s) for approval");
        }
    }
}
