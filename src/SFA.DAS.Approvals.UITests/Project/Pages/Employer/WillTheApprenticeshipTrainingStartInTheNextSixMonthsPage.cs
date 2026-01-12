
using NUnit.Framework;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class WillTheApprenticeshipTrainingStartInTheNextSixMonthsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Will the apprenticeship training start in the next 6 months?");


        internal async Task<AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage> StartInSixMonths()
        {

            await page.Locator("[value= 'Yes']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new AreYouSettingUpAnApprenticeshipForAnExistingEmployeePage(context));
        }

        internal async Task<SetupAnApprenticeshipPage> DontStartInSixMonths()
        {
            await page.Locator("[value= 'No']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetupAnApprenticeshipPage(context));
        }

        internal async Task<SetupAnApprenticeshipPage> DontKnowWhenItStarts()
        {
            await page.Locator("[value= 'Unknown']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new SetupAnApprenticeshipPage(context));
        }
    }
}