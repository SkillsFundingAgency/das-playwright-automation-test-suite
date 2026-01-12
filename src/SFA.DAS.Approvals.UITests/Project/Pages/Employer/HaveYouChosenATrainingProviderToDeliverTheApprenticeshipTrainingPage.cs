
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class HaveYouChosenATrainingProviderToDeliverTheApprenticeshipTrainingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Have you chosen a training provider to deliver the apprenticeship training?");


        internal async Task<WillTheApprenticeshipTrainingStartInTheNextSixMonthsPage> Yes()
        {
            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new WillTheApprenticeshipTrainingStartInTheNextSixMonthsPage(context));
        }

        internal async Task<SetupAnApprenticeshipPage> No()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetupAnApprenticeshipPage(context));
        }
    }
}