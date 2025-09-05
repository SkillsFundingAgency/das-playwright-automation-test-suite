using SFA.DAS.FAA.UITests.Project.Pages.ApplicationOverview;
using SFA.DAS.FAA.UITests.Project.Tests.Pages;
using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.FAA.UITests.Project.Helpers;

public class FAAStepsHelper(ScenarioContext context) : FrameworkBaseHooks(context)
{
    public async Task<FAASignedInLandingBasePage> GoToFAAHomePage() => await GoToFAAHomePage(context.GetUser<FAAApplyUser>());
    public async Task<FAASignedInLandingBasePage> GoToFAAHomePage(FAAPortalUser user)
    {
        await Navigate(UrlConfig.FAA_AppSearch);

        if (await new CheckFAASignedOutLandingPage(context).IsPageDisplayed())
        {
            var page = await new FAASignedOutLandingpage(context).GoToSignInPage();

            var page1 = await page.SubmitValidUserDetails(user);

            await page1.Continue();
        }

        return new FAASignedInLandingBasePage(context);
    }

    public async Task<FAASignedInLandingBasePage> SubmitNewUserDetails()
    {
        await Navigate(UrlConfig.FAA_AppSearch);

        if (await new CheckFAASignedOutLandingPage(context).IsPageDisplayed())
        {
            var faaUser = context.Get<FAAUserNameDataHelper>();

            var faaApplyUser = new FAAApplyUser { Username = faaUser.FaaNewUserEmail, IdOrUserRef = faaUser.FaaNewUserPassword, MobilePhone = faaUser.FaaNewUserMobilePhone };

            var page = await new FAASignedOutLandingpage(context).GoToSignInPage();

            var page1 = await page.SubmitNewUserDetails(faaApplyUser);

            await page1.Continue();
        }

        return new FAASignedInLandingBasePage(context);
    }


    //public void VerifyApplicationStatus(bool IsSucessful)
    //{
    //    var user = context.GetUser<FAAApplyUser>();

    //    var page = GoToFAAHomePage(user).GoToApplications();

    //    if (IsSucessful) page.OpenSuccessfulApplicationPage().ViewApplication();

    //    else page.OpenUnSuccessfulApplicationPage().ViewApplication();
    //}

    //public FAA_ApplicationOverviewPage ApplyForAVacancy(string numberOfQuestions)
    //{
    //    var applicationFormPage = GoToFAAHomePageAndApply();

    //    applicationFormPage = applicationFormPage.Access_Section1_1SchoolCollegeQualifications().SelectSectionCompleted().VerifyEducationHistory_1();

    //    applicationFormPage = applicationFormPage.Access_Section1_2TrainingCourse().SelectSectionCompleted().VerifyEducationHistory_2();

    //    applicationFormPage = applicationFormPage.Access_Section2_1Jobs().SelectSectionCompleted().VerifyWorkHistory_1();

    //    applicationFormPage = applicationFormPage.Access_Section2_2VolunteeringAndWorkExperience().SelectSectionCompleted().VerifyWorkHistory_2();

    //    applicationFormPage = applicationFormPage.Access_Section3_1SkillsAndStrengths().SelectSectionCompleted().VerifyApplicationsQuestions_1();

    //    applicationFormPage = applicationFormPage.Access_Section3_2Interests().SelectSectionCompleted().VerifyApplicationsQuestions_2();

    //    switch (numberOfQuestions)
    //    {
    //        case "first":
    //            applicationFormPage = applicationFormPage.Access_Section3_3AdditionalQuestion1().SelectYesAndCompleteSection().VerifyApplicationsQuestions_3();
    //            break;

    //        case "second":
    //            applicationFormPage = applicationFormPage.Access_Section3_4AdditionalQuestion2().SelectYesAndCompleteSection().VerifyApplicationsQuestions_4();
    //            break;

    //        case "both":
    //            applicationFormPage = applicationFormPage.Access_Section3_3AdditionalQuestion1().SelectYesAndCompleteSection().VerifyApplicationsQuestions_3();
    //            applicationFormPage = applicationFormPage.Access_Section3_4AdditionalQuestion2().SelectYesAndCompleteSection().VerifyApplicationsQuestions_4();
    //            break;
    //    }

    //    applicationFormPage = applicationFormPage.Access_Section4_1Adjustment().SelectYesAndContinue().SelectSectionCompleted().VerifyInterviewAadjustments_1();

    //    applicationFormPage = applicationFormPage.Access_Section5_1DisabilityConfidence().SelectSectionCompleted().VerifyDisabilityConfidence_1();

