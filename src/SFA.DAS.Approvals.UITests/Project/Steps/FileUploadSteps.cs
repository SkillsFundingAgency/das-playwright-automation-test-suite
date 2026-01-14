using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.FileUploadModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class FileUploadSteps
    {
        private readonly ScenarioContext context;
        private ProviderStepsHelper providerStepsHelper;
        private ApprenticeDataHelper apprenticeDataHelper;
        private FileUploadHelper fileUploadHelper;

        public FileUploadSteps(ScenarioContext _context)
        {
            context = _context;
            providerStepsHelper = new ProviderStepsHelper(context);
            apprenticeDataHelper = new ApprenticeDataHelper(context);
            fileUploadHelper = new FileUploadHelper(context);
        }


        [Given("Provider have few apprentices to add using CSV file upload")]
        public async Task GivenProviderHaveFewApprenticesToAddUsingCSVFileUpload()
        {
            var foundationTrainingDetails = new TrainingFactory(coursesDataHelper => coursesDataHelper.GetRandomFoundationCourse());
            List<Apprenticeship> listOfApprenticeship = new List<Apprenticeship>();

            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.NonLevy, 1, null, listOfApprenticeship);
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship, trainingFactory: foundationTrainingDetails);
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);

        }

        [Given("one of the apprentice on Level-7 course is above (.*) years")]
        public async Task GivenOneOfTheApprenticeOnLevelCourseIsAboveYears(int ageLimit)
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            var level7TrainingDetails = new TrainingFactory(DateTime.Today, coursesDataHelper => coursesDataHelper.GetRandomLevel7Course());
            var apprenticeDetails = new ApprenticeFactory(ageLimit + 1);
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship, apprenticeFactory: apprenticeDetails, trainingFactory: level7TrainingDetails);
            context["listOfApprenticeship"] = listOfApprenticeship;

        }

        [Given("one of the apprentice on Foundation course is above (.*) years")]
        public async Task GivenOneOfTheApprenticeOnFoundationCourseIsAboveYears(int ageLimit)
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            var foundationTrainingDetails = new TrainingFactory(DateTime.Today, coursesDataHelper => coursesDataHelper.GetRandomFoundationCourse());
            var apprenticeDetails = new ApprenticeFactory(ageLimit + 1);
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship, apprenticeFactory: apprenticeDetails, trainingFactory: foundationTrainingDetails);
            context["listOfApprenticeship"] = listOfApprenticeship;

            ICsvFileFactory csvFileFactory = new CsvFileFactory();
            await csvFileFactory.CreateCsvFile(listOfApprenticeship, fileUploadHelper.CsvFileLocation());            
        }

        [When("Provider uploads the csv file")]
        public async Task WhenProviderUploadsTheCsvFile()
        {
            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            var page = await StartBulkUploadJourney();
            await page.UploadFile(fileUploadHelper.CsvFileLocation());
        }

        [Then("system does not allow to upload the file and displays an error message")]
        public async Task ThenSystemDoesNotAllowToUploadTheFileAndDisplaysAnErrorMessage()
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            var errorMessage = "The apprentice's date of birth must show that they are not older than 25 years old at the start of their training";

            var rowsToValidate = new[] { 3, 4 };        //based on previous step, the error message will be displayed for rows 3 and 4

            foreach (var rowNumber in rowsToValidate)
            {
                var apprentice = listOfApprenticeship[rowNumber - 1];

                var erroneousRow =
                    $"{rowNumber} {apprentice.EmployerDetails.EmployerName} {apprentice.ApprenticeDetails.ULN} {apprentice.ApprenticeDetails.FullName} {errorMessage}";

                await new UploadCsvFilePage(context).ValidateErrorMessage(erroneousRow);
            }
        }

        [Then("the user can bulk upload apprentices")]
        internal async Task ThenTheUserCanBulkUploadApprentices()
        {
            await new ApprenticeRequests_ProviderPage(context).NavToHomePage();
            var page = await StartBulkUploadJourney();
            await page.NavToHomePage();
        }

        private async Task<UploadCsvFilePage> StartBulkUploadJourney()
        {            
            var page1 = await new ProviderHomePage(context).GotoSelectJourneyPage();
            var page2 = await new AddApprenticeDetails_EntryMothodPage(context).SelectOptionToUploadCsvFile();
            return await page2.ClickContinueButton();
        }


    }
}
