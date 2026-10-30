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

        internal async Task<ManualApprenticeshipCoursePage> SelectEmployerManualEntry()
        {
            await SelectEmployerAddApprencticesAndSend();
            return await VerifyPageAsync(() => new ManualApprenticeshipCoursePage(context, false));
        }

        internal async Task<ManualCohortConfirmationPage> SendManualEntryRequestToProvider(string message)
        {
            await page.Locator("#WhoIsAddingApprentices-Provider").CheckAsync();
            await page.Locator("[name='message']").FillAsync(message);
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();
            var sent = await VerifyPageAsync(() => new ManualCohortConfirmationPage(context, "Add apprentice request sent to training provider"));
            await Assertions.Expect(page.Locator(".govuk-summary-list__row:nth-child(3) > .govuk-summary-list__value")).ToHaveTextAsync(message);
            return sent;
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
