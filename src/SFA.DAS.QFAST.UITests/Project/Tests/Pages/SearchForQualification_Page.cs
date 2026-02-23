

using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages
{
    public class SearchForQualification_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Search for a qualification" })).ToBeVisibleAsync();
        public async Task<SearchForQualification_Page> ValidateErrorMessage()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
            var notext = page.Locator(".govuk-error-message:has-text(\"Enter a search term\")");
            await Assertions.Expect(notext).ToBeVisibleAsync();
            await page.Locator("#SearchTerm").FillAsync("elec");
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
            var noresult = page.Locator(".govuk-error-message:has-text(\"Search term is 1 character(s) too short.\")");
            await Assertions.Expect(noresult).ToBeVisibleAsync();
            await page.Locator("#SearchTerm").FillAsync("£$%$%");
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
            await Assertions.Expect(page.GetByText("No qualifications match your search criteria")).ToBeVisibleAsync();
            return await VerifyPageAsync(() => new SearchForQualification_Page(context));
        }
        public async Task<SearchForQualification_Page> SearchForQualificationUsingPartialTitle(string paritaltext, string actualtext)
        {
            await page.Locator("#SearchTerm").FillAsync(paritaltext);
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
            var rows = page.Locator("tbody.govuk-table__body tr.govuk-table__row");
            await rows.First.WaitForAsync();
            if (await rows.CountAsync() == 0)
            {
                throw new InvalidOperationException(
                    $"No search results returned for '{paritaltext}'.");
            }
            var firstResultTitle = page.Locator("tbody.govuk-table__body tr.govuk-table__row").First.Locator("td").Nth(1).Locator("a");
            var actualValue = await firstResultTitle.InnerTextAsync();
            if (!actualValue.Contains(actualtext, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"Expected first result to contain '{actualtext}', but found '{actualValue}'.");
            }
            // Fuzzy Search allowed with Two Incorrect Characters and not allowed more than two incorrect characters 
            await page.Locator("#SearchTerm").FillAsync("Mathmtics");
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
            var rows1 = page.Locator("tbody.govuk-table__body tr.govuk-table__row");
            await rows1.First.WaitForAsync();
            if (await rows1.CountAsync() == 0)
            {
                throw new InvalidOperationException(
                    "No search results returned for 'Mathematics'.");
            }
            var firstResultTitle1 = rows1.First.Locator("td").Nth(1).Locator("a");
            var actualValue1 = await firstResultTitle1.InnerTextAsync();
            if (!actualValue1.Contains("Mathematics", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"Expected first result to contain 'Mathematics', but found '{actualValue1}'.");
            }
            return await VerifyPageAsync(() => new SearchForQualification_Page(context));
        }
        public async Task<QualificationDetails_Page> ValidateQAN()
        {
            //white space outside the value should be trimmed from the search term
            await page.Locator("#SearchTerm").FillAsync("    10013398    ");
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
            var rows = page.Locator("tbody.govuk-table__body tr.govuk-table__row");
            await rows.First.WaitForAsync();
            var firstQanCell = rows.First.Locator("td").First;
            var qanValue = (await firstQanCell.InnerTextAsync()).Trim();
            if (qanValue != "10013398")
            {
                throw new InvalidOperationException(
                    $"Expected QAN to be '10013398', but found '{qanValue}'.");
            }
            // Qan should be searchable with slashes
            await page.Locator("#SearchTerm").FillAsync("100/1339/8");
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
            var rows1 = page.Locator("tbody.govuk-table__body tr.govuk-table__row");
            await rows.First.WaitForAsync();
            var firstQanCell1 = rows.First.Locator("td").First;
            var qanValue1 = (await firstQanCell.InnerTextAsync()).Trim();
            if (qanValue1 != "10013398")
            {
                throw new InvalidOperationException(
                    $"Expected QAN to be '10013398', but found '{qanValue}'.");
            }
            await page.GetByRole(AriaRole.Link, new() { Name = "RAD Advanced Vocational Graded Examination in Dance" }).ClickAsync();
            return await VerifyPageAsync(() => new QualificationDetails_Page(context));            
        }
    }
}
