namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class AddApprenticeDetails_SelectJourneyPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator optionToCreateANewCohort => page.GetByRole(AriaRole.Radio, new() { Name = "Yes, create a new cohort" });
        private ILocator optionToAddToAnExistingCohort => page.GetByRole(AriaRole.Radio, new() { Name = "No, add to an existing cohort" });
        private ILocator continueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        #endregion
        
        
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Do you want to create a new cohort?");
        }

        internal async Task<ChooseAnEmployerPage> SelectOptionCreateANewCohort()
        {
            await optionToCreateANewCohort.CheckAsync();
            await continueButton.ClickAsync();
            return await VerifyPageAsync(() => new ChooseAnEmployerPage(context));
        }

        internal async Task<ChooseACohortPage> SelectOptionUseExistingCohort()
        {
            await optionToAddToAnExistingCohort.CheckAsync();
            await continueButton.ClickAsync();
            return await VerifyPageAsync(() => new ChooseACohortPage(context));
        }

        internal async Task<bool> IsAddToAnExistingCohortOptionDisplayed() => await optionToAddToAnExistingCohort.IsVisibleAsync();
        
        internal async Task<bool> IsCreateANewCohortOptionDisplayed() => await optionToCreateANewCohort.IsVisibleAsync();

        internal async Task AssertProviderPermissionsMsg() => await Assertions.Expect(page.GetByRole(AriaRole.Group)).ToContainTextAsync("You do not have permission to create cohorts. You will need to ask the employer to give you permission to do this on their behalf.");

    }
}
