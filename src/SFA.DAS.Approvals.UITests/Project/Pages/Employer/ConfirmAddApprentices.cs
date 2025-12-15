namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ConfirmAddApprentices(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Start adding apprentices");
        }

        internal async Task<ConfirmRequestSentPage> SelectProviderAddApprencticesAndSend()
        {
            await page.Locator("#WhoIsAddingApprentices-Provider").CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new ConfirmRequestSentPage(context));
        }
    
    }
}
