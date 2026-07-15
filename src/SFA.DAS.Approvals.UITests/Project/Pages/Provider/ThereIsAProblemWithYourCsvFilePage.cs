using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
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
}
