
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Are you setting up an apprenticeship for an existing employee?");


        internal async Task<SetUpAnApprenticeshipForExistingEmployeePage> Yes()
        {
            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetUpAnApprenticeshipForExistingEmployeePage(context));
        }


        internal async Task<SetUpAnApprenticeshipForNewEmployeePage> No()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetUpAnApprenticeshipForNewEmployeePage(context));
        }
    }
}