    //    return applicationFormPage;
    //}

    //public FAA_ApplicationOverviewPage ApplyForAVacancy(string numberOfQuestions, object user)
    //{
    //    FAA_ApplicationOverviewPage applicationFormPage;

    //    switch (user)
    //    {
    //        case FAAApplyUser faaUser:
    //            applicationFormPage = GoToFAAHomePageAndApply(faaUser);
    //            break;
    //        case FAAApplySecondUser faaUser2:
    //            applicationFormPage = GoToFAAHomePageAndApply(faaUser2);
    //            break;
    //        default:
    //            throw new ArgumentException("Unsupported user type", nameof(user));
    //    }

    //    applicationFormPage = applicationFormPage.Access_Section1_1SchoolCollegeQualifications().SelectSectionCompleted().VerifyEducationHistory_1();

    //    applicationFormPage = applicationFormPage.Access_Section1_2TrainingCourse().SelectSectionCompleted().VerifyEducationHistory_2();

    //    applicationFormPage = applicationFormPage.Access_Section2_1Jobs().SelectSectionCompleted().VerifyWorkHistory_1();

    //    applicationFormPage = applicationFormPage.Access_Section2_2VolunteeringAndWorkExperience().SelectSectionCompleted().VerifyWorkHistory_2();

    //    applicationFormPage = applicationFormPage.Access_Section3_1SkillsAndStrengths().SelectSectionCompleted().VerifyApplicationsQuestions_1();

    //    applicationFormPage = applicationFormPage.Access_Section3_2Interests().SelectSectionCompleted().VerifyApplicationsQuestions_2();

    //    switch (numberOfQuestions)
    //    {
    //        case "first":
    //            applicationFormPage = applicationFormPage.Access_Section3_3AdditionalQuestion1().SelectYesAndCompleteSection().VerifyApplicationsQuestions_3();
    //            break;

    //        case "second":
    //            applicationFormPage = applicationFormPage.Access_Section3_4AdditionalQuestion2().SelectYesAndCompleteSection().VerifyApplicationsQuestions_4();
    //            break;

    //        case "both":
    //            applicationFormPage = applicationFormPage.Access_Section3_3AdditionalQuestion1().SelectYesAndCompleteSection().VerifyApplicationsQuestions_3();
    //            applicationFormPage = applicationFormPage.Access_Section3_4AdditionalQuestion2().SelectYesAndCompleteSection().VerifyApplicationsQuestions_4();
    //            break;
    //    }

    //    applicationFormPage = applicationFormPage.Access_Section4_1Adjustment().SelectYesAndContinue().SelectSectionCompleted().VerifyInterviewAadjustments_1();

    //    applicationFormPage = applicationFormPage.Access_Section5_1DisabilityConfidence().SelectSectionCompleted().VerifyDisabilityConfidence_1();

    //    return applicationFormPage;
    //}

