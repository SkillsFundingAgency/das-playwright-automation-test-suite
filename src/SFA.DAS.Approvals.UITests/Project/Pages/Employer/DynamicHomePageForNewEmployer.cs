
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{

    // Dynamic homepage screen
    internal class SetupAnApprenticeshipDynamicHomepage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#set-up-an-apprenticeship")).ToContainTextAsync("Set up an apprenticeship");

        internal async Task<DoYouKnowWhichCourseYourApprenticeWillTakePage> StartNow()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();
            return await VerifyPageAsync(() => new DoYouKnowWhichCourseYourApprenticeWillTakePage(context));
        }

    }

    // Dynamic homepage screen
    internal class ContinueSettingupAnApprenticeshipDynamicHomepage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#call-to-action-continue-setting-up-an-apprenticeship")).ToContainTextAsync("Continue setting up an apprenticeship");

        internal async Task<DoYouNeedToCreateAdvertForThisApprenticeship> Continue()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new DoYouNeedToCreateAdvertForThisApprenticeship(context));
        }

    }

    // Dynamic homepage screen
    internal class ReviewApprenticeDetailsDynamicHomepage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#call-to-action-your-apprentice-status")).ToContainTextAsync("Your apprentice");

        internal async Task<EmployerApproveApprenticeDetailsPage> ReviewApprenticeDetails()
        {
            await page.Locator("//a[contains(.,'Review apprentice details')]").ClickAsync();
            return await VerifyPageAsync(() => new EmployerApproveApprenticeDetailsPage(context));
        }

    }

    internal class ViewApprenticeDetailsDynamicHomepage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#call-to-action-your-apprentice")).ToContainTextAsync("Your apprentice");

        internal async Task ViewApprenticeDetails()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "View apprentice details" }).First.ClickAsync();
        }

    }
}