namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ChooseFundingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator continueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

        public override  async Task VerifyPage()    
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Choose funding");
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

        internal async Task<ChooseReservationPage> SelectReservedFunds()
        {
            await page.Locator("#FundingType-2").ClickAsync();
            await ClickContinueButton();
            return await VerifyPageAsync(() => new ChooseReservationPage(context));
        }

        internal async Task<ChooseFundingPage> SelectReserveNewFunds()
        {
            await page.Locator("#FundingType-3").ClickAsync();
            await ClickContinueButton();
            return await VerifyPageAsync(() => new ChooseFundingPage(context));
        }

    }
}
