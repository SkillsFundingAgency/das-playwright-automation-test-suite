namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ProviderDeleteReservationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Are you sure you want to delete this reservation?");
        }

    }
}
