namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class AddApprenticeDetails_EntryMothodPage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        #region locators
        private ILocator optionToSelectApprenticesFromILR => page.Locator("text=Choose details from ILR (individual learner record)"); 
        private ILocator optionToUploadACsvFile => page.Locator("text=Upload a CSV file");
        private ILocator ContinueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How do you want to add learner details?");
        }

        internal async Task<DoYouWantToCreateANewCohortPage> SelectOptionToApprenticesFromILR()
        {
            var page = await SelectOptionToAddApprenticeFromILRAndContinue();
            await page.ClickOnContinueButton();
            return await VerifyPageAsync(() => new DoYouWantToCreateANewCohortPage(context));
        }

        internal async Task<UsingFileUploadPage> SelectOptionToUploadCsvFile()
        {
            await optionToUploadACsvFile.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new UsingFileUploadPage(context));
        }

        internal async Task<ProviderSelectAReservationPage> SelectOptionToAddApprenticesFromILRList_SelectReservationRoute()
        {
            var page = await SelectOptionToAddApprenticeFromILRAndContinue();
            await page.ClickOnContinueButton();
            return await VerifyPageAsync(() => new ProviderSelectAReservationPage(context));
        }

        internal async Task SelectOptionToAddApprenticesFromILRList_InsufficientPermissionsRoute()
        {
            await optionToSelectApprenticesFromILR.CheckAsync();
            await ContinueButton.ClickAsync();
        }

        internal async Task<BeforeContinuePage> SelectOptionToAddApprenticeFromILRAndContinue()
        {
            await optionToSelectApprenticesFromILR.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new BeforeContinuePage(context));
        }

        internal async Task<FundingRestrictionsPage> SelectOptionToAddApprenticesFromILRList_FundingRestrictionsRoute()
        {
            await optionToSelectApprenticesFromILR.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new FundingRestrictionsPage(context));
        }

    }

}
