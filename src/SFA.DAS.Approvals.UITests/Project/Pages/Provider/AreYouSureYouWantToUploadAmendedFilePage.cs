namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class AreYouSureYouWantToUploadAmendedFilePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are you sure you want to upload an amended file?");
        }

        private ILocator ContinueButton => page.GetByText("Continue");
        private ILocator ConfirmTrueRadioButton => page.Locator("#confirm-true");
        private ILocator ConfirmFalseRadioButton => page.Locator("#confirm-false");


        internal async Task SelectYesAndContinue()
        {
            await ConfirmTrueRadioButton.ClickAsync();
            await ContinueButton.ClickAsync();
        }

        internal async Task SelectNoAndContinue()
        {
            await ConfirmFalseRadioButton.ClickAsync();
            await ContinueButton.ClickAsync();
        }
    }
}

