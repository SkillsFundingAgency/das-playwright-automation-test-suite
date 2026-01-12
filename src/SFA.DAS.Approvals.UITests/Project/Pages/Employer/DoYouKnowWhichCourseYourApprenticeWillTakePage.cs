
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class DoYouKnowWhichCourseYourApprenticeWillTakePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Do you know which course your apprentice will take?");


        public async Task<HaveYouChosenATrainingProviderToDeliverTheApprenticeshipTrainingPage> Yes()
        {
            
            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new HaveYouChosenATrainingProviderToDeliverTheApprenticeshipTrainingPage(context));
        }


        public async Task<SetupAnApprenticeshipPage> No()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetupAnApprenticeshipPage(context));
        }
    }
}