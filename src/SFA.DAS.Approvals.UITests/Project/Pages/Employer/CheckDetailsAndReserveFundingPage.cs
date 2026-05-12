namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class CheckDetailsAndReserveFundingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check details and reserve funding");
        }

        internal async Task<YouHaveSuccessfullyReservedFundingForApprenticeshipTrainingPage> ClickConfirmButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "like to reserve funding" }).ClickAsync();
            
            return await VerifyPageAsync(() => new YouHaveSuccessfullyReservedFundingForApprenticeshipTrainingPage(context));
        }


    }
}