    public async Task<FAA_ApplicationOverviewPage> ApplyForAVacancyWithNewAccount(bool qualificationdetails, bool trainingCourse, bool job, bool workExperience, bool interviewSupport, bool disabilityConfident)
    {
        var applicationFormPage = await GoToFAASearchResultsPageToSelectAVacancyAndApply();

        if (qualificationdetails)
        {
            var page = await applicationFormPage.Access_Section1_1SchoolCollegeQualifications();

            var page1 = await page.SelectYesAndContinue();

            var page2 = await page1.SelectAQualificationAndContinue();

            var page3 = await page2.AddQualificationDetailsAndContinue();

            applicationFormPage = await page3.SelectSectionCompleted();

            await applicationFormPage.VerifyEducationHistory_1();
        }
        else
        {
            var page = await applicationFormPage.Access_Section1_1SchoolCollegeQualifications();

            applicationFormPage = await page.SelectNoAndContinue();

            await applicationFormPage.VerifyEducationHistory_1();
        }

        if (trainingCourse)
        {
            var page = await applicationFormPage.Access_Section1_2TrainingCourse();

            var page1 = await page.SelectYesAndContinue();

            var page2 = await page1.SelectATrainingCourseAndContinue();

            applicationFormPage = await page2.SelectSectionCompleted();

            await applicationFormPage.VerifyEducationHistory_2();
        }
        else
        {
            var page = await applicationFormPage.Access_Section1_2TrainingCourse();

            applicationFormPage = await page.SelectNoAndContinue();

            await applicationFormPage.VerifyEducationHistory_2();
        }

        if (job)
        {
            var page = await applicationFormPage.Access_Section2_1Jobs();

            var page1 = await page.SelectYesAndContinue();

            var page2 = await page1.SelectAJobAndContinue();

            applicationFormPage = await page2.SelectSectionCompleted();

            await applicationFormPage.VerifyWorkHistory_1();
        }
        else
        {
            var page = await applicationFormPage.Access_Section2_1Jobs();

            applicationFormPage = await page.SelectNoAndContinue();

            await applicationFormPage.VerifyWorkHistory_1();
        }

        if (workExperience)
        {
            var page = await applicationFormPage.Access_Section2_2VolunteeringAndWorkExperience();

            var page1 = await page.SelectYesAndContinue();

            var page2 = await page1.SelectAVolunteeringAndWorkExperience();

            applicationFormPage = await page2.SelectSectionCompleted();

            await applicationFormPage.VerifyWorkHistory_2();
        }
        else
        {
            var page = await applicationFormPage.Access_Section2_2VolunteeringAndWorkExperience();

            applicationFormPage = await page.SelectNoAndContinue();

            await applicationFormPage.VerifyEducationHistory_2();
        }

        var page10 = await applicationFormPage.Access_Section3_1SkillsAndStrengths();

        applicationFormPage = await page10.SelectYesAndCompleteSection();

        await applicationFormPage.VerifyApplicationsQuestions_1();

        var page11 = await applicationFormPage.Access_Section3_2Interests();

        applicationFormPage = await page11.SelectYesAndCompleteSection();

        await applicationFormPage.VerifyApplicationsQuestions_2();

        var page12 = await applicationFormPage.RespondToAdditionalQuestion1();

        applicationFormPage = await page12.SelectYesAndCompleteSection();

        await applicationFormPage.VerifyApplicationsQuestions_3();

        var page13 = await applicationFormPage.RespondToAdditionalQuestion2();

        applicationFormPage = await page13.SelectYesAndCompleteSection();

        await applicationFormPage.VerifyApplicationsQuestions_4();

        if (interviewSupport)
        {
            var page20 = await applicationFormPage.Access_Section4_1Adjustment();

            var page21 = await page20.SelectYesAndContinue();

            applicationFormPage = await page21.SelectSectionCompleted();

            await applicationFormPage.VerifyInterviewAadjustments_1();
        }
        else
        {
            var page20 = await applicationFormPage.Access_Section4_1Adjustment();

            var page21 = await page20.SelectNoAndContinue();

            applicationFormPage = await page21.SelectSectionCompleted();

            await applicationFormPage.VerifyInterviewAadjustments_1();
        }

        if (disabilityConfident)
        {
            var page20 = await applicationFormPage.Access_Section5_1DisabilityConfidence();

            var page21 = await page20.SelectYesAndContinue();

            applicationFormPage = await page21.SelectSectionCompleted();

            await applicationFormPage.VerifyDisabilityConfidence_1();
        }
        else
        {
            var page20 = await applicationFormPage.Access_Section5_1DisabilityConfidence();

            var page21 = await page20.SelectNoAndContinue();

            applicationFormPage = await page21.SelectSectionCompleted();

            await applicationFormPage.VerifyDisabilityConfidence_1();
        }

        return applicationFormPage;
    }


    //public FAA_ApplicationOverviewPage GoToVacancyDetailsPageThenSaveBeforeApplying() => GoToFAAHomePage().SearchByReferenceNumber().SaveAndApplyForVacancy().Apply();
    //public FAA_ApplicationOverviewPage GoToSearchResultsPagePageAndSaveBeforeApplying() => GoToFAAHomePage().SearchAndSaveVacancyByReferenceNumber().SaveFromSearchResultsAndApplyForVacancy();
    //public FAA_SubmittedApplicationPage GoToYourApplicationsPageAndWithdrawAnApplication() => GoToFAAHomePage().GoToApplications().OpenSubmittedlApplicationPage().WithdrawSelectedApplication();
    //public FAA_SubmittedApplicationPage GoToYourApplicationsPageAndWithdrawARandomApplication() => GoToFAAHomePage().GoToApplications().OpenSubmittedlApplicationPage().WithdrawRandomlySelectedApplication();
    //public FAA_SubmittedApplicationPage GoToYourApplicationsPageAndOpenSubmittedApplicationsPage() => GoToFAAHomePage().GoToApplications().OpenSubmittedlApplicationPage();

