using SFA.DAS.EmployerPortal.UITests.Project;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class EmployerHomePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync(objectContext.GetOrganisationName());
        }

        internal async Task<YourFundingReservationsPage> ClickOnFundingReservationsLink()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Funding reservations" }).ClickAsync();
            return await VerifyPageAsync(() => new YourFundingReservationsPage(context));
        }
    }
}
