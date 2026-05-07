namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ConfirmYourReservationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check details and reserve funding");
        }

        internal async Task<YouHaveSuccessfullyReservedFundingForApprenticeshipTrainingPage> ClickConfirmButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
            
            return await VerifyPageAsync(() => new YouHaveSuccessfullyReservedFundingForApprenticeshipTrainingPage(context));
        }


    }
}
