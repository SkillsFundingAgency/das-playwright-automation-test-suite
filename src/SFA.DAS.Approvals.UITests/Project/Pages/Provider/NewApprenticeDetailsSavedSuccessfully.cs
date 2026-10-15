namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class NewApprenticeDetailsSavedSuccessfully(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("New apprentice details saved successfully");
        }
    }
}
