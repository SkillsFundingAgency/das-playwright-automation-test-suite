namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ConfirmLearnerRemovalPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Confirm learner removal");
        }

        internal async Task<ApproveApprenticeDetailsPage> ConfirmRemoval()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, remove the record" }).CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

    }
}
