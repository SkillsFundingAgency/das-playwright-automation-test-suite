using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application;
using System.Text.RegularExpressions;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages
{
    public class QualificationDetails_Page (ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Qualification Details" })).ToBeVisibleAsync();
        public async Task<Application_Details_Page> ClickOnFirstAssociatedApplication()
        {
            var firstApplicationLink = page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex("^View application") }).First;
            await firstApplicationLink.ClickAsync();
            return await VerifyPageAsync(() => new Application_Details_Page(context));
        }
        public async Task ClickOnBackLink()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();
        }
        public async Task VerifyStatusOfQualification(string expectedStatus)
        {
            await page.Locator("td").Nth(2).Locator("a").ClickAsync();
            var statusField = page.Locator(".govuk-summary-list__row:has(dt:text-is('Status')) dd.govuk-summary-list__value");
            await Assertions.Expect(statusField).ToHaveTextAsync(expectedStatus);
        }
    }
}
