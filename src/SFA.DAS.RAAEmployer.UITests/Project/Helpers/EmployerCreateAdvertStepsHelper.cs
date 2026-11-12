using Azure;
using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;
using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages.DynamicHomePageEmployer;

namespace SFA.DAS.RAAEmployer.UITests.Project.Helpers;

public class EmployerCreateDraftAdvertStepsHelper(ScenarioContext context) : EmployerCreateAdvertStepsHelper(context)
{
    internal async Task<VacancyReferencePage> SubmitDraftAdvert(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
    {
        var page = await CompleteAboutTheEmployer(createAdvertPage);

        var page1 = await page.EnterAdditionalQuestionsForApplicants();

        var page2 = await page1.CompleteAllAdditionalQuestionsForApplicants(IsFoundationAdvert, true, true);

        return await CheckAndSubmitAdvert(page2);
    }

    internal async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CompleteDraftAdvert(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
    {
        var page = await CompleteAboutTheEmployer(createAdvertPage);

        var page1 = await page.EnterAdditionalQuestionsForApplicants();

        var page2 = await page1.CompleteAllAdditionalQuestionsForApplicants(IsFoundationAdvert, true, true);

        var page3 = await page2.CheckYourAnswers();

        var page4 = await page3.PreviewAdvert();

        var page5 = await page4.ReturnToPreviousPage();

        var page6 = await page5.DeleteVacancy();

        return await page6.NoDeleteVacancy();
    }


    internal async Task CompleteDeleteOfDraftVacancy()
    {
        var page = new CreateAnApprenticeshipAdvertOrVacancyPage(context);

        await page.VerifyPage();

        var page1 = await page.CheckYourAnswers();

        var page2 = await page1.PreviewAdvert();

        var page3 = await page2.ReturnToPreviousPage();

        var page4 = await page3.DeleteVacancy();

        await page4.YesDeleteAdvert();
    }

    internal async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateDraftAdvert()
    {
        return await CreateDraftAdvert(await CreateAnApprenticeshipAdvertOrVacancy(), false);
    }

    internal async Task<YourApprenticeshipAdvertsHomePage> CancelAdvert()
    {
        var page = await CreateAnApprenticeshipAdvertOrVacancy();

        var page1 = await EnterAdvertTitleMultiOrg(page);

        await page1.EmployerCancelAdvert();

        return await VerifyPageHelper.VerifyPageAsync(context, () => new YourApprenticeshipAdvertsHomePage(context));
    }
}

//public class EmployerCreateAdvertPrefStepsHelper(ScenarioContext context, RAAEmployerUser rAAEmployerUser) : EmployerCreateAdvertStepsHelper(context)
//{
//    protected override YourApprenticeshipAdvertsHomePage GoToRecruitmentHomePage() => rAAEmployerLoginHelper.GoToRecruitmentHomePage(rAAEmployerUser);

//    public CreateAnApprenticeshipAdvertOrVacancyPage GoToCreateAnApprenticeshipAdvertPage()
//    {
//        return rAAEmployerLoginHelper.GoToCreateAnAdvertHomePage(rAAEmployerUser).GoToCreateAnApprenticeshipAdvertPage();
//    }

//    protected override ApprenticeshipTrainingPage EnterAdvertTitle(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage) =>
//        NavigateToAdvertTitle(createAdvertPage).EnterVacancyTitle();
//}

public class EmployerCreateAdvertStepsHelper(ScenarioContext context) : CreateAdvertVacancyBaseStepsHelper()
{
    protected bool IsFoundationAdvert => context.ContainsKey("isFoundationAdvert") && (bool)context["isFoundationAdvert"];

    protected readonly ScenarioContext context = context;

    protected readonly RAAEmployerLoginStepsHelper rAAEmployerLoginHelper = new(context);

    internal static async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateFirstDraftAdvert_PrefTest(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
    {
        return await FirstAdvertSummary(createAdvertPage);
    }

    internal async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateFirstDraftAdvert(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
    {
        return await CreateDraftAdvert(createAdvertPage, true);
    }

    internal async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateDraftAdvert(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, bool createFirstDraftAdvert)
    {
        CreateAnApprenticeshipAdvertOrVacancyPage page;

        if (createFirstDraftAdvert)
        {
            page = await FirstAdvertSummary(createAdvertPage);
        }
        else
        {
            page = await AdvertOrVacancySummary(createAdvertPage);
        }

        return await EmploymentDetails(page, "employer", RAAConst.NationalMinWages);
    }

    internal async Task CreateFirstAdvertAndSubmit(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
    {
        createAdvertPage = await CreateFirstDraftAdvert(createAdvertPage);

        createAdvertPage = await CompleteAboutTheEmployer(createAdvertPage);

        createAdvertPage = await Application(createAdvertPage, true, true);

        await CheckAndSubmitAdvert(createAdvertPage);
    }

    internal async Task<CreateAnApprenticeshipAdvertOrVacancyPage> AddAnAdvert()
    {
        await new RecruitmentDynamicHomePage(context, true).ContinueToCreateAdvert();

        var page = new DoYouNeedToCreateAnAdvertPage(context);

        var page1 = await page.ClickYesRadioButtonTakesToRecruitment();

        return await page1.GoToCreateAnApprenticeshipAdvertPage();
    }

    internal async Task CreateOfflineVacancy()
    {
        await CreateANewAdvert(false);

        var page = await SearchVacancyByVacancyReference();

        var page1 = await page.NavigateToViewAdvertPage();

        await page1.VerifyDisabilityConfident();
    }

    internal async Task<VacancyReferencePage> CloneAnAdvert()
    {
        var page = await GoToRecruitmentHomePage();

        var page1 = await page.SelectLiveAdvert();

        var page2 = await page1.CloneAdvert();

        var page3 = await page2.SelectYes();

        var page4 = await page3.UpdateTitle();

        var page5 = await page4.UpdateVacancyTitleAndGoToCheckYourAnswersPage();

        var page6 = await page5.UpdateAdditionalQuestion();

        var page7 = await page6.UpdateAllAdditionalQuestionsAndGoToCheckYourAnswersPage(true, true);

        return await SubmitAndSetVacancyReference(page7);
    }

    internal async Task<VacancyReferencePage> CloneAnArchivedAdvert()
    {
        var page = await GoToRecruitmentHomePage();

        var page1 = await page.SelectArchivedAdvert();

        var page2 = await page1.CloneAdvert();

        var page3 = await page2.SelectYes();

        var page4 = await page3.UpdateTitle();

        var page5 = await page4.UpdateVacancyTitleAndGoToCheckYourAnswersPage();

        var page6 = await page5.UpdateAdditionalQuestion();

        var page7 = await page6.UpdateAllAdditionalQuestionsAndGoToCheckYourAnswersPage(true, true);

        return await SubmitAndSetVacancyReference(page7);
    }

    internal async Task<VacancyReferencePage> CreateANewAdvert_WageType(string wageType) => await CreateANewAdvert(string.Empty, "employer", false, wageType);

    internal async Task<VacancyReferencePage> CreateANewAdvert_LocationAndWageType(string locationType, string wageType) => await CreateANewAdvert(string.Empty, locationType, false, wageType);

    internal async Task<VacancyReferencePage> CreateANewAdvert() => await CreateANewAdvert(RAAConst.LegalEntityName);

    internal async Task<VacancyReferencePage> CreateANewAdvert(string employername) => await CreateANewAdvert(employername, "employer");

    internal async Task<VacancyReferencePage> CreateANewAdvert(string employername, string locationType) => await CreateANewAdvert(employername, locationType, false, RAAConst.NationalMinWages);

    internal async Task<VacancyReferencePage> CreateANewAdvert(bool isApplicationMethodFAA) => await CreateANewAdvertOrVacancy(string.Empty, "employer", true, RAAConst.NationalMinWages, isApplicationMethodFAA, false, true, true);

    internal async Task<VacancyReferencePage> CreateANewAdvert(string employername, bool disabilityConfidence) => await CreateANewAdvertOrVacancy(employername, "employer", disabilityConfidence, RAAConst.FixedWageType, true, false, true, true);

    internal async Task<VacancyReferencePage> CreateANewAdvert(string employername, string locationType, bool disabilityConfidence, string wageType) => await CreateANewAdvertOrVacancy(employername, locationType, disabilityConfidence, wageType, true, false, true, true);

    protected override async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipAdvertOrVacancy()
    {
        var page = await GoToRecruitmentHomePage();

        var page1 = await page.CreateAnApprenticeshipAdvert();

        return await page1.GoToCreateAnApprenticeshipAdvertPage();
    }

    protected override async Task<CreateAnApprenticeshipAdvertOrVacancyPage> AdvertOrVacancySummary(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
    {
        var page = await EnterAdvertTitle(createAdvertPage);

        return await AdvertSummary(page, IsFoundationAdvert);
    }


    private async Task<EmployerVacancySearchResultPage> SearchVacancyByVacancyReference()
    {
        var page = await rAAEmployerLoginHelper.NavigateToRecruitmentHomePage();

        return await page.SearchAdvertByReferenceNumber();
    }

    protected override async Task<CreateAnApprenticeshipAdvertOrVacancyPage> AboutTheEmployer(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, string employername, bool disabilityConfidence, bool isApplicationMethodFAA)
    {
        var page = await createAdvertPage.EmployerName();

        var page1 = await page.ChooseEmployerNameForEmployerJourney(employername);

        var page2 = await page1.EnterEmployerDescriptionAndGoToContactDetailsPage(disabilityConfidence, optionalFields);

        var page3 = await page2.EnterContactDetailsAndGoToApplicationProcessPage(optionalFields);

        return await page3.SelectApplicationMethod_Employer(isApplicationMethodFAA);
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


    protected override async Task<CreateAnApprenticeshipAdvertOrVacancyPage> Application(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, bool enterQuestion1, bool enterQuestion2)
    {
        var page = await createAdvertPage.EnterAdditionalQuestionsForApplicants();

        return await page.CompleteAllAdditionalQuestionsForApplicants(IsFoundationAdvert, true, true);
    }


    protected async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CompleteAboutTheEmployer(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
    {
        var page = await SkillsAndQualifications(createAdvertPage);

        return await AboutTheEmployer(page, string.Empty, true, true);
    }


    private static async Task<CreateAnApprenticeshipAdvertOrVacancyPage> FirstAdvertSummary(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
    {
        var page = await NavigateToAdvertTitle(createAdvertPage);

        var page1 = await page.EnterVacancyTitleForTheFirstAdvert();

        var page2 = await page1.SelectYes();

        return await AdvertSummary(page2, false);
    }


    protected virtual async Task<ApprenticeshipTrainingPage> EnterAdvertTitle(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
    {
        var page = await EnterAdvertTitleMultiOrg(createAdvertPage);

        return await page.SelectOrganisationMultiOrg();
    }


    protected static async Task<SelectOrganisationPage> EnterAdvertTitleMultiOrg(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage)
    {
        var page = await NavigateToAdvertTitle(createAdvertPage);

        return await page.EnterAdvertTitleMultiOrg();
    }

    internal async Task<VacancyReferencePage> TriggerValidationErrorsOnCreateAdvertPages()
    {
        var page = await CreateAnApprenticeshipAdvertOrVacancy();
        var titlePage = await NavigateToAdvertTitle(page);
        await titlePage.TriggerVacancyTitleValidationError();

        var trainingCoursepage = await titlePage.EnterAdvertTitleMultiOrg();
        await trainingCoursepage.TriggerValidationErrorOnOrganisationPage();

        var orgPage = await trainingCoursepage.SelectOrganisationMultiOrg();
        await orgPage.TriggerValidationErrorOnTrainingTitle();

        var page2 = await orgPage.EnterTrainingTitle();
        var page3 = await page2.ConfirmTrainingproviderAndContinue(false);
        await page3.TriggerValidationErrorOnProviderSelection();

        var page4 = await page3.SelectTrainingProvider();
        var page5 = await page4.ConfirmProviderAndContinueToSummaryPage();
        await page5.TriggerValidationErrorsOnShortDescription();

        var page6 = await page5.EnterShortDescription();
        await page6.TriggerValidationErrorOnWhatApprenticeWillDo();

        var page7 = await page6.EnterShortDescriptionOfWhatApprenticeWillDo();
        var page8 = await page7.EnterAllDescription();
        var page9 = await page8.ImportantDates();
        await page9.TriggerValidationErrorsOnDates();

        var page10 = await page9.EnterImportantDates();
        await page10.TriggerValidationErrorsOnDuration();

        var page11 = await page10.EnterDuration();
        await page11.TriggerValidationErrorsOnWageTypes();

        var page12 = await page11.ChooseWage_Employer(RAAConst.NationalMinWagesForApprentices);
        await page12.TriggerValidationErrorOnPayInformation();

        var page13 = await page12.SubmitExtraInformationAboutPay();
        await page13.TriggerValidationErrorOnNumberOfPositions();

        var page14 = await page13.SubmitNoOfPositionsAndNavigateToChooseLocationPage();
        await page14.TriggerValidationErrorsOnLocations();

        var page15 = await page14.ChooseAddressAndGoToCreateApprenticeshipPage("employer");
        var page16 = await page15.Skills();
        await page16.TriggerValidationOnSkillsSelection();

        var page17 = await page16.SelectSkillAndGoToQualificationsPage();
        await page17.TriggerValidationErrorsOnQualifications();

        var page18 = await page17.SelectYesToAddQualification();
        await page18.TriggerValidationOnAddQualification();

        var page19 = await page18.EnterQualifications();
        var page20 = await page19.ConfirmQualificationsAndGoToFutureProspectsPage();
        await page20.TriggerValidationErrorOnFutureProspect();

        var page21 = await page20.EnterFutureProspect();
        var page22 = await page21.EnterThingsToConsiderAndReturnToCreateAdvert(optionalFields);
        var page23 = await page22.EmployerName();
        await page23.TriggerValidationErrorOnEmployName();

        var page24 = await page23.ChooseEmployerNameForEmployerJourney(RAAConst.ExistingTradingName);
        await page24.TriggerValidationOnEmployerDescription();

        var page25 = await page24.EnterEmployerDescriptionAndGoToContactDetailsPage(false, optionalFields);
        var page26 = await page25.EnterContactDetailsAndGoToApplicationProcessPage(optionalFields);
        var page27 = await page26.SelectApplicationMethod_Employer(true);
        var page28 = await page27.EnterAdditionalQuestionsForApplicants();
        var page29 = await page28.CompleteAllAdditionalQuestionsForApplicants(IsFoundationAdvert, false, false);

        await CheckAndSubmitAdvert(page29);

        return await VerifyPageHelper.VerifyPageAsync(context, () => new VacancyReferencePage(context));
    }


    private static async Task<CreateAnApprenticeshipAdvertOrVacancyPage> AdvertSummary(ApprenticeshipTrainingPage page, bool isFoundationAdvert)
    {
        var page1 = await page.EnterTrainingTitle();

        var page2 = await page1.ConfirmTrainingproviderAndContinue(isFoundationAdvert);

        var page3 = await page2.SelectTrainingProvider();

        var page4 = await page3.ConfirmProviderAndContinueToSummaryPage();

        var page5 = await page4.EnterShortDescription();

        var page6 = await page5.EnterShortDescriptionOfWhatApprenticeWillDo();

        return await page6.EnterAllDescription();
    }


    protected virtual async Task<YourApprenticeshipAdvertsHomePage> GoToRecruitmentHomePage() => await rAAEmployerLoginHelper.GoToRecruitmentHomePage();
}