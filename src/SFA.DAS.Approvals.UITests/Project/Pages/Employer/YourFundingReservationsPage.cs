namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class YourFundingReservationsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your funding reservations");
        }

        public async Task<YouCannotCreateAnotherFundingReservationPage> TryClickOnReserveMoreFundingLink()
        {
            await page.Locator("a", new() { HasTextString = "Reserve more funding" }).ClickAsync();
            return await VerifyPageAsync(() => new YouCannotCreateAnotherFundingReservationPage(context));
        }
    }
}
