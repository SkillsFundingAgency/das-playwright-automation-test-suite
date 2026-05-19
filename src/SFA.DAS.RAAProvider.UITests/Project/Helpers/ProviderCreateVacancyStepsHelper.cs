using SFA.DAS.RAA.Service.Project.Helpers;
using SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAProvider.UITests.Project.Helpers
{
    public class ProviderCreateVacancyStepsHelper(ScenarioContext context, bool newTab) : CreateAdvertVacancyBaseStepsHelper()
    {
        private bool _isMultiOrg;
        protected bool IsFoundationAdvert => context.ContainsKey("isFoundationAdvert") && (bool)context["isFoundationAdvert"];

        private string _hashedid = string.Empty;
        private readonly RecruitmentProviderHomePageStepsHelper _recruitmentProviderHomePageStepsHelper = new(context);

        public ProviderCreateVacancyStepsHelper(ScenarioContext context) : this(context, false) { }

        public async Task<VacancyReferencePage> CreateANewVacancyForSpecificEmployer(string employername, string hashedid)
        {
            _hashedid = hashedid;

            return await CreateANewVacancy(employername);
        }

        public async Task<VacancyReferencePage> CreateANewVacancyForRandomEmployer() => await CreateANewVacancy(true);

        public async Task<VacancyReferencePage> CreateAnonymousVacancy() => await CreateANewVacancy(RAAConst.Anonymous);

        public async Task<VacancyReferencePage> CreateOfflineVacancy() => await CreateANewVacancy(false);

        public async Task<VacancyReferencePage> CreateVacancyForWageType(string wageType) => await CreateANewAdvertOrVacancy(string.Empty, "employer", wageType, true, true, true);

        public async Task<VacancyReferencePage> CreateVacancyForLocationTypes(string locationType, bool enterQuestion1, bool enterQuestion2) => 
            await CreateANewAdvertOrVacancy(string.Empty, locationType, RAAConst.NationalMinWages, true, enterQuestion1, enterQuestion2);

        public async Task<VacancyReferencePage> CreateVacancyForLocationAndWageTypes(string locationType, string wageType) => await CreateANewAdvertOrVacancy(string.Empty, locationType, wageType, true, true, true);

        private async Task<VacancyReferencePage> CreateANewVacancy(string employername) => await CreateANewVacancy(employername, true);

        private async Task<VacancyReferencePage> CreateANewVacancy(bool isApplicationMethodFAA) => await CreateANewVacancy(string.Empty, isApplicationMethodFAA);

        private async Task<VacancyReferencePage> CreateANewVacancy(string employername, bool isApplicationMethodFAA) => await CreateANewAdvertOrVacancy(employername, "employer", RAAConst.NationalMinWages, isApplicationMethodFAA, true, true);

        private async Task<VacancyReferencePage> CreateANewAdvertOrVacancy(string employername, string locationType, string wageType, bool isApplicationMethodFAA, bool enterQuestion1, bool enterQuestion2)
        {
            return await CreateANewAdvertOrVacancy(employername, locationType, false, wageType, isApplicationMethodFAA, true, enterQuestion1, enterQuestion2);
        }

        protected override async Task<CreateAnApprenticeshipAdvertOrVacancyPage> AboutTheEmployer(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, string employername, bool disabilityConfidence, bool isApplicationMethodFAA)
        {
            var page = await createAdvertPage.EmployerName();

            var page1 = await page.ChooseEmployerNameForEmployerJourney(employername);

            var page2 = await page1.EnterEmployerDescriptionAndGoToContactDetailsPage(disabilityConfidence, optionalFields);

            var page3 = await page2.EnterProviderContactDetails(optionalFields);

            return await page3.SelectApplicationMethod_Employer(isApplicationMethodFAA);
        }

        protected override async Task<CreateAnApprenticeshipAdvertOrVacancyPage> Application(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, bool enterQuestion1, bool enterQuestion2)
        {
            var page = await createAdvertPage.EnterAdditionalQuestionsForApplicants();

            return await page.CompleteAllAdditionalQuestionsForApplicants(IsFoundationAdvert, enterQuestion1, enterQuestion2);
        }

        protected override async Task<CreateAnApprenticeshipAdvertOrVacancyPage> SkillsAndQualifications(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
        {
            if (IsFoundationAdvert)
            {
                var page = await createAdvertPage.FutureProspects();

                var page1 = await page.EnterFutureProspect();

                return await page1.EnterThingsToConsiderAndReturnToCreateAdvert(optionalFields);
            }
            else
            {
                var page = await createAdvertPage.Skills();

                var page1 = await page.SelectSkillAndGoToQualificationsPage();

                var page2 = await page1.SelectYesToAddQualification();

                var page3 = await page2.EnterQualifications();

                var page4 = await page3.ConfirmQualificationsAndGoToFutureProspectsPage();

                var page5 = await page4.EnterFutureProspect();

                return await page5.EnterThingsToConsiderAndReturnToCreateAdvert(optionalFields);
            }
        }

        protected override async Task<CreateAnApprenticeshipAdvertOrVacancyPage> EmploymentDetails(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, string locationType, string wageType)
        {
            var page = await createAdvertPage.ImportantDates();

            var page1 = await page.EnterImportantDates();

            var page2 = await page1.EnterDuration();

            var page3 = await page2.ChooseWage_Employer(wageType);

            var page4 = await page3.SubmitExtraInformationAboutPay();

            var page5 = await page4.SubmitNoOfPositionsAndNavigateToChooseLocationPage();

            return await page5.ChooseAddressAndGoToCreateApprenticeshipPage(locationType);
        }

        // protected override CreateAnApprenticeshipAdvertOrVacancyPage AdvertOrVacancySummary(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
        // {
        //     return EnterVacancyTitle(NavigateToAdvertTitle(createAdvertPage))
        //         .EnterTrainingTitle()
        //         .ConfirmTrainingAndContinueToSummaryPage()
        //         .EnterShortDescription()
        //         .EnterShortDescriptionOfWhatApprenticeWillDo()
        //         .EnterAllDescription();
        // }

        protected override async Task<CreateAnApprenticeshipAdvertOrVacancyPage> AdvertOrVacancySummary(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
        {
            var page = await EnterAdvertTitle(createAdvertPage);

            return await AdvertSummary(page, IsFoundationAdvert);
        }

        protected virtual async Task<ApprenticeshipTrainingPage> EnterAdvertTitle(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
        {
            var page = await NavigateToAdvertTitle(createAdvertPage);

            return await page.EnterAdvertTitleMultiOrgProvider();
        }

        protected override async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipAdvertOrVacancy()
        {
            var page = await GoToRecruitmentHomePage();

            var page1 = await page.GoToViewAllVacancyPage();
            var page2 = await page1.ReturnToDashboard();

            var page3 = await page2.CreateVacancy();
            var page4 = await page3.StartNow();

            (CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, bool isMultiOrg) = await page4.SelectEmployer(_hashedid);

            _isMultiOrg = isMultiOrg;

            return createAdvertPage;
        }

        //internal RecruitmentHomePage CancelAdvert()
        //{
        //    (CreateAnApprenticeshipAdvertOrVacancy()).AdvertTitle().EnterVacancyTitle().EmployerCancelAdvert();
        //    return new RecruitmentHomePage(context);
        //}

        //    internal async Task<RecruitmentHomePage> CancelAdvert()
        //{
        //    var page = await CreateAnApprenticeshipAdvertOrVacancy();

        //    var page1 = await EnterAdvertTitleMultiOrg(page);

        //    await page1.EmployerCancelAdvert();

        //    return await VerifyPageHelper.VerifyPageAsync(context, () => new RecruitmentHomePage(context));
        //}

        //internal CreateAnApprenticeshipAdvertOrVacancyPage CreateDraftAdvert() => CreateDraftAdvert(CreateAnApprenticeshipAdvertOrVacancy(), false);

        //internal CreateAnApprenticeshipAdvertOrVacancyPage CreateDraftAdvert(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, bool createFirstDraftAdvert)
        //{

        //    return EmploymentDetails(createFirstDraftAdvert ? FirstAdvertSummary(createAdvertPage) : AdvertOrVacancySummary(createAdvertPage), "employer", RAAConst.NationalMinWages);
        //}

        //private static CreateAnApprenticeshipAdvertOrVacancyPage FirstAdvertSummary(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage) =>
        //    AdvertSummary(NavigateToAdvertTitle(createAdvertPage).EnterVacancyTitleForTheFirstAdvert().SelectYes(), false);

        private static async Task<CreateAnApprenticeshipAdvertOrVacancyPage> AdvertSummary(ApprenticeshipTrainingPage page, bool isFoundationAdvert)
        {
            var page1 = await page.EnterTrainingTitle();

            var page2 = await page1.ConfirmTrainingAndContinueToSummaryPage();

            //var page3 = await page2.SelectTrainingProvider();

            //var page4 = await page3.ConfirmProviderAndContinueToSummaryPage();

            var page3 = await page2.EnterShortDescription();

            var page4 = await page3.EnterShortDescriptionOfWhatApprenticeWillDo();

            return await page4.EnterAllDescription();
        }

        //internal VacancyReferencePage CloneAnAdvert() => SubmitAndSetVacancyReference(GoToRecruitmentHomePage().SelectLiveVacancy().CloneVacancy()
        //    .SelectYes().UpdateTitle().UpdateVacancyTitleAndGoToCheckYourAnswersPage().UpdateAdditionalQuestion().UpdateAllAdditionalQuestionsAndGoToCheckYourAnswersPage(true, true));

        protected virtual async Task<RecruitmentHomePage> GoToRecruitmentHomePage() => await _recruitmentProviderHomePageStepsHelper.GoToRecruitmentProviderHomePage(true);


    }
}