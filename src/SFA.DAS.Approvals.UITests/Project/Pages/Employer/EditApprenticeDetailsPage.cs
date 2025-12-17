using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class EditApprenticeDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        private ILocator NoRPlText => page.Locator("dd: has-text('This apprentice has no recognised prior learning'))");

        private ILocator priceReduced => page.Locator("dt:has-text('Off-the-job training time reduction due to prior learning') + dd");


        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Edit apprentice details");
        }

        internal async Task EditDoB(DateTime DoB)
        {
            await page.Locator("id=dob-day").FillAsync(DoB.Day.ToString("D2"));
            await page.Locator("id=dob-month").FillAsync(DoB.Month.ToString("D2"));
            await page.Locator("id=dob-year").FillAsync(DoB.Year.ToString("D4"));
        }

        internal async Task ClickUpdateDetailsButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Update details" }).ClickAsync();
        }

        internal async Task ValidateErrorMessage(string errorMsg, string locatorId)
        {
            await Assertions.Expect(page.GetByLabel("There is a problem")).ToContainTextAsync("There is a problem " + errorMsg);
            await Assertions.Expect(page.Locator("#error-message-" + locatorId)).ToContainTextAsync(errorMsg);

        }

        internal async Task<EmployerApproveApprenticeDetailsPage> NoRecognitionOfPriorLearning()
        {
            await Assertions.Expect(page.Locator("dd")).ToContainTextAsync("This apprentice has no recognised prior learning");
            await page.Locator("#continue-button").ClickAsync();
            return await VerifyPageAsync(() => new EmployerApproveApprenticeDetailsPage(context));
        }

        internal async Task<EmployerApproveApprenticeDetailsPage> RecognitionOfPriorLearning(Apprenticeship apprenticeship)
        {

            await new CommonStepsHelper(context).VerifyText(priceReduced, apprenticeship.RPLDetails.DurationReducedBy.ToString());

            await page.Locator("#continue-button").ClickAsync();
            return await VerifyPageAsync(() => new EmployerApproveApprenticeDetailsPage(context));
        }
    }
}
