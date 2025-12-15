
using System.Threading.Tasks;

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

        return await VerifyPageHelper.VerifyPageAsync(() => new FAASignedInLandingBasePage(context));
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

        return await VerifyPageHelper.VerifyPageAsync(() => new FAASignedInLandingBasePage(context));
    }


    public async Task VerifyApplicationStatus(bool IsSucessful)
    {
        var user = context.GetUser<FAAApplyUser>();

        var page = await GoToFAAHomePage(user);
        
        var page1 = await page.GoToApplications();

        if (IsSucessful) { var page2 = await page1.OpenSuccessfulApplicationPage(); await page2.ViewApplication(); }

        else { var page2 = await page1.OpenUnSuccessfulApplicationPage(); await page2.ViewApplication(); }

    }

    public async Task<FAA_ApplicationOverviewPage> ApplyForAVacancy(string numberOfQuestions)
    {
        return await ApplyForAVacancy(numberOfQuestions, context.GetUser<FAAApplyUser>(), false);
    }

    public async Task<FAA_ApplicationOverviewPage> ApplyForAVacancy(string numberOfQuestions, FAAPortalUser user, bool multipleLocations)
    {
        var applicationFormPage = await GoToFAAHomePageAndApply(user);

        var page = await applicationFormPage.Access_Section1_1SchoolCollegeQualifications();

        applicationFormPage = await page.SelectSectionCompleted();

        await applicationFormPage.VerifyEducationHistory_1();

        var page1 = await applicationFormPage.Access_Section1_2TrainingCourse();

        applicationFormPage = await page1.SelectSectionCompleted();

        await applicationFormPage.VerifyEducationHistory_2();

        var page2 = await applicationFormPage.Access_Section2_1Jobs();

        applicationFormPage = await page2.SelectSectionCompleted();

        await applicationFormPage.VerifyWorkHistory_1();

        var page3 = await applicationFormPage.Access_Section2_2VolunteeringAndWorkExperience();

        applicationFormPage = await page3.SelectSectionCompleted();

        await applicationFormPage.VerifyWorkHistory_2();

        bool IsFoundationAdvert = context.ContainsKey("isFoundationAdvert") && (bool)context["isFoundationAdvert"];

        if (IsFoundationAdvert)
        {
            var page4 = await applicationFormPage.Access_Section3_2Interests_Foundations();

            applicationFormPage = await page4.SelectSectionCompleted();

            await applicationFormPage.VerifyApplicationsQuestions_2();
        }
        else
        {
            var page4 = await applicationFormPage.Access_Section3_1SkillsAndStrengths();

            applicationFormPage = await page4.SelectSectionCompleted();

            await applicationFormPage.VerifyApplicationsQuestions_1();

            var page5 = await applicationFormPage.Access_Section3_2Interests();

            applicationFormPage = await page5.SelectSectionCompleted();

            await applicationFormPage.VerifyApplicationsQuestions_2();
        }

        bool firstQuestion, secondQuestion;

        firstQuestion = numberOfQuestions == "first" || numberOfQuestions == "both";

        secondQuestion = numberOfQuestions == "second" || numberOfQuestions == "both";

        if (firstQuestion)
        {
            if (IsFoundationAdvert)
            {
                var page5 = await applicationFormPage.Access_Section3_3AdditionalQuestion1_Foundations();

                applicationFormPage = await page5.SelectYesAndCompleteSection();

                await applicationFormPage.VerifyApplicationsQuestions_3();
            }
            else
            {
                var page5 = await applicationFormPage.Access_Section3_3AdditionalQuestion1();

                applicationFormPage = await page5.SelectYesAndCompleteSection();

                await applicationFormPage.VerifyApplicationsQuestions_3();
            }
        }

        if (secondQuestion)
        {
            if (IsFoundationAdvert)
            {

                var page4 = await applicationFormPage.Access_Section3_4AdditionalQuestion2_Foundations();

                applicationFormPage = await page4.SelectYesAndCompleteSection();

                await applicationFormPage.VerifyApplicationsQuestions_4();
            }
            else
            {
                var page4 = await applicationFormPage.Access_Section3_4AdditionalQuestion2();

                applicationFormPage = await page4.SelectYesAndCompleteSection();

                await applicationFormPage.VerifyApplicationsQuestions_4();
            }
        }

        var page6 = await applicationFormPage.Access_Section4_1Adjustment();

        var page7 = await page6.SelectYesAndContinue();

        applicationFormPage = await page7.SelectSectionCompleted();

        await applicationFormPage.VerifyInterviewAadjustments_1();

        var page8 = await applicationFormPage.Access_Section5_1DisabilityConfidence();

        applicationFormPage = await page8.SelectSectionCompleted();

        await applicationFormPage.VerifyDisabilityConfidence_1();

        bool multipleLocationsFlag = context.ContainsKey("multipleLocations") && (bool)context["multipleLocations"];

        if (multipleLocations || multipleLocationsFlag)
        {
            var page9 = await applicationFormPage.Access_Section6_1Locations();

            var page10 = await page9.SelectLocationsAndContinue();

            applicationFormPage = await page10.SelectSectionCompleted();

            await applicationFormPage.VerifyLocations_1();
        }

        return applicationFormPage;
    }

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

    public async Task<FAA_ApplicationOverviewPage> GoToVacancyDetailsPageThenSaveBeforeApplying()
    {
        var page = await GoToFAAHomePage();

        var page1 = await page.SearchByReferenceNumber();

        var page2 = await page1.SaveAndApplyForVacancy();
        
        return await page2.Apply();
    }

    public async Task<FAA_ApplicationOverviewPage> GoToSearchResultsPagePageAndSaveBeforeApplying()
    {
        var page = await GoToFAAHomePage();

        var page1 = await page.SearchAndSaveVacancyByReferenceNumber();

        return await page1.SaveFromSearchResultsAndApplyForVacancy();

    }

    public async Task<FAA_SubmittedApplicationPage> GoToYourApplicationsPageAndWithdrawAnApplication()
    {
        var page = await GoToYourApplicationsPageAndOpenSubmittedApplicationsPage();

        return await page.WithdrawSelectedApplication();
    }

    public async Task<FAA_SubmittedApplicationPage> GoToYourApplicationsPageAndWithdrawARandomApplication()
    {
        var page = await GoToYourApplicationsPageAndOpenSubmittedApplicationsPage();

        return await page.WithdrawRandomlySelectedApplication();
    }

    public async Task<FAA_SubmittedApplicationPage> GoToYourApplicationsPageAndOpenSubmittedApplicationsPage()
    {
        var page = await GoToFAAHomePage();

        var page1 = await page.GoToApplications();

        return await page1.OpenSubmittedlApplicationPage();
    }

    private async Task<FAA_ApplicationOverviewPage> GoToFAASearchResultsPageToSelectAVacancyAndApply()
    {
        var landingPage = new FAASignedInLandingBasePage(context);

        var searchResultsPage = await landingPage.SearchRandomVacancyAndGetVacancyTitle();

        var apprenticeSummaryPage = await searchResultsPage.ClickFirstApprenticeshipThatCanBeAppliedFor();

        return await apprenticeSummaryPage.Apply();
    }

    private async Task<FAA_ApplicationOverviewPage> GoToFAAHomePageAndApply() => await GoToFAAHomePageAndApply(context.GetUser<FAAApplyUser>());
    
    private async Task<FAA_ApplicationOverviewPage> GoToFAAHomePageAndApply(FAAPortalUser user)
    {
        var page = await GoToFAAHomePage(user);

        var page1 = await page.SearchByReferenceNumber();

        return await page1.Apply();
    }

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