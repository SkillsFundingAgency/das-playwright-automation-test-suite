namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ChooseReservationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator continueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

        public override  async Task VerifyPage()    
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Choose a reservation");
        }

        internal async Task<SelectLearnerFromILRPage> SelectNewReservation()
        {
            await page.Locator("#CreateNew").ClickAsync();
            await continueButton.ClickAsync();
            return await VerifyPageAsync(() => new SelectLearnerFromILRPage(context));
        }

    }
}
