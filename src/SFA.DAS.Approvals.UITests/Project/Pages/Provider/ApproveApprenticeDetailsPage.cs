using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ApproveApprenticeDetailsPage(ScenarioContext context) : ApprovalsBasePage(context), IPageWithABackLink

    {
        #region locators
        private ILocator banner => page.Locator("#main-content");
        private ILocator employerName => page.Locator("dt:has-text('Employer') + dd");
        private ILocator cohortReference => page.Locator("dt:has-text('Cohort reference') + dd");
        private ILocator status => page.Locator("dt:has-text('Status') + dd");
        private ILocator message => page.Locator("h2:has-text('Message') + div.govuk-inset-text");
        private ILocator row(string ULN) => page.Locator($"table tbody tr:has-text('{ULN}')");
        private ILocator viewLink(string name) => page.GetByRole(AriaRole.Link, new() { Name = $"View{name}" }).First;
        private ILocator deleteLink(string name) => page.Locator(".delete-apprentice").First;
        private ILocator AddAnotherApprenticeLink => page.Locator("a:has-text('Add another apprentice')");
        private ILocator DeleteThisCohortLink => page.GetByRole(AriaRole.Link, new() { Name = "Delete this cohort" }).First;
        private ILocator approveRadioOption => page.Locator("label:has-text('Yes, approve and notify employer')");
        private ILocator firstRadioOption => page.Locator("div.govuk-radios__item input[type='radio']").First;
        private ILocator doNotApproveRadioOption => page.Locator("label:has-text('No, save and return to apprentice requests')");
        private ILocator messageToEmployerTextBox => page.Locator(".govuk-textarea").First;
        private ILocator saveAndSubmitButton => page.Locator("button:has-text('Save and continue')");
        private ILocator ContinueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        private ILocator rplVerifiedCheckBox => page.Locator("#rplVerified");
        private ILocator AddRplLink => page.GetByRole(AriaRole.Link, new() { Name = "Add RPL" }).First;
        private ILocator saveAndexitLink => page.Locator("a:has-text('Save and exit')");
        private ILocator Name(ILocator apprenticeRow) => apprenticeRow.Locator("td:nth-child(1)");
        private ILocator Uln(ILocator apprenticeRow) => apprenticeRow.Locator("td:nth-child(2)");
        private ILocator Dob(ILocator apprenticeRow) => apprenticeRow.Locator("td:nth-child(3)");
        private ILocator TrainingDates(ILocator apprenticeRow) => apprenticeRow.Locator("td:nth-child(4)");
        private ILocator Price(ILocator apprenticeRow) => apprenticeRow.Locator("td:nth-child(5)");
        private ILocator sendToEmployerRadioOption => page.Locator("label:has-text('No, send to employer to review or add details')");
        private ILocator messageToEmployerToReviewTextBox => page.Locator(".govuk-textarea").Last;
        #endregion


        public override async Task VerifyPage()
        {
            var headerText = await page.Locator(".govuk-heading-xl").First.TextContentAsync();
            Assert.IsTrue(Regex.IsMatch(headerText ?? "", "Check apprentice details|Check 2 apprentices' details"));
        }

        public async Task ClickOnBackLinkAsync() => await page.Locator("a.govuk-back-link").ClickAsync();
        public async Task ClickContinueButtonAsync() => await ContinueButton.ClickAsync();

        internal async Task VerifyCohort(Apprenticeship apprenticeship, string cohortStatus)
        {
            await Assertions.Expect(employerName).ToHaveTextAsync(apprenticeship.EmployerDetails.EmployerName.ToString());
            await Assertions.Expect(cohortReference).ToHaveTextAsync(apprenticeship.Cohort.Reference);
            await Assertions.Expect(status).ToHaveTextAsync(cohortStatus);
            //await Assertions.Expect(message).ToHaveTextAsync("No message added.");

            var expectedName = apprenticeship.ApprenticeDetails.FullName;
            var expectedULN = apprenticeship.ApprenticeDetails.ULN.ToString();
            var expectedDOB = apprenticeship.ApprenticeDetails.DateOfBirth.ToString("d MMM yyyy", CultureInfo.InvariantCulture);
            var expectedTrainingDates = apprenticeship.TrainingDetails.StartDate.ToString("MMM yyyy", CultureInfo.InvariantCulture) + " to " + apprenticeship.TrainingDetails.EndDate.ToString("MMM yyyy", CultureInfo.InvariantCulture);
            var expectedPrice = apprenticeship.TrainingDetails.TotalPrice.ToString("C0");

            var apprenticeRow = row(apprenticeship.ApprenticeDetails.ULN.ToString());
            await Assertions.Expect(Name(apprenticeRow)).ToHaveTextAsync(expectedName.Trim());
            await Assertions.Expect(Uln(apprenticeRow)).ToHaveTextAsync(expectedULN.Trim());
            await Assertions.Expect(Dob(apprenticeRow)).ToHaveTextAsync(expectedDOB.Trim());
            await Assertions.Expect(TrainingDates(apprenticeRow)).ToHaveTextAsync(expectedTrainingDates.Trim());
            await Assertions.Expect(Price(apprenticeRow)).ToHaveTextAsync(expectedPrice.Trim());
        
        }

        internal async Task GetCohortId(Apprenticeship apprenticeship)
        {
            var cohortRef = await cohortReference.InnerTextAsync();
            apprenticeship.Cohort.Reference = cohortRef;

            await Task.Delay(100);
            context.Set(apprenticeship, "Apprenticeship");
        }

        internal async Task<ViewApprenticeDetails_ProviderPage> ClickOnViewApprenticeLink(string name)
        {
            await viewLink("  " + name).ClickAsync();
            return await VerifyPageAsync(() => new ViewApprenticeDetails_ProviderPage(context));
        }

        internal async Task<SelectLearnerFromILRPage> ClickOnAddAnotherApprenticeLink()
        {
            await AddAnotherApprenticeLink.ClickAsync();
            return await VerifyPageAsync(() => new SelectLearnerFromILRPage(context));
        }

        internal async Task<ProviderSelectAReservationPage> ClickOnAddAnotherApprenticeLink_SelectReservationRoute()
        {
            await AddAnotherApprenticeLink.ClickAsync();
            return await VerifyPageAsync(() => new ProviderSelectAReservationPage(context));
        }

        internal async Task<SelectLearnerFromILRPage> ClickOnAddAnotherApprenticeLink_SelectExistingReservationRoute()
        {
            await AddAnotherApprenticeLink.ClickAsync();
            await new AddApprenticeDetails_EntryMothodPage(context).SelectOptionToAddApprenticesFromILRList_InsufficientPermissionsRoute();
            var reservation = await new SelectReservationPage(context).SelectReservation();
            return new SelectLearnerFromILRPage(context);
        }


        internal async Task<AddApprenticeDetails_EntryMothodPage> ClickOnAddAnotherApprenticeLink_ToSelectEntryMthodPage()
        {
            await AddAnotherApprenticeLink.ClickAsync();
            return await VerifyPageAsync(() => new AddApprenticeDetails_EntryMothodPage(context));            
        }
        
        internal async Task<CohortApprovedAndSentToEmployerPage> ProviderApproveCohort()
        {
            await rplVerifiedCheckBox.ClickAsync();
            await approveRadioOption.ClickAsync();
            await messageToEmployerTextBox.FillAsync("Please review the details and approve the request.");
            await saveAndSubmitButton.ClickAsync();
            return await VerifyPageAsync(() => new CohortApprovedAndSentToEmployerPage(context));
        }

        internal async Task<CohortSentToEmployerForReview> ProviderSendCohortForEmployerReview()
        {
            await rplVerifiedCheckBox.ClickAsync();
            await sendToEmployerRadioOption.ClickAsync();
            await messageToEmployerToReviewTextBox.FillAsync("Please review the details and approve the request.");
            await saveAndSubmitButton.ClickAsync();
            return await VerifyPageAsync(() => new CohortSentToEmployerForReview(context));
        }

        internal async Task ValidateWarningMessageForFoundationCourses(string warningMsg) => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(warningMsg);

        internal async Task<CohortApproved> ProviderApprovesCohortAfterEmployerApproval()
        {
            await approveRadioOption.ClickAsync();
            await saveAndSubmitButton.ClickAsync();
            return await VerifyPageAsync(() => new CohortApproved(context));
        }

        internal async Task CanCohortBeApproved(bool flag)
        {
            if (flag)
            {
                await approveRadioOption.ClickAsync();
                await messageToEmployerTextBox.FillAsync("Please review the details and approve the request.");
            }
            else
            {
                await Assertions.Expect(approveRadioOption).ToHaveCountAsync(0);
            }

        }

        internal async Task<ConfirmApprenticeDeletionPage> ClickOnDeleteApprenticeLink(string name)
        {
            await deleteLink("  " + name).ClickAsync();
            return await VerifyPageAsync(() => new ConfirmApprenticeDeletionPage(context));
        }

        internal async Task<ConfirmCohortDeletionPage> ClickOnDeleteCohortLink()
        {
            await DeleteThisCohortLink.ClickAsync();
            return await VerifyPageAsync(() => new ConfirmCohortDeletionPage(context));
        }

        internal async Task<ApprenticeRequests_ProviderPage> ClickOnSaveAndExitLink()
        {
            await saveAndexitLink.ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeRequests_ProviderPage(context));
        }

        internal async Task<AddRecognitionOfPriorLearningDetailsPage> AddRPL() {
            await AddRplLink.ClickAsync();
            return await VerifyPageAsync(() => new AddRecognitionOfPriorLearningDetailsPage(context));
        }

        internal async Task VerifyBanner(string text) => await Assertions.Expect(banner).ToContainTextAsync(text);

        internal async Task VerifyBanner(string title, string content) => await Assertions.Expect(page.GetByLabel(title)).ToContainTextAsync(content);

        internal async Task SelectFirstRadioButtonAndSubmit(string optionalMsg=null)
        {
            await firstRadioOption.CheckAsync();
            await rplVerifiedCheckBox.ClickAsync();
            await saveAndSubmitButton.ClickAsync();
        }

       internal async Task<ProviderAccessDeniedPage> TryOpenLink(string linkName)
        {
            await page.GetByRole(AriaRole.Link, new() { Name = linkName }).Last.ClickAsync();
            return await VerifyPageAsync(() => new ProviderAccessDeniedPage(context));
        }

    }
}
