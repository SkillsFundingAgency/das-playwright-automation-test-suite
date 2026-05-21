namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ConfirmYourReservationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check and confirm your reservation");
        }

        internal async Task<YouHaveReservedFundingForTrainingPage> ClickConfirmButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
            
            return await VerifyPageAsync(() => new YouHaveReservedFundingForTrainingPage(context));
        }


    }
}
