namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ReserveFundingForNonLevyEmployersPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Reserve funding for non-levy employers");
        }

        internal async Task<ChooseAnEmployerPage> ClickOnReserveFundingButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Reserve funding" }).ClickAsync();
            
            return await VerifyPageAsync(() => new ChooseAnEmployerPage(context));
        }
    }
}
