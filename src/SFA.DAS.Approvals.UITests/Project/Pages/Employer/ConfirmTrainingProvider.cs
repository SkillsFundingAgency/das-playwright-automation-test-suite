namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ChooseYourMainTrainingProvidePage(ScenarioContext context, string pageTitle) : ApprovalsBasePage(context)
    {
        public override  async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync(pageTitle);
        }

        internal async Task<ConfirmAddApprentices> ConfirmTrainingProviderDetails()
        {
            await page.Locator("#UseThisProvider").CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new ConfirmAddApprentices(context));
        }
    }
}
