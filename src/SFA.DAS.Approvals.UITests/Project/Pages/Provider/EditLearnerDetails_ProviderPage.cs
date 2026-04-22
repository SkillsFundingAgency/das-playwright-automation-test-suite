using System;
using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class EditLearnerDetails_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync(new Regex(@"Edit (learner|apprentice) details"));
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

        internal async Task ValidateEditability(Table table)
        {
            foreach (var row in table.Rows)
            {
                var locator = page.Locator(row["Locator"]);
                var isEditable = bool.Parse(row["Editable"]);

                Assert.IsTrue(await IsEditableAsync(locator) == isEditable);
       
            }

        }

        public async Task<bool> IsEditableAsync(ILocator locator)
        {
            // If element is hidden, it's not editable
            if (!await locator.IsVisibleAsync())
                return false;

            // If it's a form control, check disabled/readonly
            string tag = await locator.EvaluateAsync<string>("el => el.tagName.toLowerCase()");

            if (tag is "input" or "textarea" or "select")
            {
                bool disabled = await locator.IsDisabledAsync();
                bool readOnly = await locator.EvaluateAsync<bool>("el => el.readOnly === true");
                return !disabled && !readOnly;
            }

            // If contenteditable
            bool contentEditable = await locator.EvaluateAsync<bool>("el => el.isContentEditable");
            if (contentEditable)
                return true;

            // Otherwise: not an editable element
            return false;
        }

    }
}
