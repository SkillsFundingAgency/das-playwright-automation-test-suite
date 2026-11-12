using System;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class UploadCsvFilePage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        private ILocator PageHeader => page.Locator(".govuk-label--xl");
        private ILocator ContinueButton => page.Locator("//button[contains(text(),'Continue')]");
        private ILocator ChooseFileButton => page.Locator("#attachment");
        private ILocator UploadFileButton => page.Locator("#submit-upload-apprentices");
        private ILocator CancelUploadLink => page.Locator("#cancel-upload-link");

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

        internal async Task<ThereIsAProblemWithYourCsvFilePage> UploadFileErrorCase(string filePath)
        {
            await ChooseFileButton.SetInputFilesAsync(filePath);
            await UploadFileButton.ClickAsync();
            return await VerifyPageAsync(() => new ThereIsAProblemWithYourCsvFilePage(context));
        }

        internal async Task CancelUpload() => await CancelUploadLink.ClickAsync();
    }



    internal class ThereIsAProblemWithYourCsvFilePage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("There is a problem with your CSV file");
        }

        internal async Task ValidateErrorMessage(string errornousRow)
        {
            await Assertions.Expect(page.GetByLabel("There is a problem")).ToContainTextAsync("There is a problem You need to correct the errors and upload the file again");
            await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync(errornousRow);
        }

        internal async Task<ThereIsAProblemWithYourCsvFilePage> TryReUploadFile(string filePath)
        {
            await page.SetInputFilesAsync("#attachment", filePath);
            await page.ClickAsync("#submit-upload-apprentices");
            return this;
        }
    }

    internal class FileUploadCancelledPage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("File upload cancelled");
        }
    }
}
