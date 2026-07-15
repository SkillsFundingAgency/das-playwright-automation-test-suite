namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class HowDoYouWantToAddLearnerDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How do you want to add learner details?");

        internal async Task<SelectLearnerFromILRPage> SelectOptionChooseDetailsFromILR()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Choose details from ILR" }).CheckAsync();
            await ClickOnButton("Continue");
            return await VerifyPageAsync(() => new SelectLearnerFromILRPage(context));
        }
       


    }
}
