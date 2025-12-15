namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class DetailsOfIlrDataMismatchPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Details of ILR data mismatch");
        }

        internal async Task SelectILRDataMismatchOptions() => await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        internal async Task ClickOnBackLink() => await page.GetByRole(AriaRole.Link, new() { Name = "Back to apprentice details" }).ClickAsync();

    }
}
