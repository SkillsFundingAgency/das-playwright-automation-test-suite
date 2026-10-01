namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class UploadCsvFilePage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        private ILocator PageHeader => page.Locator(".govuk-label--xl");
        private ILocator ContinueButton => page.Locator("//button[contains(text(),'Continue')]");
        private ILocator ChooseFileButton => page.Locator("#attachment");
        private ILocator UploadFileButton => page.Locator("#submit-upload-apprentices");

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Upload a CSV file");
        }

        internal async Task<ThereIsAProblemWithYourCsvFilePage> TryUploadFile(string filePath)
        {
            await ChooseFileButton.SetInputFilesAsync(filePath);
            await UploadFileButton.ClickAsync();
            return await VerifyPageAsync(() => new ThereIsAProblemWithYourCsvFilePage(context));
        }

        internal async Task<ReviewApprenticeDetailsBulkUploadPage> UploadFile(string filePath)
        {
            await ChooseFileButton.SetInputFilesAsync(filePath);
            await UploadFileButton.ClickAsync();
            return await VerifyPageAsync(() => new ReviewApprenticeDetailsBulkUploadPage(context));
        }


    }
}
