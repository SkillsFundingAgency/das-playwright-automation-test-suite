namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ChooseConnection(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator continueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

        public override  async Task VerifyPage()    
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Choose a connection");
        }


        internal async Task<ChooseYourMainTrainingProviderPage> SelectConnection(string employerName)
        {
            await page.GetByLabel(employerName).ClickAsync();
            await continueButton.ClickAsync();
            return new ChooseYourMainTrainingProviderPage(context);
        }
    }
}
