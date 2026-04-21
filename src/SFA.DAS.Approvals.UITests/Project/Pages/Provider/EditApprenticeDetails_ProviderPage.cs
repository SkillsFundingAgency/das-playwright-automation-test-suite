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

        internal async Task ValidateEditability(Table table)
        {
            foreach (var row in table.Rows)
            {
                var locator = row["Locator"];
                var expected = bool.Parse(row["Editable"]);
                var actual = await isEditable(page, locator);

                Assert.AreEqual(expected, actual, $"Locator '{locator}' editability does not match expected value.");
            }
        }

        private async Task<bool> isEditable(IPage page, string locator)
        {
            var element = page.Locator(locator);

            // If element doesn't exist → treat as non-editable
            if (!(await element.CountAsync() == 0)) return false;

            // Hidden input → not editable
            var type = await element.GetAttributeAsync("type");
            if (type == "hidden") return false;

            // Not visible → not editable
            if (!(await element.IsVisibleAsync())) return false;
            // Disabled or readonly → not editable
            if (await element.IsDisabledAsync()) return false;
            if (await element.GetAttributeAsync("readonly") != null) return false;

            return true;
        }
    }
}
