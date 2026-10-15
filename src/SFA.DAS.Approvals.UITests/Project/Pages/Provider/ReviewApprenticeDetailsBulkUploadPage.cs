using System.Globalization;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ReviewApprenticeDetailsBulkUploadPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check new apprentice records");
        }

        private ILocator ContinueButton => page.Locator("#continue-button");
        private ILocator EmployerDetails => page.Locator(".bu-employer-details");
        private ILocator UploadAnAmendedFileRadioButton => page.Locator("#details-new-file");
        private ILocator UploadAnAmendedFileActionLink => page.Locator("#upload-amended-file-link");
        private ILocator SaveButDontSendToEmployerRadioButton => page.Locator("#details-save");
        private ILocator ApproveAllAndSendToEmployerButton => page.Locator("#details-approve");
        private ILocator CancelUploadLink => page.Locator("#cancel-upload-link");

        public async Task<ReviewApprenticeDetailsBulkUploadPage> VerifyCorrectInformationIsDisplayed(List<FileUploadReviewEmployerDetails> apprenticeList)
        {
            await VerifyEmployerDetails(apprenticeList);
            return this;
        }

        public async Task<ReviewApprenticeDetailsBulkUploadPage> VerifyCorrectInformationIsDisplayed(List<Apprenticeship> apprenticeships)
        {
            await VerifyEmployerDetails(apprenticeships);
            return this;
        }

        public async Task<AreYouSureYouWantToUploadAmendedFilePage> SelectToUploadAnAmendedFile()
        {
            await UploadAnAmendedFileRadioButton.ClickAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new AreYouSureYouWantToUploadAmendedFilePage(context));
        }

        public async Task<NewApprenticeDetailsSavedSuccessfully> SelectToSaveAllButDontSendToEmployer()
        {
            await SaveButDontSendToEmployerRadioButton.ClickAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new NewApprenticeDetailsSavedSuccessfully(context));
        }

        public async Task<NewApprenticesAddedAndSentToEmployer> SelectToApproveAllAndSendToEmployer()
        {
            await ApproveAllAndSendToEmployerButton.ClickAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new NewApprenticesAddedAndSentToEmployer(context));
        }

        public async Task<AreYouSureYouWantToUploadAmendedFilePage> SelectToUploadAnAmendedFileThroughLink()
        {
            await UploadAnAmendedFileActionLink.ClickAsync();
            return await VerifyPageAsync(() => new AreYouSureYouWantToUploadAmendedFilePage(context));
        }

        public async Task CancelUpload() => await CancelUploadLink.ClickAsync();

        private async Task VerifyCohortDetails(List<FileUploadReviewCohortDetail> cohortDetails, ILocator employerDetails)
        {
            var cohortReferences = employerDetails.Locator(".bu-cohort-ref");
            var apprenticeCountsAndTotals = employerDetails.Locator(".bu-apprentice-count");

            await Assertions.Expect(cohortReferences).ToHaveCountAsync(cohortDetails.Count);
            await Assertions.Expect(apprenticeCountsAndTotals).ToHaveCountAsync(cohortDetails.Count);

            for (var index = 0; index < cohortDetails.Count; index++)
            {
                var cohortDetailsForRow = cohortDetails[index];
                var expectedCohortReference = string.IsNullOrWhiteSpace(cohortDetailsForRow.CohortRef)
                    ? "This will be created when you save or send to employers"
                    : cohortDetailsForRow.CohortRef;

                await Assertions.Expect(cohortReferences.Nth(index))
                    .ToHaveTextAsync($"Cohort: {expectedCohortReference}");
                await Assertions.Expect(apprenticeCountsAndTotals.Nth(index))
                    .ToHaveTextAsync(cohortDetailsForRow.NumberOfApprenticeshipAndTotalCost);
            }
        }

        private async Task VerifyEmployerDetails(List<FileUploadReviewEmployerDetails> expectedEmployers)
        {
            var employerDetails = EmployerDetails;
            await Assertions.Expect(employerDetails).ToHaveCountAsync(expectedEmployers.Count);

            for (var index = 0; index < expectedEmployers.Count; index++)
            {
                var employerDetailsForRow = employerDetails.Nth(index);
                var expectedEmployer = expectedEmployers[index];
                var employerNameAndAgreementId = employerDetailsForRow.Locator(".bu-employer-name");

                await Assertions.Expect(employerNameAndAgreementId)
                    .ToContainTextAsync(expectedEmployer.EmployerName);
                await Assertions.Expect(employerNameAndAgreementId.Locator(".bu-agreementId"))
                    .ToHaveTextAsync($"Agreement ID: {expectedEmployer.AgreementId}");

                await VerifyCohortDetails(expectedEmployer.CohortDetails, employerDetailsForRow);
            }
        }

        private async Task VerifyEmployerDetails(List<Apprenticeship> apprenticeships)
        {
            var expectedEmployers = apprenticeships
                .GroupBy(apprenticeship => apprenticeship.EmployerDetails.AgreementId)
                .ToList();
            var employerDetails = EmployerDetails;

            await Assertions.Expect(employerDetails).ToHaveCountAsync(expectedEmployers.Count);

            for (var index = 0; index < expectedEmployers.Count; index++)
            {
                var expectedEmployer = expectedEmployers[index];
                var employerDetailsForRow = employerDetails.Nth(index);
                var firstApprenticeship = expectedEmployer.First();
                var employerNameAndAgreementId = employerDetailsForRow.Locator(".bu-employer-name");

                await Assertions.Expect(employerNameAndAgreementId)
                    .ToContainTextAsync(firstApprenticeship.EmployerDetails.EmployerName);
                await Assertions.Expect(employerNameAndAgreementId.Locator(".bu-agreementId"))
                    .ToHaveTextAsync($"Agreement ID: {firstApprenticeship.EmployerDetails.AgreementId}");

                var expectedCohorts = expectedEmployer
                    .GroupBy(apprenticeship => apprenticeship.Cohort.Reference)
                    .ToList();
                var cohortReferences = employerDetailsForRow.Locator(".bu-cohort-ref");
                var apprenticeCountsAndTotals = employerDetailsForRow.Locator(".bu-apprentice-count");

                await Assertions.Expect(cohortReferences).ToHaveCountAsync(expectedCohorts.Count);
                await Assertions.Expect(apprenticeCountsAndTotals).ToHaveCountAsync(expectedCohorts.Count);

                for (var cohortIndex = 0; cohortIndex < expectedCohorts.Count; cohortIndex++)
                {
                    var expectedCohort = expectedCohorts[cohortIndex].ToList();
                    var cohortReference = expectedCohort[0].Cohort.Reference;
                    var expectedCohortReference = string.IsNullOrWhiteSpace(cohortReference)
                        ? "This will be created when you save or send to employers"
                        : cohortReference;
                    var totalPrice = expectedCohort.Sum(apprenticeship => apprenticeship.TrainingDetails.TotalPrice);

                    await Assertions.Expect(cohortReferences.Nth(cohortIndex))
                        .ToHaveTextAsync($"Cohort: {expectedCohortReference}");
                    await Assertions.Expect(apprenticeCountsAndTotals.Nth(cohortIndex))
                        .ToContainTextAsync($"{expectedCohort.Count} apprentice");
                    await Assertions.Expect(apprenticeCountsAndTotals.Nth(cohortIndex))
                        .ToContainTextAsync(totalPrice.ToString("C0", CultureInfo.GetCultureInfo("en-GB")));
                }
            }
        }
    }

    public class FileUploadReviewEmployerDetails
    {
        public string EmployerName { get; set; } = string.Empty;
        public string AgreementId { get; set; } = string.Empty;
        public List<FileUploadReviewCohortDetail> CohortDetails { get; set; } = [];
    }

    public class FileUploadReviewCohortDetail
    {
        public string CohortRef { get; set; } = string.Empty;
        public string NumberOfApprenticeshipAndTotalCost { get; set; } = string.Empty;
    }
}

