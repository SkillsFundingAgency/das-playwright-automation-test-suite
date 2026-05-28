namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class UploadCsvFilePage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Upload a CSV file");
        }

        internal async Task<ThereIsAProblemWithYourCsvFilePage> TryUploadFile(string filePath)
        {
            await page.SetInputFilesAsync("#attachment", filePath);
            await page.ClickAsync("#submit-upload-apprentices");
            return await VerifyPageAsync(() => new ThereIsAProblemWithYourCsvFilePage(context));
        }


    }
}
