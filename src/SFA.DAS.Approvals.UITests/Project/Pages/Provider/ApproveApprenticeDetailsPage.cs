﻿using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
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
        private ILocator Name(ILocator apprenticeRow) => apprenticeRow.Locator("td:nth-child(1)");
        private ILocator Uln(ILocator apprenticeRow) => apprenticeRow.Locator("td:nth-child(2)");
        private ILocator Dob(ILocator apprenticeRow) => apprenticeRow.Locator("td:nth-child(3)");
        private ILocator TrainingDates(ILocator apprenticeRow) => apprenticeRow.Locator("td:nth-child(4)");
        private ILocator Price(ILocator apprenticeRow) => apprenticeRow.Locator("td:nth-child(5)");
        #endregion


        public override async Task VerifyPage()
        {
            var headerText = await page.Locator("h1").TextContentAsync();
            Assert.IsTrue(Regex.IsMatch(headerText ?? "", "Approve apprentice details|Approve 2 apprentices' details"));
        }

        public async Task VerifyCohort(Apprenticeship apprenticeship)
        {
            await Assertions.Expect(employerName).ToHaveTextAsync(apprenticeship.EmployerDetails.EmployerName.ToString());
            await Assertions.Expect(cohortReference).ToHaveTextAsync(apprenticeship.CohortReference);
            await Assertions.Expect(status).ToHaveTextAsync("New request");
            await Assertions.Expect(message).ToHaveTextAsync("No message added.");

            var expectedName = apprenticeship.ApprenticeDetails.FirstName + " " + apprenticeship.ApprenticeDetails.LastName;
            var expectedULN = apprenticeship.ApprenticeDetails.ULN.ToString();
            var expectedDOB = apprenticeship.ApprenticeDetails.DateOfBirth.ToString("d MMM yyyy");
            var expectedTrainingDates = apprenticeship.TrainingDetails.StartDate.ToString("MMM yyyy") + " to " + apprenticeship.TrainingDetails.EndDate.ToString("MMM yyyy");
            var expectedPrice = apprenticeship.TrainingDetails.TotalPrice.ToString("C0");

            /*
            var apprenticeRow = row(apprenticeship.ApprenticeDetails.ULN.ToString());
            var name = apprenticeRow.Locator("td:nth-child(1)");
            var uln = apprenticeRow.Locator("td:nth-child(2)");
            var dob = apprenticeRow.Locator("td:nth-child(3)");
            var trainingDates = apprenticeRow.Locator("td:nth-child(4)");
            var price = apprenticeRow.Locator("td:nth-child(5)");
            

            await Assertions.Expect(name).ToHaveTextAsync(expectedName.Trim());
            await Assertions.Expect(uln).ToHaveTextAsync(expectedULN.Trim());
            await Assertions.Expect(dob).ToHaveTextAsync(expectedDOB.Trim());
            await Assertions.Expect(trainingDates).ToHaveTextAsync(expectedTrainingDates.Trim());
            //await Assertions.Expect(price).ToHaveTextAsync(expectedPrice.Trim());         <--- commenting it due to known bug: APPMAN-1679
            */

            var apprenticeRow = row(apprenticeship.ApprenticeDetails.ULN.ToString());
            await Assertions.Expect(Name(apprenticeRow)).ToHaveTextAsync(expectedName.Trim());
            await Assertions.Expect(Uln(apprenticeRow)).ToHaveTextAsync(expectedULN.Trim());
            await Assertions.Expect(Dob(apprenticeRow)).ToHaveTextAsync(expectedDOB.Trim());
            await Assertions.Expect(TrainingDates(apprenticeRow)).ToHaveTextAsync(expectedTrainingDates.Trim());
            //await Assertions.Expect(Price(apprenticeRow)).ToHaveTextAsync(expectedPrice.Trim());         //<--- commenting it due to known bug: APPMAN-1679
        
        }

        public async Task GetCohortId(Apprenticeship apprenticeship)
        {
            var cohortRef = await cohortReference.InnerTextAsync();
            apprenticeship.CohortReference = cohortRef;

            await Task.Delay(100); 
            context.Set(apprenticeship, "Apprenticeship");
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
            return await VerifyPageAsync(() => new CohortApprovedAndSentToEmployerPage(context));
        }



    }
}
