using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ApproveApprenticeDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator employerName => page.Locator("dt:has-text('Employer') + dd");
        private ILocator cohortReference => page.Locator("dt:has-text('Cohort reference') + dd");
        private ILocator status => page.Locator("dt:has-text('Status') + dd");
        private ILocator message => page.Locator("h2:has-text('Message') + div.govuk-inset-text");
        private ILocator row(string ULN) => page.Locator($"table tbody tr:has-text('{ULN}')");
        private ILocator AddAnotherApprenticeLink => page.Locator("a:has-text('Add another apprentice')");
        private ILocator DeleteThisCohortLink => page.Locator("a:has-text('Delete this cohort')");
        private ILocator approveRadioOption => page.Locator("label:has-text('Yes, approve and notify employer')");
        private ILocator doNotApproveRadioOption => page.Locator("label:has-text('No, save and return to apprentice requests')");
        private ILocator messageToEmployerTextBox => page.Locator(".govuk-textarea").First;
        private ILocator saveAndSubmitButton => page.Locator("button:has-text('Save and submit')");
        private ILocator saveAndexitLink => page.Locator("a:has-text('Save and exit')");
        #endregion


        public override async Task VerifyPage()
        {
            var headerText = await page.Locator("h1").TextContentAsync();
            Assert.IsTrue(Regex.IsMatch(headerText ?? "", "Approve apprentice details|Approve 2 apprentices' details"));
        }

        public async Task VerifyCohort(Apprenticeship apprenticeship)
        {
            await Assertions.Expect(employerName).ToHaveTextAsync(apprenticeship.EmployerDetails.EmployerName.ToString());
            await Assertions.Expect(status).ToHaveTextAsync("New request");
            await Assertions.Expect(message).ToHaveTextAsync("No message added.");

            var cohortRef = await cohortReference.InnerTextAsync();
            apprenticeship.CohortReference = cohortRef;
            context.Set(apprenticeship, "Apprenticeship");
        }

        public async Task VerifyCohort2(Apprenticeship apprenticeship)
        {
            /*
            await Assertions.Expect(employerName).ToHaveTextAsync(apprenticeship.EmployerDetails.EmployerName.ToString());
            await Assertions.Expect(status).ToHaveTextAsync("New request");
            await Assertions.Expect(message).ToHaveTextAsync("No message added.");

            var cohortRef = await cohortReference.InnerTextAsync();
            apprenticeship.CohortReference = cohortRef;
            context.Set(apprenticeship, "Apprenticeship");
            */

            var expectedName = apprenticeship.ApprenticeDetails.FirstName + " " + apprenticeship.ApprenticeDetails.LastName;
            var expectedULN = apprenticeship.ApprenticeDetails.ULN.ToString();
            var expectedDOB = apprenticeship.ApprenticeDetails.DateOfBirth.ToString("d MMM yyyy");
            var expectedTrainingDates = apprenticeship.TrainingDetails.StartDate.ToString("MMM yyyy") + " to " + apprenticeship.TrainingDetails.EndDate.ToString("MMM yyyy");
            var expectedPrice = apprenticeship.TrainingDetails.TotalPrice.ToString("C0");   //ToString("C0", new CultureInfo("en-GB")

            var x = row(apprenticeship.ApprenticeDetails.ULN.ToString());
            var name = x.Locator("td:nth-child(1)");
            var uln = x.Locator("td:nth-child(2)");
            var dob = x.Locator("td:nth-child(3)");
            var trainingDates = x.Locator("td:nth-child(4)");
            var price = x.Locator("td:nth-child(5)");

            await Assertions.Expect(name).ToHaveTextAsync(expectedName);
            await Assertions.Expect(uln).ToHaveTextAsync(expectedULN);
            await Assertions.Expect(dob).ToHaveTextAsync(expectedDOB);
            await Assertions.Expect(trainingDates).ToHaveTextAsync(expectedTrainingDates);
            await Assertions.Expect(price).ToHaveTextAsync(expectedPrice);

            Debugger.Break();


        }

        public async Task<string> GetCohortId(Apprenticeship apprenticeship)
        {
            var cohortRef = await cohortReference.InnerTextAsync();
            apprenticeship.CohortReference = cohortRef;
            context.Set(apprenticeship, "Apprenticeship");
            return cohortRef;
        }

        public async Task<AddApprenticeDetails_EntryMothodPage> ClickOnAddAnotherApprenticeLink()
        {
            await AddAnotherApprenticeLink.ClickAsync();
            return new AddApprenticeDetails_EntryMothodPage(context);
        }

        public async Task<CohortApprovedAndSentToEmployerPage> ProviderApproveCohort()
        {
            await approveRadioOption.ClickAsync();
            await messageToEmployerTextBox.FillAsync("Please review the details and approve the request.");
            await saveAndSubmitButton.ClickAsync();
            return new CohortApprovedAndSentToEmployerPage(context);
        }



    }
}