    private async Task<FAA_ApplicationOverviewPage> GoToFAASearchResultsPageToSelectAVacancyAndApply()
    {
        var landingPage = new FAASignedInLandingBasePage(context);

        var searchResultsPage = await landingPage.SearchRandomVacancyAndGetVacancyTitle();

        var apprenticeSummaryPage = await searchResultsPage.ClickFirstApprenticeshipThatCanBeAppliedFor();

        return await apprenticeSummaryPage.Apply();
    }
    //private FAA_ApplicationOverviewPage GoToFAAHomePageAndApply() => GoToFAAHomePage().SearchByReferenceNumber().Apply();
    //private FAA_ApplicationOverviewPage GoToFAAHomePageAndApply(FAAApplyUser user) => GoToFAAHomePage(user).SearchByReferenceNumber().Apply();
    //private FAA_ApplicationOverviewPage GoToFAAHomePageAndApply(FAAApplySecondUser user) => GoToFAAHomePage(user).SearchByReferenceNumber().Apply();

    //public FAA_ApplicationOverviewPage ApplyForFirstVacancy(bool qualificationdetails, bool trainingCourse, bool job, bool workExperience, bool interviewSupport, bool disabilityConfident)
    //{
    //    var applicationFormPage = GoToFAAHomePageAndApply();

    //    if (qualificationdetails)
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section1_1SchoolCollegeQualifications()
    //            .SelectYesAndContinue()
    //            .SelectAQualificationAndContinue()
    //            .AddQualificationDetailsAndContinue()
    //            .SelectSectionCompleted()
    //            .VerifyEducationHistory_1();
    //    }
    //    else
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section1_1SchoolCollegeQualifications().SelectNoAndContinue().VerifyEducationHistory_1();
    //    }

    //    if (trainingCourse)
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section1_2TrainingCourse()
    //            .SelectYesAndContinue()
    //            .SelectATrainingCourseAndContinue()
    //            .SelectSectionCompleted()
    //            .VerifyEducationHistory_2();
    //    }
    //    else
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section1_2TrainingCourse()
    //            .SelectNoAndContinue()
    //            .VerifyEducationHistory_2();
    //    }

    //    if (job)
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section2_1Jobs()
    //            .SelectYesAndContinue()
    //            .SelectAJobAndContinue()
    //            .SelectSectionCompleted()
    //            .VerifyWorkHistory_1();
    //    }
    //    else
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section2_1Jobs()
    //            .SelectNoAndContinue()
    //            .VerifyWorkHistory_1();
    //    }

    //    if (workExperience)
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section2_2VolunteeringAndWorkExperience()
    //            .SelectYesAndContinue()
    //            .SelectAVolunteeringAndWorkExperience()
    //            .SelectSectionCompleted()
    //            .VerifyWorkHistory_2();
    //    }
    //    else
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section2_2VolunteeringAndWorkExperience()
    //            .SelectNoAndContinue()
    //            .VerifyEducationHistory_2();
    //    }

    //    applicationFormPage = applicationFormPage.Access_Section3_1SkillsAndStrengths()
    //        .SelectYesAndCompleteSection()
    //        .VerifyApplicationsQuestions_1()
    //        .Access_Section3_2Interests()
    //        .SelectYesAndCompleteSection()
    //        .VerifyApplicationsQuestions_2()
    //        .RespondToAdditionalQuestion1()
    //        .SelectYesAndCompleteSection()
    //        .VerifyApplicationsQuestions_3()
    //        .RespondToAdditionalQuestion2()
    //        .SelectYesAndCompleteSection()
    //        .VerifyApplicationsQuestions_4();

    //    if (interviewSupport)
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section4_1Adjustment()
    //            .SelectYesAndContinue()
    //            .SelectSectionCompleted()
    //            .VerifyInterviewAadjustments_1();
    //    }
    //    else
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section4_1Adjustment()
    //            .SelectNoAndContinue()
    //            .SelectSectionCompleted()
    //            .VerifyInterviewAadjustments_1();
    //    }

    //    if (disabilityConfident)
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section5_1DisabilityConfidence()
    //            .SelectYesAndContinue()
    //            .SelectSectionCompleted()
    //            .VerifyDisabilityConfidence_1();
    //    }
    //    else
    //    {
    //        applicationFormPage = applicationFormPage.Access_Section5_1DisabilityConfidence()
    //            .SelectNoAndContinue()
    //            .SelectSectionCompleted()
    //            .VerifyDisabilityConfidence_1();
    //    }

    //    return applicationFormPage;
    //}
}