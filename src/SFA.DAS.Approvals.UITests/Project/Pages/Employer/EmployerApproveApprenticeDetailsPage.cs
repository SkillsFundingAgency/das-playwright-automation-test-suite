using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class EmployerApproveApprenticeDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator organisation => page.Locator("dt:has-text('Organisation') + dd");
        private ILocator reference => page.Locator("dt:has-text('Reference') + dd");
        private ILocator status => page.Locator("dt:has-text('Status') + dd");
        private ILocator messageFromProvider => page.Locator(".govuk-inset-text").First;
        private ILocator approveRadioOption => page.Locator("label:has-text('Yes, approve and notify training provider')");
        private ILocator doNotApproveRadioOption => page.Locator("label:has-text('No, request changes from training provider')");
        private ILocator messageToEmployerTextBox => page.Locator(".govuk-textarea").First;
        private ILocator SubmitButton => page.Locator("button:has-text('Submit')");
        private ILocator editLink => page.Locator(".edit-apprentice");
        private ILocator editLink2 => page.Locator("table tr").Nth(1).Locator(".edit-apprentice");
        private ILocator addAnotherApprenticeLink => page.Locator("a:has-text('Add another apprentice')");
        #endregion


        public override async Task VerifyPage()
        {
            var headerText = await page.Locator("h1").TextContentAsync();
            Assert.IsTrue(Regex.IsMatch(headerText ?? "", "Approve apprentice details|Approve 2 apprentices' details"));
        }

        internal async Task VerifyCohort(Apprenticeship apprenticeship)
        {
            await Assertions.Expect(organisation).ToHaveTextAsync(apprenticeship.EmployerDetails.EmployerName.ToString());
            await Assertions.Expect(reference).ToHaveTextAsync(apprenticeship.Cohort.Reference);
            await Assertions.Expect(status).ToHaveTextAsync(apprenticeship.Cohort.Status_Employer);
            await Assertions.Expect(messageFromProvider).ToHaveTextAsync("Please review the details and approve the request.");
        }

        internal async Task<ApprenticeDetailsApproved> EmployerApproveCohort()
        {
            await approveRadioOption.ClickAsync();
            await SubmitButton.ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeDetailsApproved(context));
        }

        internal async Task ValidateWarningMessageForFoundationCourses(string warningMsg) => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(warningMsg);

        internal async Task ValidateCohortStatus(string cohortStatus)
        {
            await new CommonStepsHelper(context).VerifyText(status, cohortStatus);
        }

        internal async Task VerifyRPLDetails(List<Apprenticeship> apprenticeships)
        {
            for (int i = 0; i < apprenticeships.Count; i++)
            {
                var apprentice = apprenticeships[i];
                if (!(apprentice.TrainingDetails.StandardCode is 805 or 806 or 807 or 808 or 809 or 810 or 811)) {
                    //RPL check does not appear for foundation courses
                  
                    var rplAvailable = page.Locator($"//td[contains(.,'{apprentice.ApprenticeDetails.FirstName}')]/following-sibling::td[contains(., 'Yes')]/following-sibling::td/a[contains(., 'View')]");

                    if(await rplAvailable.IsVisibleAsync())
                    {
                        await rplAvailable.ClickAsync();
                        var page1 = await new EditApprenticeDetailsPage(context).RecognitionOfPriorLearning(apprentice);
                    }

                }
            }           
        }

        internal async Task<ApprenticeRequestsPage> TryClickAddAnotherApprenticeLink()
        {
            await addAnotherApprenticeLink.ClickAsync();
            var page = await VerifyPageAsync(() => new YouCannotCreateAnotherFundingReservationPage(context));
            await page.ClickOnApprenticeRequestsLink();
            return await VerifyPageAsync(() => new ApprenticeRequestsPage(context));
        }
    }
}
