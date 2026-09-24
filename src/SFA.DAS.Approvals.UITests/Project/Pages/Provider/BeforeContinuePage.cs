namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ILRAddLearnersPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add learners from ILR");

        internal async Task ClickOnContinueButton() => await ClickOnButton("Continue");

    }
}