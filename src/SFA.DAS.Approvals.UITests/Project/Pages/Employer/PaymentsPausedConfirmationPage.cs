
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class PaymentsPausedConfirmationPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;

        internal PaymentsPausedConfirmationPage(ScenarioContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Payments paused");
        }

        internal async Task ClickViewLearnerDetailLink()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "View learner details" }).ClickAsync();
        }


    }
}
