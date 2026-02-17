namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class SelectFundingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator continueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

        public override  async Task VerifyPage()    
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Select funding");
        }

        internal async Task ClickContinueButton()
        {
            await continueButton.ClickAsync();
        }

        internal async Task SelectFundingtype()
        {
            await page.GetByLabel("Select funding").SelectOptionAsync("Current levy funds");

            await ClickContinueButton();
        }

        internal async Task<SelectReservationPage> SelectReservedFunds()
        {
            await page.Locator("#FundingType-2").ClickAsync();
            await ClickContinueButton();
            return await VerifyPageAsync(() => new SelectReservationPage(context));
        }

        internal async Task<SelectFundingPage> SelectReserveNewFunds()
        {
            await page.Locator("#FundingType-3").ClickAsync();
            await ClickContinueButton();
            return await VerifyPageAsync(() => new SelectFundingPage(context));
        }

    }
}
