using Microsoft.VisualBasic;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class EmployerSteps
    {
        protected readonly ScenarioContext context;
        private readonly EmployerStepsHelper employerStepsHelper;
        private readonly CommonStepsHelper commonStepsHelper;
        private readonly CommitmentsDbSqlHelper commitmentsDbSqlHelper;

        private readonly ObjectContext _objectContext;

        private readonly EmployerWithMultipleAccountsUser _employerWithMultipleAccountsUser;

        private ApprenticeDataHelper apprenticeDataHelper;

        public EmployerSteps(ScenarioContext context)
        {
            this.context = context;
            _objectContext = context.Get<ObjectContext>();
            _employerWithMultipleAccountsUser = context.GetUser<EmployerWithMultipleAccountsUser>();
            employerStepsHelper = new EmployerStepsHelper(context);
            apprenticeDataHelper = new ApprenticeDataHelper(context);
            commonStepsHelper = new CommonStepsHelper(context);
            commitmentsDbSqlHelper = context.Get<CommitmentsDbSqlHelper>();
        }


        [When(@"^the Employer approves the apprentice request \(cohort\)$")]
        [Then("^the Employer can approve the cohort$")]
        public async Task WhenTheEmployerApprovesTheApprenticeRequestCohort()
        {
            var page = await employerStepsHelper.OpenCohort();
            await page.EmployerApproveCohort();
        }


        [When("^Employer does not take any action on that cohort for more than 2 weeks$")]
        public async Task WhenEmployerDoesNotTakeAnyActionOnThatCohortForMoreThan2Weeks()
        {
            var cohortRef = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;
            await commitmentsDbSqlHelper.UpdateCohortLastUpdatedDate(cohortRef, DateAndTime.Now.AddDays(-15));

            //test environments are configured to run web job: "ExpireInactiveCohortsWithEmployerAfter2WeeksSchedule" every 4th minute
            await commonStepsHelper.WaitForStatusUpdateAsync(
                getStatusFunc: async () =>
                {
                    return await context.Get<CommitmentsDbSqlHelper>().GetWithPartyValueFromCommitmentsDb(cohortRef);
                },
                "2",
                TimeSpan.FromMinutes(5)
            );
        }


        [Then("^the Employer can access live apprentice records under Manager Your Apprentices section$")]
        public async Task ThenTheEmployerCanAccessLiveApprenticeRecordsUnderManagerYourApprenticesSection()
        {
            await employerStepsHelper.CheckLearnerOnManageYourLearnersPage();
        }


        [When("^Employer reviews the above cohort$")]
        public async Task WhenEmployerReviewsTheAboveCohort()
        {
            await employerStepsHelper.OpenCohort();
        }


        [Then(@"display the ""([^""]*)"" as per table below")]
        public async Task ThenDisplayTheAsPerTableBelow(bool warningMessage)
        {
            var page = new EmployerApproveLearnerDetailsPage(context);

            if (warningMessage)
            {
                var warningMsg = "! Warning One or more of your learners have age eligibility criteria. Check the date of birth is correct or go to the funding rules to check who is eligible.";
                await page.ValidateWarningMessageForFoundationCourses(warningMsg);
            }

            await page.EmployerApproveCohort();
        }


        [When(@"Employer tries to edit live apprentice record by setting age lower than (.*)")]
        public async Task WhenEmployerTriesToEditLiveApprenticeRecordBySettingAgeLowerThan(int lowerAgeLimit)
        {
            var apprentice = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var uln = apprentice.ApprenticeDetails.ULN.ToString();
            var name = apprentice.ApprenticeDetails.FullName;
            var startDate = apprentice.TrainingDetails.StartDate;
            var DoB = startDate.AddYears(-lowerAgeLimit + 1);

            await employerStepsHelper.EmployerLogInToEmployerPortal();
            await new InterimLearnersHomePage(context, false).VerifyPage();
            var apprenticeDetailsPage = await employerStepsHelper.EmployerSearchOpenApprovedLearnerRecord(new LearnersHomePage(context), uln, name);
            await employerStepsHelper.TryEditApprenticeAge(apprenticeDetailsPage, DoB);
        }


        [Then(@"the employer is stopped with an error message for (.*)")]
        public async Task ThenTheEmployerIsStoppedWithAnErrorMessageFor(int ageLimit)
        {
            string errorMessage = $"The apprentice must be at least {ageLimit} years old at the start of their training";
            await new EditLearnerDetailsPage(context).ValidateErrorMessage(errorMessage, "DateOfBirth");
        }


        [Given(@"^Employer create and send an empty cohort to the training provider to add learner details$")]
        [When(@"^the employer create and send an empty cohort to the training provider to add learner details$")]
        public async Task GivenEmployerCreatesEmptyRequestCohort()
        {
            List<Apprenticeship> listOfApprenticeship = new List<Apprenticeship>();
            listOfApprenticeship = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, 1, null, listOfApprenticeship);
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);

            await employerStepsHelper.AddEmptyCohort();
        }


        [Given(@"the Employer(.*) creates (.*) cohorts and sends them to provider to add apprentices")]
        [When(@"the Employer(.*) creates (.*) cohorts and sends them to provider to add apprentices")]
        public async Task TheEmployerCreateACohortAndSendToProviderToAddApprentices(int? employerIndex, int numberOfCohorts)
        {
            employerIndex ??= 1;

            var listOfApprenticeship = context.ContainsKey(ScenarioKeys.ListOfApprenticeship)
                ? context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship) ?? new List<Apprenticeship>()
                : new List<Apprenticeship>();


            var newApprenticeships = await apprenticeDataHelper.CreateApprenticeshipObject(EmployerType.Levy, numberOfCohorts);

            // Pages read the apprenticeship list from context while the cohorts are being created
            listOfApprenticeship.AddRange(newApprenticeships);
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);

            for (var i = 1; i <= numberOfCohorts; i++)
            {
                var apprenticeship = newApprenticeships[i - 1];

                if (employerIndex == 1)
                { // Levy transfer

                    var employer = await apprenticeDataHelper.GetEmployerDetails(
                    _employerWithMultipleAccountsUser.OrganisationName,
                    _employerWithMultipleAccountsUser.AccountDetails,
                    _employerWithMultipleAccountsUser.Username,
                    EmployerType.Levy);
                    apprenticeship.EmployerDetails = employer;

                   var cohort = await employerStepsHelper.EmployerCreateMultipleCohortsViaLevyFundsAndSendsToProvider(apprenticeship);
                   apprenticeship.Cohort = cohort;
                }
                else if (employerIndex == 2)
                { // Direct Transfer

                    var employer = await apprenticeDataHelper.GetEmployerDetails(
                    _employerWithMultipleAccountsUser.SecondOrganisationName,
                    _employerWithMultipleAccountsUser.AccountDetails,
                    _employerWithMultipleAccountsUser.Username,
                    EmployerType.Levy);
                    apprenticeship.EmployerDetails = employer;

                    var cohort = await employerStepsHelper.EmployerCreateCohortsViaDirectTransferAndSendsToProvider(apprenticeship,_employerWithMultipleAccountsUser.OrganisationName);
                    apprenticeship.Cohort = cohort;
                }
                else if (employerIndex == 3)
                { // New reservation

                    var employer = await apprenticeDataHelper.GetEmployerDetails(
                    _employerWithMultipleAccountsUser.ThirdOrganisationName,
                    _employerWithMultipleAccountsUser.AccountDetails,
                    _employerWithMultipleAccountsUser.Username,
                    EmployerType.Levy);
                    apprenticeship.EmployerDetails = employer;

                    var cohort = await employerStepsHelper.EmployerCreateCohortsViaReserveNewFundsOptionAndSendsToProvider(apprenticeship);
                    apprenticeship.Cohort = cohort;
                }
            }

    
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
        }

        [Then("the Employer sees the cohort in Ready to review with status of (.*)")]
        public async Task ThenTheEmployerReviewsCohort(string status)
        {
            await employerStepsHelper.ReadyForReviewCohort(status);
        }


        [Then("^the NonLevyEmployer reviews and approves the cohort from dynamic homepage$")]
        public async Task ThenTheEmployerReviewsCohortApproveFromDynamicHomePage()
        {
            await employerStepsHelper.ReadyForReviewOnDynamicHomePage("Ready for approval");
            await new EmployerApproveLearnerDetailsPage(context).EmployerApproveCohort();
        }


        [Then("^Add The employer can follow the link from dynamicHomePage to view live apprentice details$")]
        public async Task ThenTheNLEmployerViewLIveApprenticeFromDynamicHomePage()
        {
            var apprentice = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var name = apprentice.ApprenticeDetails.FullName;

            await employerStepsHelper.GoToLiveApprenticeshipPageFromDynamicHomePage();
            await new LearnerDetailsPage(context, name).EmployerVerifyApprenticeStatusAndDetails(ApprenticeshipStatus.Live, "Learner confirmation", "Unconfirmed");
        }


        [When("the Employer approves the cohort and sends to provider")]
        public async Task ThenTheEmployerApprovesCohort()
        {
            var page = new EmployerApproveLearnerDetailsPage(context);
            await page.EmployerApproveCohort();
        }


        [Then("verify RPL details")]
        public async Task ThenTheEmployerVerifyRPLDetails()
        {
            var apprenticeships = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var page = new EmployerApproveLearnerDetailsPage(context);
            await page.VerifyRPLDetails(apprenticeships);
        }


        [Given("^the Employer logins using an existing NonLevy Account which has reached it max reservations limit$")]
        public async Task GivenTheEmployerLoginsUsingAnExistingNonLevyAccountWhichHasReachedItMaxReservationsLimit()
        {
            var listOfApprenticeship = new List<Apprenticeship>();
            Apprenticeship apprenticeship = await new ApprenticeDataHelper(context).CreateEmptyCohortObject(EmployerType.NonLevyUserAtMaxReservationLimit);
            listOfApprenticeship.Add(apprenticeship);
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
        }


        [When("^the Employer tries to add another apprentice to an existing cohort$")]
        public async Task WhenTheEmployerTriesToAddAnotherApprenticeToAnExistingCohort()
        {
            await employerStepsHelper.OpenAnyDraftCohort();
        }


        [Then("^the Employer is blocked with a shutter page for existing cohort$")]
        public async Task ThenTheEmployerIsBlockedWithAShutterPageForExistingCohort()
        {
            await new EmployerApproveLearnerDetailsPage(context).TryClickAddAnotherApprenticeLink();
        }


        [Then("^the employer is blocked to create new reservations$")]
        public async Task ThenTheEmployerIsBlockedToCreateNewReservations()
        {
            await employerStepsHelper.EmployerTriesToCreateReservation();
        }


        [When("^The nonlevyemployer follows the link from dynamicHomePage to create a new reservation$")]
        public async Task WhenTheNonLevyEmployerReservesFundingFromReservedPanel()
        {
            await employerStepsHelper.EmployerTriesToCreateReservationOnDynamicHomepage();
        }


        [Then("^The nonlevyemployer follows the link from dynamicHomePage to create a cohort and send it to the training Provider$")]
        public async Task WhenTheNonLevyEmployerSendCohortToProvider()
        {
            var dynamicHomepageSettingAnApprenticeShip = new ContinueSettingupAnApprenticeshipDynamicHomepage(context);
            var continueSettingupAnApprenticeshipPage = await dynamicHomepageSettingAnApprenticeShip.Continue();
            await continueSettingupAnApprenticeshipPage.DoNotCreateAdvertForThisApprenticeship();
            await employerStepsHelper.AddEmptyCohortFromNonLevyReserveFundsAddApprenticePage();
        }


        [Given(@"Employer creates a reservation with GSO standard and tries to add a learner to the reservation")]
        public async Task GivenEmployerCreatesAReservationWithGSOStandardAndTriesToAddALearnerToTheReservation()
        {
            await employerStepsHelper.EmployerCreatesReservationAndAddsApprentice();
        }


        [Then(@"the Employer is blocked with error message")]
        public async Task ThenTheEmployerIsBlockedWithErrorMessage()
        {
            var errorMsg1 = "You previously chose a reservation for an apprenticeship unit.";
            var errorMsg2 = "You must send a request to your training provider to add learners doing apprenticeship units.";
            var employerSelectRoutePage = new HowWouldYouLikeToAddLearnersPage(context);
            await employerSelectRoutePage.VerifyErrorMessage(new[] { errorMsg1, errorMsg2 });
        }


        [When(@"Employer sends an empty cohort to the provider")]
        public async Task WhenEmployerSendsAnEmptyCohortToTheProvider()
        {
            var employerSelectRoutePage = new HowWouldYouLikeToAddLearnersPage(context);
            var page = await employerSelectRoutePage.SelectProviderAddApprencticesAndSend();
            var cohortRef = await page.GetCohortId();
            await commonStepsHelper.SetCohortDetails(cohortRef, "Ready for review", "Under review with Provider");
        }


    }

}