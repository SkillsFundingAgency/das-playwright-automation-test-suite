namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class UploadCsvFilePage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Upload a CSV file");
        }

        internal async Task UploadFile(string filePath)
        {
            await page.SetInputFilesAsync("#attachment", filePath);
            await page.ClickAsync("#submit-upload-apprentices");
        }

        internal async Task ValidateErrorMessage(string errornousRow)
        {
            await Assertions.Expect(page.GetByLabel("There is a problem")).ToContainTextAsync("There is a problem You need to correct the errors and upload the file again");
            await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync(errornousRow);

        }
    }
}
