
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class DoYouKnowWhichApprenticeshipTrainingYourApprenticeWillTakePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Do you know which apprenticeship training your apprentice will take?");


        internal async Task<WhenWillTheApprenticeStartTheirApprenticeTraining> Yes()
        {
            await page.Locator("[value= 'true']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new WhenWillTheApprenticeStartTheirApprenticeTraining(context));
        }

        internal async Task<WhenWillTheApprenticeStartTheirApprenticeTraining> ReserveFundsAsync(string courseName)
        {
            await page.Locator("[value= 'true']").ClickAsync();
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Start typing to search" }).ClickAsync();
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Start typing to search" }).FillAsync(courseName.Substring(0, 3));
            await page.GetByRole(AriaRole.Option, new() { Name = courseName }).First.ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new WhenWillTheApprenticeStartTheirApprenticeTraining(context));
        }
    }
}