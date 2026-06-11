using Azure;
using Reqnroll.Assist;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class ProviderSteps
    {
        private readonly ScenarioContext context;
        private readonly CommonStepsHelper commonStepsHelper;
        private readonly ProviderHomePageStepsHelper providerHomePageStepsHelper;
        private readonly ProviderStepsHelper providerStepsHelper;
        private readonly DbSteps dbSteps;
        private readonly LearnerDataOuterApiSteps learnerDataOuterApiSteps;

        public ProviderSteps(ScenarioContext _context)
        {
            context = _context;
            commonStepsHelper = new CommonStepsHelper(context);
            providerHomePageStepsHelper = new ProviderHomePageStepsHelper(context);
            providerStepsHelper = new ProviderStepsHelper(context);
            dbSteps = new DbSteps(context);
            learnerDataOuterApiSteps = new LearnerDataOuterApiSteps(context);
        }

        [When(@"^Provider sends an apprentice request \(cohort\) to the employer by selecting same apprentices$")]
        public async Task WhenProviderSendsAnApprenticeRequestCohortToTheEmployerBySelectingSameApprentices()
        {
            await providerStepsHelper.ProviderCreateAndApproveACohortViaIlrRoute();
            await commonStepsHelper.SetCohortDetails(null, "Under review with Employer", "Ready for approval");
        }

        [When("^creates reservations for each learner$")]
        public async Task WhenCreatesReservationsForEachLearner()
        {
            await providerStepsHelper.ProviderReserveFunds();
        }

        [When(@"^sends an apprentice request \(cohort\) to the employer by selecting apprentices from ILR list and reservations$")]
        public async Task WhenSendsAnApprenticeRequestCohortToTheEmployerBySelectingApprenticesFromILRListAndReservations()
        {
            var page = await providerStepsHelper.ProviderAddsFirstApprenitceUsingReservation();
            var page1 = await providerStepsHelper.ProviderAddsApprenticesUsingReservation(page, 1);
            await providerStepsHelper.ProviderApproveCohort(page1);
            await commonStepsHelper.SetCohortDetails(null, "Under review with Employer", "Ready for approval");
        }

        [Then("^return the cohort back to the Provider$")]
        public async Task ThenReturnTheCohortBackToTheProvider()
        {
            var cohortRef = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;

            await providerHomePageStepsHelper.GoToProviderHomePage(false);
            await new ProviderHomePage(context).GoToApprenticeRequestsPage();

            var page = new ApprenticeRequests_ProviderPage(context);
            await page.NavigateToBingoBox(ApprenticeRequests.ReadyForReview);
            await page.VerifyCohortExistsAsync(cohortRef);

        }

        [Then("^Provider can access live apprentice records under Manager Your Apprentices section$")]
        internal async Task<ManageYourLearners_ProviderPage> ThenProviderAccessLiveApprenticeRecords()
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            await providerHomePageStepsHelper.GoToProviderHomePage(true);
            await UserNavigatesToManageYourApprenticesPage();
            var page = new ManageYourLearners_ProviderPage(context);

            foreach (var apprentice in listOfApprenticeship)
            {
                var uln = apprentice.ApprenticeDetails.ULN.ToString();
                var name = apprentice.ApprenticeDetails.FullName;

                await page.VerifyApprenticeFound(uln, name);
            }

            return page;
        }


        [Then(@"system stop user to add that apprentice with an error message for ""([^""]*)""")]
        public async Task ThenSystemStopUserToAddThatApprenticeWithAnErrorMessageFor(string ageLimit)
        {
            int age = int.Parse(ageLimit);
            string errorMsg = age < 20 ? $"The apprentice must be at least {age} years old at the start of their training" : $"The apprentice must be {age - 1} years or under at the start of their training";
            var page = await new ProviderStepsHelper(context).ProviderCreateACohortViaIlrRouteWithInvalidDoB();
            await page.VerfiyErrorMessage("DateOfBirth", errorMsg);
            await page.NavToHomePage();
        }


        [When(@"Provider tries to edit live apprentice record by setting age higher than (.*)")]
        public async Task WhenProviderTriesToEditLiveApprenticeRecordBySettingAgeHigherThan(int upperAgeLimit)
        {
            var apprentice = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var uln = apprentice.ApprenticeDetails.ULN.ToString();
            var name = apprentice.ApprenticeDetails.FullName;
            var startDate = apprentice.TrainingDetails.StartDate;
            var DoB = startDate.AddYears(-upperAgeLimit - 1);

            await providerHomePageStepsHelper.GoToProviderHomePage(true);
            await UserNavigatesToManageYourApprenticesPage();

            var apprenticeDetailsPage = await providerStepsHelper.ProviderSearchOpenApprovedApprenticeRecord(new ManageYourLearners_ProviderPage(context), uln, name);
            await providerStepsHelper.TryEditApprenticeAge(apprenticeDetailsPage, DoB);
        }


        [Then(@"the provider is stopped with an error message for (.*)")]
        public async Task ThenTheProviderIsStoppedWithAnErrorMessageFor(int ageLimit)
        {
            string errorMessage = $"The apprentice must be younger than {ageLimit} years old at the start of their training";
            await new EditLearnerDetails_ProviderPage(context).ValidateErrorMessage(errorMessage, "DateOfBirth");
        }


        [Then("^apprentice\\/learner record is no longer available on SelectLearnerFromILR page$")]
        public async Task ThenApprenticeLearnerRecordIsNoLongerAvailableOnSelectLearnerFromILRPage()
        {
            await providerStepsHelper.ProviderVerifyLearnerNotAvailableForSelection();
        }


        [When("^Provider tries to add a new apprentice using details from table below$")]
        public async Task WhenProviderTriesToAddANewApprenticeUsingDetailsFromTableBelow(Table table)
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var apprentice = listOfApprenticeship.FirstOrDefault();
            var originalStartDate = apprentice.TrainingDetails.StartDate;
            var originalEndDate = apprentice.TrainingDetails.EndDate;
            var OltdDetails = table.CreateSet<OltdDetails>().ToList();

            foreach (var item in OltdDetails)
            {
                //Update valid apprentice object with new start and end dates. Then push it as new apprentice details on SLD endpoint
                apprentice.TrainingDetails.StartDate = originalStartDate.AddMonths(Convert.ToInt32(item.NewStartDate));
                apprentice.TrainingDetails.EndDate = originalEndDate.AddMonths(Convert.ToInt32(item.NewEndDate));

                listOfApprenticeship[0] = apprentice;
                context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);

                // Push data on SLD end point  
                await new LearnerDataOuterApiSteps(context).SLDPushDataIntoAS();

                // Try to add above apprentice and validate error message  
                var page = await providerStepsHelper.GoToSelectApprenticeFromILRPage();
                var page1 = await providerStepsHelper.TryAddFirstApprenticeFromILRList(page);
                var oltdErrorMsg = "The date overlaps with existing dates for the same apprentice";


                if (item.DisplayOverlapErrorOnStartDate)
                    await page1.VerfiyErrorMessage("StartDate", oltdErrorMsg);
                else
                    await page1.VerfiyErrorMessage("StartDate", "");

                if (item.DisplayOverlapErrorOnEndDate)
                    await page1.VerfiyErrorMessage("EndDate", oltdErrorMsg);
                else
                    await page1.VerfiyErrorMessage("EndDate", "");

            }

        }

        [When("^user navigates to Apprentice requests page$")]
        public async Task WhenUserNavigatesToApprenticeRequestsPage()
        {
            await new ProviderHomePage(context).GoToApprenticeRequestsPage();
        }       

        [When("^the provider adds apprentices along with RPL details and sends to employer to review$")]
        public async Task WhenTheProviderAddsApprenticesAlongWithRPLDetailsAndSendsToEmployerToReview()
        {
            var cohortRef = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(true);
            var page = await new ProviderStepsHelper(context).ProviderAddApprencticesFromIlrRoute();
            await page.ProviderSendCohortForEmployerReview();
            await commonStepsHelper.SetCohortDetails(cohortRef, "Under review with Employer", "Ready for review");
        }

        [Then("^the provider adds apprentice details, approves the cohort and sends it to the employer for approval$")]
        [Then("^the provider can add apprentice details and approve the cohort$")]
        public async Task ThenTheProviderAddsApprenticeDetailsApprovesTheCohortAndSendsItToTheEmployerForApproval()
        {
            var cohortRef = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;

            var page = await providerStepsHelper.ProviderOpenTheCohort(cohortRef);
            await providerStepsHelper.AddOtherApprenticesFromILRListWithRPL(page, 0);
            await page.ProviderApproveCohort();
            await commonStepsHelper.SetCohortDetails(cohortRef, "Under review with Employer", "Ready for approval");
        }

        [Then("^the provider adds apprentice details, select an existing reservation, approves the cohort and sends it to the employer for approval$")]
        public async Task ThenTheProviderAddsApprenticeDetailsSelectsExistingReservationApprovesTheCohortAndSendsItToTheEmployerForApproval()
        {
            var cohortRef = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;

            var page = await providerStepsHelper.ProviderOpenTheCohort(cohortRef);
            await providerStepsHelper.ProviderAddApprencticesFromIlrRouteUseExistingReservation(page);
            await page.ProviderApproveCohort();
            await commonStepsHelper.SetCohortDetails(cohortRef, "Under review with Employer", "Ready for approval");
        }

        [Then("^provider cannot add apprentices as they do not have permissions to create reservations$")]
        public async Task ThenProviderCannotAddApprenticesAsTheyDoNotHavePermissionsToCreateReservations()
        {
            var cohortRef = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;
            var page = await providerStepsHelper.ProviderOpenTheCohort(cohortRef);
            var page1 = await page.ClickOnAddAnotherApprenticeLink_ToSelectEntryMthodPage();
            await page1.SelectOptionToAddApprenticesFromILRList_InsufficientPermissionsRoute();
            var page2 = new YouNeedPermissionToDoThisPage(context);
            var page3 = await page2.ClickOnGoToHomepageButton();
        }



        [Then("^the provider approves the cohorts$")]
        public async Task ThenTheProviderApprovesCohort()
        {
            var cohortRef = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;

            var page = await providerStepsHelper.ProviderOpenTheCohort(cohortRef);
            await page.ProviderApprovesCohortAfterEmployerApproval();
        }

        [When("^user navigates to Manage Your Apprentices page$")]
        public async Task UserNavigatesToManageYourApprenticesPage()
        {
            await new ProviderHomePage(context).GoToProviderManageYourApprenticePage();
        }

        [Given(@"^Provider sends an apprentice request \(cohort\) to an employer$")]
        public async Task GivenProviderSendsAnApprenticeRequestCohortToAnEmployer()
        {
            await learnerDataOuterApiSteps.ProviderSubmitsAnILRRecord(1, EmployerType.Levy.ToString());
            await learnerDataOuterApiSteps.SLDPushDataIntoAS();

            var page = await new ProviderStepsHelper(context).ProviderCreateAndApproveACohortViaIlrRoute();
            var cohortRef = context.GetValue<List<Apprenticeship >>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;

            await page.NavigateToBingoBox(ApprenticeRequests.WithEmployers);
            await page.VerifyCohortExistsAsync(cohortRef);

        }


        [Then("^cohort is sent back to the provider$")]
        public async Task ThenCohortIsSentBackToTheProvider()
        {
            var cohortRef = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;
            await new ProviderHomePage(context).GoToApprenticeRequestsPage();
            var page = new ApprenticeRequests_ProviderPage(context);
            await page.VerifyCohortExistsAsync(cohortRef);
        }

        [When(@"Provider resubmits ILR with apprentice aged below ""([^""]*)""")]
        public async Task WhenProviderResubmitsILRWithApprenticeAgedBelow(string lowerAgeLimit)
        {
            var age = int.Parse(lowerAgeLimit);
            await providerStepsHelper.UpdateDobAndReSubmitIlrData(age-1, age);
            var page = await providerStepsHelper.GoToSelectApprenticeFromILRPage();
            await providerStepsHelper.TryAddFirstApprenticeFromILRList(page);
        }


        [Then(@"system allows to approve apprentice details with a ""([^""]*)"" if their age is in range of (.*) - (.*) years")]
        public async Task ThenSystemAllowsToApproveApprenticeDetailsWithAIfTheirAgeIsInRangeOf_Years(bool displayWarningMsg, int lowerAgeLimit, int upperAgeLimit)
        {

            await providerStepsHelper.UpdateDobAndReSubmitIlrData(lowerAgeLimit, upperAgeLimit);
            var page = await providerStepsHelper.GoToSelectApprenticeFromILRPage();
            var page1 = await providerStepsHelper.AddFirstApprenticeFromILRList(page);
            if (displayWarningMsg)
            {
                var warningMsg = "! Warning One or more of your apprenticeships have age eligibility criteria. Check the date of birth is correct or go to the funding rules to check who is eligible.";
                await page1.ValidateWarningMessageForFoundationCourses(warningMsg);
            }            
            await providerStepsHelper.ProviderApproveCohort(page1);
            await commonStepsHelper.SetCohortDetails(null, "Under review with Employer", "Ready for approval");

        }



        [When("^the Provider tries to add another apprentice to an existing cohort$")]
        public async Task WhenTheProviderTriesToAddAnotherApprenticeToAnExistingCohort()
        {
            Apprenticeship apprenticeship = await new ApprenticeDataHelper(context).CreateEmptyCohortObject(EmployerType.NonLevyUserAtMaxReservationLimit);
            apprenticeship = await new DbSteps(context).FindUnapprovedCohortReference(apprenticeship, ApprenticeRequests.ReadyForReview);
            
            await providerStepsHelper.ProviderOpenTheCohort(apprenticeship.Cohort.Reference);
        }

        [Then("^the Provider is blocked with a shutter page for existing cohort$")]
        public async Task ThenTheProviderIsBlockedWithAShutterPageForExistingCohort()
        {
            var page = new ApproveApprenticeDetailsPage(context);
            var page1 = await page.ClickOnAddAnotherApprenticeLink_ToSelectEntryMthodPage();
            var page2 = await page1.SelectOptionToAddApprenticesFromILRList_FundingRestrictionsRoute();
            await page2.ClickOnReturnToAccountButton();
        }


        [Then("^the Provider is blocked to create new reservations$")]
        public async Task ThenTheProviderIsBlockedToCreateNewReservations()
        {
            var agreementId = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().EmployerDetails.AgreementId;

            await new ProviderHomePage(context).ClickFundingLink();
            var page = new ReserveFundingForNonLevyEmployersPage(context);
            var page1 = await page.ClickOnReserveFundingButton();
            var page2 = await page1.ChooseAnEmployer(agreementId);
            await page2.ConfirmNonLevyEmployerWithFundingRestrictions();
        }

        [Then(@"^Provider can access live learner records and modify them as per below table:$")]
        public async Task ThenProviderCanAccessLiveLearnerRecordsAndModifyThemAsPerBelowTable(Table table)
        {
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            await providerHomePageStepsHelper.GoToProviderHomePage(true);
            await UserNavigatesToManageYourApprenticesPage();
            var page = new ManageYourLearners_ProviderPage(context);

            foreach (var apprentice in listOfApprenticeship)
            {
                var uln = apprentice.ApprenticeDetails.ULN.ToString();
                var name = apprentice.ApprenticeDetails.FullName;

                await page.VerifyApprenticeFound(uln, name);
                var page2 = await page.SelectViewCurrentApprenticeDetails(name, uln);
                var page3 = await page2.ClickOnEditApprenticeDetailsLink();
                await page3.ValidateEditability(table);
                await page3.ClickOnCancelAndReturnLink();
                await page2.ReturnBackToManageYourApprenticesPage();
            }
        }

        [Then(@"Provider can add learners to above cohort using existing and new reservations")]
        public async Task ThenProviderCanAddLearnersToAboveCohortUsingExistingAndNewReservations()
        {
            var cohortRef = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;

            var page = await providerStepsHelper.ProviderOpenTheCohort(cohortRef);
            await providerStepsHelper.ProviderAddsApprenticesUsingReservation(page, 0);            
        }

        [Then(@"Provider can approve the cohort and send it to the employer for final approval")]
        public async Task ThenProviderCanApproveTheCohortAndSendItToTheEmployerForFinalApproval()
        {
            var cohortRef = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;

            await new ApproveApprenticeDetailsPage(context).ProviderApproveCohort();
            await commonStepsHelper.SetCohortDetails(cohortRef, "Under review with Employer", "Ready for approval");
        }



    }
    public class OltdDetails
    {
        public string NewStartDate { get; set; }
        public string NewEndDate { get; set; }
        public bool DisplayOverlapErrorOnStartDate { get; set; }
        public bool DisplayOverlapErrorOnEndDate { get; set; }
    }
}
