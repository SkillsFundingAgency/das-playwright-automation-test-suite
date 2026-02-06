using SFA.DAS.Approvals.UITests.Project.Pages.Provider;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class SelectReservationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator continueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

        public override  async Task VerifyPage()    
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Select a Reservation");
        }

        internal async Task<SelectLearnerFromILRPage> SelectReservation()
        {
            await page.Locator("//*[@name=\"SelectedReservationId\" and not(@id='CreateNew')]").ClickAsync();
            await continueButton.ClickAsync();
            return await VerifyPageAsync(() => new SelectLearnerFromILRPage(context));
        }

    }
}
