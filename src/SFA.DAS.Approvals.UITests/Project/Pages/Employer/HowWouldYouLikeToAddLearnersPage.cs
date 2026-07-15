namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class HowWouldYouLikeToAddLearnersPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("How would you like to add learners?");
        }

        internal async Task<RequestSentToTrainingProviderPage> SelectProviderAddApprencticesAndSend()
        {
            await page.Locator("#WhoIsAddingApprentices-Provider").CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new RequestSentToTrainingProviderPage(context));
        }

        internal async Task SelectEmployerAddApprencticesAndSend()
        {
            await page.Locator("#WhoIsAddingApprentices").CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();            
        }

        internal async Task VerifyErrorMessage(string[] errorMsgs)
        { 
            var errorMessageLocator = page.Locator("#error-message-WhoIsAddingApprentices");
            foreach (var errorMsg in errorMsgs)
            {
                await Assertions.Expect(errorMessageLocator).ToContainTextAsync(errorMsg);
            }
        }

    }
}
