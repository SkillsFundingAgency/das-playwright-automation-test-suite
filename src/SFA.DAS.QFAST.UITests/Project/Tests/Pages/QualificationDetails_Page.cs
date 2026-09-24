using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application;
using System.Text.RegularExpressions;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages
{
    public class QualificationDetails_Page (ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Qualification Details" })).ToBeVisibleAsync();
        private readonly Application_Details_Page application_Details_Page = new(context);
        private readonly DfeFundigReview_Page dfeFundigReview_Page = new(context);
        private readonly NewQualifications_Page newQualifications_Page = new(context);

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
        public async Task VerifyStatusOfMVS1Qualification(string expectedStatus)
        {
            var statusField = page.Locator(".govuk-summary-list__row:has(dt:text-is('Status')) dd.govuk-summary-list__value");
            var actualStatus = (await statusField.InnerTextAsync()).Trim();
            if (!string.Equals(actualStatus, expectedStatus, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                    $"Expected qualification status to be '{expectedStatus}', but the actual status is '{actualStatus}'.");
            }
        }
        public async Task SetStatusOfQualificaiton(string status)
        {
            await page.Locator("#AdditionalActions_ProcessStatusId").SelectOptionAsync(status);
            await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        }
        public async Task ReviewAndApproveQualification()
        {
            await page.Locator("tbody.govuk-table__body tr").Nth(1).Locator("td").Nth(2).Locator("a").ClickAsync();            
            await application_Details_Page.ClickOnDfeFundingReviewButton();
            await dfeFundigReview_Page.ApproveTheQualification();
            await dfeFundigReview_Page.SelectFundingStreamForQualification();
            await dfeFundigReview_Page.SetFundingStreamsAndApprovedTheQualification();
            await SetStatusOfQualificaiton("Approved");
            await VerifyStatusOfMVS1Qualification("Approved");
        }
    }
}
