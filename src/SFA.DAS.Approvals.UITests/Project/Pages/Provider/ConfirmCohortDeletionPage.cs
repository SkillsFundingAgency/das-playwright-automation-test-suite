namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ConfirmCohortDeletionPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Confirm cohort deletion");
        }

        internal async Task<ApprenticeRequests_ProviderPage> ConfirmDeletion()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, delete cohort" }).CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeRequests_ProviderPage(context));
        }


    }
}
