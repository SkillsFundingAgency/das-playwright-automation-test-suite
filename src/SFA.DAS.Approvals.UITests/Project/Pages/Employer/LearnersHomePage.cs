namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class LearnersHomePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Learners");
        }

        internal async Task<AddLearnerPage> GoToAddALearner()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Add a Learner or send a learner request" }).ClickAsync();
            return await VerifyPageAsync(() => new AddLearnerPage(context));            
        }

        internal async Task<ApprenticeRequestsPage> GoToReviewLearnerRequests()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Review learner requests" }).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeRequestsPage(context));
        }

        internal async Task<ApprenticeRequestsPage> GoToLearnerRequestsFromHome()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Learners" }).First.ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Review learner requests" }).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeRequestsPage(context));
        }

        internal async Task<ManageYourLearnersPage> GoToManageYourLearners()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Manage your Learners" }).ClickAsync();
            return await VerifyPageAsync(() => new ManageYourLearnersPage(context));
        }

    }
}
