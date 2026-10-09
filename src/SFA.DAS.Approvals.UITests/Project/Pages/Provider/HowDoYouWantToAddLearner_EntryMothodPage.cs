namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class HowDoYouWantToAddLearner_EntryMothodPage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        #region locators
        private ILocator optionToSelectApprenticesFromILR => page.Locator("text=Add 1 or more learners from ILR (individual learner record)"); 
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
            var page1 = await page.SelectOptionToAddOneLearnerAtATime();
            await page1.ClickOnContinueButton();
            return await VerifyPageAsync(() => new DoYouWantToCreateANewCohortPage(context));
        }

        internal async Task<UsingFileUploadPage> SelectOptionToUploadCsvFile()
        {
            await optionToUploadACsvFile.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new UsingFileUploadPage(context));
        }

        internal async Task<ProviderChooseAReservationPage> SelectOptionToAddApprenticesFromILRList_SelectReservationRoute()
        {
            var page = await SelectOptionToAddApprenticeFromILRAndContinue();
            var page1 = await page.SelectOptionToAddOneLearnerAtATime();
            await page1.ClickOnContinueButton();
            return await VerifyPageAsync(() => new ProviderChooseAReservationPage(context));
        }

        internal async Task SelectOptionToAddApprenticesFromILRList_InsufficientPermissionsRoute()
        {
            await optionToSelectApprenticesFromILR.CheckAsync();
            await ContinueButton.ClickAsync();
        }

        internal async Task<HowManyLearnersWouldYouLikeToAddPage> SelectOptionToAddApprenticeFromILRAndContinue()
        {
            await optionToSelectApprenticesFromILR.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new HowManyLearnersWouldYouLikeToAddPage(context));
        }

        internal async Task<FundingRestrictionsPage> SelectOptionToAddApprenticesFromILRList_FundingRestrictionsRoute()
        {
            await optionToSelectApprenticesFromILR.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new FundingRestrictionsPage(context));
        }

    }

}
