
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    // Dynamic homepage screen
    internal class YourApprenticeDynamicHomepage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#call-to-action-your-apprentice-status")).ToContainTextAsync("Your apprentice");

        internal async Task<EmployerApproveApprenticeDetailsPage> ReviewApprenticeDetails()
        {
            await page.Locator("//a[contains(.,'Review apprentice details')]").ClickAsync();
            return await VerifyPageAsync(() => new EmployerApproveApprenticeDetailsPage(context));
        }

    }
}