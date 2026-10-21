using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.FileUploadModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Globalization;
using System.IO;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class FileUploadSteps
    {
        private readonly ScenarioContext context;
        private ProviderStepsHelper providerStepsHelper;
        private ApprenticeDataHelper apprenticeDataHelper;
        private FileUploadHelper fileUploadHelper;
        private readonly CommonStepsHelper commonStepsHelper;
        private readonly CommitmentsDbSqlHelper _commitmentsDbSqlHelper;
        private readonly AccountsDbSqlHelper _accountsDbSqlHelper;
        private readonly EmployerStepsHelper employerStepsHelper;


        public FileUploadSteps(ScenarioContext _context)
        {
            context = _context;
            providerStepsHelper = new ProviderStepsHelper(context);
            employerStepsHelper = new EmployerStepsHelper(context);
            apprenticeDataHelper = new ApprenticeDataHelper(context);
            commonStepsHelper = new CommonStepsHelper(context);
            fileUploadHelper = new FileUploadHelper(context);
            _commitmentsDbSqlHelper = context.Get<CommitmentsDbSqlHelper>();
            _accountsDbSqlHelper = context.Get<AccountsDbSqlHelper>();
        }


        [Given("^Provider have few apprentices to add using CSV file upload$")]
        public async Task GivenProviderHaveFewApprenticesToAddUsingCSVFileUpload()
        {
            var foundationTrainingDetails = new TrainingFactory(coursesDataHelper => coursesDataHelper.GetRandomFoundationCourse());
            List<Apprenticeship> listOfApprenticeship = new List<Apprenticeship>();

            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.NonLevy, 1, null, listOfApprenticeship);
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship, trainingFactory: foundationTrainingDetails);
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);

        }

        [When("^Provider add (.*) apprentice details using bulkupload$")]
        public async Task WhenProviderHaveFewLearnerToAddUsingCSVFileUpload(int numberOfApprentices)
        {
            List<Apprenticeship> listOfApprenticeship = new List<Apprenticeship>();

            var cohortReferences = await commonStepsHelper.GetCohortReferenceList();

            // morethan one cohort
            if (cohortReferences?.Count > 1)
            {
                foreach (var cohort in cohortReferences)
                {
                  listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, numberOfApprentices, null, listOfApprenticeship, null,  null, null, cohort);              
                  context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
                }
            }
            else // single cohort
            {
               listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
               context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
            }

        }

        [Given("^one of the apprentice on Level-7 course is above (.*) years$")]
        public async Task GivenOneOfTheApprenticeOnLevelCourseIsAboveYears(int ageLimit)
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            var level7TrainingDetails = new TrainingFactory(DateTime.Today, coursesDataHelper => coursesDataHelper.GetRandomLevel7Course());
            var apprenticeDetails = new ApprenticeFactory(ageLimit + 1);
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship, apprenticeFactory: apprenticeDetails, trainingFactory: level7TrainingDetails);
            context["listOfApprenticeship"] = listOfApprenticeship;

        }

        [Given("^one of the apprentice on Foundation course is above (.*) years$")]
        public async Task GivenOneOfTheApprenticeOnFoundationCourseIsAboveYears(int ageLimit)
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            var foundationTrainingDetails = new TrainingFactory(DateTime.Today, coursesDataHelper => coursesDataHelper.GetRandomFoundationCourse());
            var apprenticeDetails = new ApprenticeFactory(ageLimit + 1);
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship, apprenticeFactory: apprenticeDetails, trainingFactory: foundationTrainingDetails);
            context["listOfApprenticeship"] = listOfApprenticeship;
        }

        [Given(@"one of the apprentice on Short GSO course")]
        public async Task GivenOneOfTheApprenticeOnShortGSOCourse()
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            var shortCourseDetails = new TrainingFactory(DateTime.Today, coursesDataHelper => coursesDataHelper.GetRandomShortCourse());
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship, apprenticeFactory: null, trainingFactory: shortCourseDetails);
            context["listOfApprenticeship"] = listOfApprenticeship;
        }


        [When("^Provider uploads the csv file$")]
        public async Task WhenProviderUploadsTheCsvFile()
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            ICsvFileFactory csvFileFactory = new CsvFileFactory();
            await csvFileFactory.CreateCsvFile(listOfApprenticeship, fileUploadHelper.CsvFileLocation());

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            var page = await StartBulkUploadJourney();
            await page.TryUploadFile(fileUploadHelper.CsvFileLocation());
        }

        [When("^Provider uploads the updated csv file$")]
        public async Task WhenProviderUploadsUpdatedCsvFile()
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            ICsvFileFactory csvFileFactory = new CsvFileFactory();
            await csvFileFactory.CreateCsvFile(listOfApprenticeship, fileUploadHelper.CsvFileLocation());

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            var page = await StartBulkUploadJourney();
            await page.UploadFile(fileUploadHelper.CsvFileLocation());
        }

        [When("^Provider uses BulkUpload to add 2 apprentice details into existing cohort and 2 apprentice details into a non-existing cohort for all employers$")]
        public async Task WhenAddAProviderUploadsUpdatedCsvFileForAllEmployers()
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var listOfApprenticeshipNew = new List<Apprenticeship>();


            for (int i = 0; i < listOfApprenticeship.Count; i++)
            {
              DateTime trainingStartDate = DateTime.Today.AddMonths(1);      
              TrainingFactory trainingDetails = new TrainingFactory(trainingStartDate, coursesDataHelper => coursesDataHelper.GetRandomFoundationCourse());
              var apprenticeDetails = new ApprenticeFactory(24);

              var newApprenticeships = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 2, null, null, apprenticeFactory: apprenticeDetails, trainingFactory: trainingDetails);


                newApprenticeships[0].EmployerDetails = listOfApprenticeship[i].EmployerDetails;
                newApprenticeships[1].EmployerDetails = listOfApprenticeship[i].EmployerDetails;
                listOfApprenticeshipNew.AddRange(newApprenticeships);
            }

            listOfApprenticeship = listOfApprenticeshipNew;
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
        }

        [Then("^Correct Information is displayed on review apprentices details page$")]
        public async Task ThenCorrectInformationIsDisplayedOnReviewApprenticeDetailsPage()
        {
            var apprenticeships = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var page = new ReviewApprenticeDetailsBulkUploadPage(context);

            await page.VerifyPage();
            await page.VerifyCorrectInformationIsDisplayed(apprenticeships);
        }

        // [Then(@"Provider approves the cohorts and send them to employer to approve")]
        // public async Task WhenProviderApprovesTheCohortsAndSendThemToEmployerToApprove()
        // {
        //     var apprenticeList = GetBulkuploadData();
        //     await new ReviewApprenticeDetailsBulkUploadPage(_context)
        //         .SelectToApproveAllAndSendToEmployer()
        //         .VerifyCorrectInformationIsDisplayed(apprenticeList);
        // }

        [When(@"User selects to upload an amended file")]
        public async Task UserSelectsToUploadAnAmendedFile()
        {
            await new ReviewApprenticeDetailsBulkUploadPage(context).SelectToUploadAnAmendedFile();
        }

        [When(@"Provider uploads another file")]
        public async Task ProviderUploadsAnotherFile()
        {
            var listOfApprenticeships = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            ICsvFileFactory csvFileFactory = new CsvFileFactory();
            await csvFileFactory.CreateCsvFile(listOfApprenticeships, fileUploadHelper.CsvFileLocation());

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            var page = await StartBulkUploadJourney();
            await page.UploadFile(fileUploadHelper.CsvFileLocation());
        }

        [When(@"User selects to upload an amended file through link")]
        public async Task UserSelectsToUploadAnAmendedFileThroughLink()
        {
            await new ReviewApprenticeDetailsBulkUploadPage(context).SelectToUploadAnAmendedFileThroughLink();
        }


        [When(@"Provider selects No on confirmation for upload an amended file")]
        public async Task ProviderSelectsNoOnConfirmationForUploadAmendedFile()
        {
            await new AreYouSureYouWantToUploadAmendedFilePage(context).SelectNoAndContinue();
        }

        [Given(@"Provider selects Yes on confirmation for upload an amended file")]
        [When(@"Provider selects Yes on confirmation for upload an amended file")]
        public async Task ProviderSelectsYesOnConfirmationForUploadAmendedFile()
        {
            await new AreYouSureYouWantToUploadAmendedFilePage(context).SelectYesAndContinue();
        }

        [Given(@"Provider selects to save all but don't send to employer")]
        [When(@"Provider selects to save all but don't send to employer")]
        public async Task ProviderSelectsToSaveAllButDontSendToEmployer()
        {
            await new ReviewApprenticeDetailsBulkUploadPage(context)
                .SelectToSaveAllButDontSendToEmployer();
        }

        [Then("^system does not allow to upload the file and displays an error message$")]
        public async Task ThenSystemDoesNotAllowToUploadTheFileAndDisplaysAnErrorMessage()
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var rowsToValidate = new[] { 3, 4, 5 };        //based on previous step, the error message will be displayed for rows 3, 4 and 5

            foreach (var rowNumber in rowsToValidate)
            {
                var apprentice = listOfApprenticeship[rowNumber - 1];

                //the error message will be different for row 5 - short courses
                var errorMessage = (rowNumber > 4) ? "Enter a valid standard code. Apprenticeship units must be added by ILR upload" : "The apprentice's date of birth must show that they are not older than 25 years old at the start of their training";

                var erroneousRow =
                    $"{rowNumber} {apprentice.EmployerDetails.EmployerName} {apprentice.ApprenticeDetails.ULN} {apprentice.ApprenticeDetails.FullName} {errorMessage}";

                await new ThereIsAProblemWithYourCsvFilePage(context).ValidateErrorMessage(erroneousRow);
            }
        }

        [Then("^the user can bulk upload apprentices$")]
        internal async Task ThenTheUserCanBulkUploadApprentices()
        {
            await new ApprenticeRequests_ProviderPage(context).NavToHomePage();
            var page = await StartBulkUploadJourney();
            await page.NavToHomePage();
        }


        [When(@"Provider add an apprentice uses details from below to create bulkupload")]
        public Task WhenProviderAddAnApprenticeUsesDetailsFromBelowToCreateBulkupload(Table table) => ValidateApprenticeRecord(table);

        public async Task ValidateApprenticeRecord(Table table)
        {
            var apprenticeRecords = table.CreateSet<MapApprenticeDetails>().ToList();
            var validApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).First();

            for (var index = 0; index < apprenticeRecords.Count; index++)
            {
                var record = apprenticeRecords[index];
                var csvFilePath = fileUploadHelper.CsvFileLocation();
                await CreateValidationCsvFile(record, validApprenticeship, csvFilePath);
                await ProviderUploadsAnotherFile(csvFilePath, record.ErrorMessage, index == 0);
            }
        }

        [Given(@"the provider has a cohort which is with employer")]
       private async Task GivenTheProviderHasACohortWhichIsWithEmployert()
        {

            var listOfApprenticeship = context.ContainsKey(ScenarioKeys.ListOfApprenticeship)
                ? context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship) ?? new List<Apprenticeship>()
                : new List<Apprenticeship>();

            // List<Apprenticeship> listOfApprenticeship = new List<Apprenticeship>();
            // listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship)  != null ? context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship) : await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship);
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1);

            var ukprn = listOfApprenticeship.FirstOrDefault().ProviderDetails.Ukprn;
            var email = listOfApprenticeship.FirstOrDefault().EmployerDetails.Email;
            var name = listOfApprenticeship.FirstOrDefault().EmployerDetails.EmployerName;
            var employerAccountId = await  _accountsDbSqlHelper.GetEmployerAccountId(email, name);
           
            var cohortRef = await _commitmentsDbSqlHelper.GetProviderCohortWhichIsWithEmployer(ukprn, employerAccountId);    
            
            
            listOfApprenticeship.FirstOrDefault().Cohort.Reference = cohortRef;
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);

            await commonStepsHelper.SetCohortDetails(cohortRef, "Under review with Employer", "Ready for approval");
        }

       [Given(@"the provider has a cohort which is with transfer-sender")]
       private async Task GivenTheProviderHasACohortWhichIsWithTransferSender()
        {
            var listOfApprenticeship = context.ContainsKey(ScenarioKeys.ListOfApprenticeship)
                ? context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship) ?? new List<Apprenticeship>()
                : new List<Apprenticeship>();
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1);

            // List<Apprenticeship> listOfApprenticeship = new List<Apprenticeship>();
            
            // listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship)  != null ? context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship) : await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship);
            var ukprn = listOfApprenticeship.FirstOrDefault().ProviderDetails.Ukprn;
            var cohortRef = await _commitmentsDbSqlHelper.GetProviderCohortWithTransferSender(ukprn);    
           
            listOfApprenticeship.FirstOrDefault().Cohort.Reference = cohortRef;
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);

            await commonStepsHelper.SetCohortDetails(cohortRef, "Under review with Employer", "Ready for approval");
        }

       [Given(@"the provider has a cohort as a result of change of party")]
       private async Task GivenTheProviderHasACohortAsAResultOfChangeOfParty()
        {

            var listOfApprenticeship = context.ContainsKey(ScenarioKeys.ListOfApprenticeship)
                ? context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship) ?? new List<Apprenticeship>()
                : new List<Apprenticeship>();
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1);

            // List<Apprenticeship> listOfApprenticeship = new List<Apprenticeship>();
            // listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship)  != null ? context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship) : await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship);
           

            var ukprn = listOfApprenticeship.FirstOrDefault().ProviderDetails.Ukprn;
            var employerAccountId = await  _accountsDbSqlHelper.GetEmployerAccountId(listOfApprenticeship.FirstOrDefault().EmployerDetails.Email, listOfApprenticeship.FirstOrDefault().EmployerDetails.EmployerName);
            var cohortRef = await _commitmentsDbSqlHelper.GetProviderCohortWithChangeOfParty(ukprn, employerAccountId);    

            listOfApprenticeship.FirstOrDefault().Cohort.Reference = cohortRef;
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);

            await commonStepsHelper.SetCohortDetails(cohortRef, "Under review with Employer", "Ready for approval");
        }



        [When(@"the provider tries a bulk upload file to add apprentices in that cohort")]
        public async Task TheProviderTriesABulkUploadFileToAddApprenticesInThatCohort() {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            ICsvFileFactory csvFileFactory = new CsvFileFactory();
            await csvFileFactory.CreateCsvFile(listOfApprenticeship, fileUploadHelper.CsvFileLocation());

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            var page = await StartBulkUploadJourney();
            await page.UploadFileErrorCase(fileUploadHelper.CsvFileLocation());
        }


        [Then(@"Non Editable Cohorts error message is displayed")]
        private async Task ThenNonEditableCohortsErrorMessageIsDisplayed()
        {
            var errorPageMsg = "You cannot add apprentices to this cohort, as it is with the employer. You need to add this learner to a different or new cohort.";
            await new ThereIsAProblemWithYourCsvFilePage(context).ValidateErrorMessage(errorPageMsg);
        }

        [Then(@"Transfer Sender Cohorts error message is displayed")]
        private async Task ThenTransferSenderCohortsErrorMessageIsDisplayed()
        {
        
            var errorPageMsg = "You cannot add apprentices to this cohort, as it is with the transfer sending employer. You need to add this learner to a different or new cohort.";
            await new ThereIsAProblemWithYourCsvFilePage(context).ValidateErrorMessage(errorPageMsg);
        }

        [Then(@"an error message is displayed")]
        private async Task ThenAnErrorMessageIsDisplayed()
        {
            var errorPageMsg = "You cannot add apprentices to this cohort. You need to add this learner to a different or new cohort.";
            await new ThereIsAProblemWithYourCsvFilePage(context).ValidateErrorMessage(errorPageMsg);
        }
        

        [Given(@"Provider says No on Confirm cancel confirmation page")]
        [When(@"Provider says No on Confirm cancel confirmation page")]
        private async Task ProviderSelectsNoOnConfirmCancelConfirmationPage() => await new AreYouSureYouWantToDiscardThisFilePage(context).SelectNoAndContinue();


        [Given(@"Provider says Yes on Confirm cancel confirmation page")]
        [When(@"Provider says Yes on Confirm cancel confirmation page")]
        private async Task ProviderSelectsYesOnConfirmCancelConfirmationPage() => await new AreYouSureYouWantToDiscardThisFilePage(context).SelectYesAndContinue();

        [Given(@"Upload cancelled confirmation page is displayed")]
        [When(@"Upload cancelled confirmation page is displayed")]
        private async Task UploadCancelled() => await new FileUploadCancelledPage(context).VerifyPage();

        [Given(@"Provider selects to Cancel bulk upload")]
        [When(@"Provider selects to Cancel bulk upload")]
        private async Task ProviderSelectsToCancelBulkUpload() => await new UploadCsvFilePage(context).CancelUpload();


        [Given(@"Provider uses BulkUpload to add (.*) apprentice details for a non-levy employer into a non-existing cohort")]
         private async Task GivenProviderUsesBulkUploadToAddApprenticeDetailsForANon_LevyEmployerIntoANon_ExistingCohort(int numberOfApprentices)
        {

            List<Apprenticeship> listOfApprenticeship = new List<Apprenticeship>();
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.NonLevy, numberOfApprentices);

            await employerStepsHelper.AddEmptyCohort();
           
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
        }
        
        private async Task ProviderUploadsAnotherFile(string csvFilePath, string expectedErrorMessage, bool startUploadJourney)
        {
            UploadCsvFilePage page;
            if (startUploadJourney)
            {
                await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
                page = await StartBulkUploadJourney();
            }
            else
            {
                page = new UploadCsvFilePage(context);
            }

            var errorPage = await page.UploadFileErrorCase(csvFilePath);
            await errorPage.ValidateErrorMessage(expectedErrorMessage);
        }

        private static async Task CreateValidationCsvFile(MapApprenticeDetails record, Apprenticeship validApprenticeship, string filePath)
        {
            const string csvHeader = "CohortRef,AgreementID,ULN,FamilyName,GivenNames,DateOfBirth,EmailAddress,StdCode,StartDate,EndDate,TotalPrice,EPAOrgID,ProviderRef,RecognisePriorLearning,TrainingTotalHours,TrainingHoursReduction,IsDurationReducedByRPL,DurationReducedBy,PriceReducedBy";

            var apprentice = validApprenticeship.ApprenticeDetails;
            var training = validApprenticeship.TrainingDetails;
            var rpl = validApprenticeship.RPLDetails;
            var employer = validApprenticeship.EmployerDetails;
            var values = new[]
            {
                UseTableValueOr(record.CohortRef, validApprenticeship.Cohort.Reference),
                UseTableValueOr(record.AgreementId, employer.AgreementId),
                UseTableValueOr(record.ULN, apprentice.ULN),
                UseTableValueOr(record.FamilyName, apprentice.LastName),
                UseTableValueOr(record.GivenNames, apprentice.FirstName),
                UseTableValueOr(record.DateOfBirth, apprentice.DateOfBirth.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                UseTableValueOr(record.EmailAddress, apprentice.Email),
                UseTableValueOr(record.StdCode, training.LarsCode),
                UseTableValueOr(record.StartDate, training.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                UseTableValueOr(record.EndDate, training.EndDate.ToString("yyyy-MM", CultureInfo.InvariantCulture)),
                UseTableValueOr(record.TotalPrice, training.TotalPrice.ToString(CultureInfo.InvariantCulture)),
                UseTableValueOr(record.EPAOrgID, "EPA0001"),
                UseTableValueOr(record.ProviderRef, RandomDataGenerator.GenerateRandomAlphabeticString(6)),
                rpl.IsDurationReducedByRPL.ToString(),
                rpl.TrainingTotalHours.ToString(CultureInfo.InvariantCulture),
                rpl.TrainingHoursReduction.ToString(CultureInfo.InvariantCulture),
                rpl.IsDurationReducedByRPL.ToString(),
                rpl.DurationReducedBy.ToString(CultureInfo.InvariantCulture),
                rpl.PriceReducedBy.ToString(CultureInfo.InvariantCulture)
            };

            var csvRow = string.Join(",", values.Select(EscapeCsvValue));
            await File.WriteAllLinesAsync(filePath, new[] { csvHeader, csvRow });
        }

        private static string UseTableValueOr(string tableValue, string validValue) =>
            tableValue == "valid" ? validValue : tableValue ?? string.Empty;

        private static string EscapeCsvValue(string value)
        {
            value ??= string.Empty;
            if (value.Length == 0)
                return "\"\"";

            if (value.IndexOfAny(new[] { ',', '"', '\r', '\n' }) < 0)
                return value;

            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        private async Task<UploadCsvFilePage> StartBulkUploadJourney()
        {
            var page1 = await new ProviderHomePage(context).GotoSelectJourneyPage();
            var page2 = await new HowDoYouWantToAddLearner_EntryMothodPage(context).SelectOptionToUploadCsvFile();
            return await page2.ClickContinueButton();
        }

    }
}
