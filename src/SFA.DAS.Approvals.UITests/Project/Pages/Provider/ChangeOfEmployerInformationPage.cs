namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ChangeOfEmployerInformationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Change of employer");
        }

        internal async Task ClickOnBackLink() => await page.Locator("#back-link").ClickAsync();
    }
}
