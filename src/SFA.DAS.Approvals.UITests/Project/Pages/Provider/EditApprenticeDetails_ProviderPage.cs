using System;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class EditApprenticeDetails_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Edit apprentice details");
        }

        internal async Task EditDoB(DateTime DoB)
        {
            await page.Locator("id=dob-day").FillAsync(DoB.Day.ToString("D2"));
            await page.Locator("id=dob-month").FillAsync(DoB.Month.ToString("D2"));
            await page.Locator("id=dob-year").FillAsync(DoB.Year.ToString("D4"));
        }

        internal async Task ClickUpdateDetailsButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Update details" }).ClickAsync();
        }

        internal async Task ValidateErrorMessage(string errorMsg, string locatorId)
        {
            await Assertions.Expect(page.GetByLabel("There is a problem")).ToContainTextAsync("There is a problem " + errorMsg);
            await Assertions.Expect(page.Locator("#error-message-" + locatorId)).ToContainTextAsync(errorMsg);
        }

        internal async Task ClickOnCancelAndReturnLink() => await page.GetByRole(AriaRole.Link, new() { Name = "Cancel and return" }).ClickAsync();
    }
}
