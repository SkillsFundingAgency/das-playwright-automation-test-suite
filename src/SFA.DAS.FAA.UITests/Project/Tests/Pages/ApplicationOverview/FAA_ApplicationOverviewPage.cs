namespace SFA.DAS.FAA.UITests.Project.Pages.ApplicationOverview;

public partial class FAA_ApplicationOverviewPage : FAABasePage
{

    private static string AdditionalQuestion1Link => ("#additionalquestion1");
    private static string AdditionalQuestion2Link => ("#additionalquestion2");
    private static string AdditionalQuestionTextBox => ("#AdditionalQuestionAnswer");

    #region Section-1 Education history

    public async Task<SchoolCollegeAndUniversityQualificationsPage> Access_Section1_1SchoolCollegeQualifications()
    {
        await NavigateToTask(EducationHistoryFirstQuestion, EducationHistory_1);

        return await VerifyPageAsync(() => new SchoolCollegeAndUniversityQualificationsPage(context));
    }

    public async Task<TrainingCoursePage> Access_Section1_2TrainingCourse()
    {
        await NavigateToTask(EducationHistoryFirstQuestion, EducationHistory_2);

        return await VerifyPageAsync(() => new TrainingCoursePage(context));
    }

    #endregion

    #region Section-2 Work history


    public async Task<JobsPage> Access_Section2_1Jobs()
    {
        await NavigateToTask(WorkHistoryFirstQuestion, WorkHistory_1);

        return await VerifyPageAsync(() => new JobsPage(context));
    }

    public async Task<VolunteeringAndWorkExperiencePage> Access_Section2_2VolunteeringAndWorkExperience()
    {
        await NavigateToTask(WorkHistoryFirstQuestion, WorkHistory_2);

        return await VerifyPageAsync(() => new VolunteeringAndWorkExperiencePage(context));
    }

    #endregion

    #region Section-3 Applications Questions

    public async Task<WhatAreYourSkillsAndStrengthsPage> Access_Section3_1SkillsAndStrengths()
    {
        await NavigateToTask(ApplicationQuestionsFirstQuestion, ApplicationQuestions_1);

        return await VerifyPageAsync(() => new WhatAreYourSkillsAndStrengthsPage(context));
    }

    public async Task<WhatInterestsYouAboutTThisApprenticeshipPage> Access_Section3_2Interests()
    {
        await NavigateToTask(ApplicationQuestionsFirstQuestion, ApplicationQuestions_2);

        return await VerifyPageAsync(() => new WhatInterestsYouAboutTThisApprenticeshipPage(context));
    }

    public async Task<AdditionQuestion1Page> Access_Section3_3AdditionalQuestion1()
    {
        await NavigateToTask(ApplicationQuestionsFirstQuestion, advertDataHelper.AdditionalQuestion1);

        return await VerifyPageAsync(() => new AdditionQuestion1Page(context));
    }

    public async Task<AdditionQuestion2Page> Access_Section3_4AdditionalQuestion2()
    {
        await NavigateToTask(ApplicationQuestionsFirstQuestion, advertDataHelper.AdditionalQuestion2);

        return await VerifyPageAsync(() => new AdditionQuestion2Page(context));
    }

    #endregion


    #region Section-4 Interview adjustments
    public async Task<AskForSupportAtAnInterviewPage> Access_Section4_1Adjustment()
    {
        await NavigateToTask(InterviewAdjustmentsFirstQuestion, InterviewAdjustments_1);

        return await VerifyPageAsync(() => new AskForSupportAtAnInterviewPage(context));
    }

    #endregion

    #region Section-5 Disability Confidence
    public async Task<DisabilityConfidentSchemePage> Access_Section5_1DisabilityConfidence()
    {
        await NavigateToTask(DisabilityConfidenceFirstQuestion, DisabilityConfidence_1);

        return await VerifyPageAsync(() => new DisabilityConfidentSchemePage(context));
    }

    #endregion

    public async Task<CheckYourApplicationBeforeSubmittingPage> PreviewApplication()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourApplicationBeforeSubmittingPage(context));
    }

    public async Task<AdditionQuestion1Page> RespondToAdditionalQuestion1()
    {
        await page.Locator(AdditionalQuestion1Link).ClickAsync();

        return await VerifyPageAsync(() => new AdditionQuestion1Page(context));
    }

    public async Task<AdditionQuestion2Page> RespondToAdditionalQuestion2()
    {
        await page.Locator(AdditionalQuestion2Link).ClickAsync();

        return await VerifyPageAsync(() => new AdditionQuestion2Page(context));
    }

    private async Task NavigateToTask(string sectionName, string taskName)
    {
        objectContext.SetDebugInformation($"Clicking task - '{taskName}' under section '{sectionName}'");

        await page.GetByRole(AriaRole.Link, new() { Name = taskName }).ClickAsync();
    }

    public async Task VerifyEducationHistory_1() => await Verify_Section1(EducationHistory_1, "Complete");
    public async Task VerifyEducationHistory_2() => await Verify_Section1(EducationHistory_2, "Complete");

    public async Task VerifyWorkHistory_1() => await Verify_Section2(WorkHistory_1, "Complete");
    public async Task VerifyWorkHistory_2() => await Verify_Section2(WorkHistory_2, "Complete");

    public async Task VerifyApplicationsQuestions_1() => await Verify_Section3(ApplicationQuestions_1, "Complete");
    public async Task VerifyApplicationsQuestions_2() => await Verify_Section3(ApplicationQuestions_2, "Complete");
    public async Task VerifyApplicationsQuestions_3()
    {
        var additionalQuestion1Title = new FAA_ApplicationOverviewPage(context);

        await Verify_Section3(await additionalQuestion1Title.GetAdditionalQuestion1TitleText(), "Complete");
    }

    public async Task VerifyApplicationsQuestions_4()
    {
        var additionalQuestion2Title = new FAA_ApplicationOverviewPage(context);

        await Verify_Section3(await additionalQuestion2Title.GetAdditionalQuestion2TitleText(), "Complete");
    }

    public async Task VerifyInterviewAadjustments_1() => await Verify_Section4(InterviewAdjustments_1, "Complete");
    public async Task VerifyDisabilityConfidence_1() => await Verify_Section5(DisabilityConfidence_1, "Complete");

    private async Task Verify_Section1(string taskName, string status) => await VerifySections(EducationHistoryFirstQuestion, taskName, status);

    private async Task Verify_Section2(string taskName, string status) => await VerifySections(WorkHistoryFirstQuestion, taskName, status);

    private async Task Verify_Section3(string taskName, string status) => await VerifySections(ApplicationQuestionsFirstQuestion, taskName, status);

    private async Task Verify_Section4(string taskName, string status) => await VerifySections(InterviewAdjustmentsFirstQuestion, taskName, status);

    private async Task Verify_Section5(string taskName, string status) => await VerifySections(DisabilityConfidenceFirstQuestion, taskName, status);

    private async Task VerifySections(string sectionName, string taskName, string status)
    {
        objectContext.SetDebugInformation($"verifying status is '{status}' for task - '{taskName}', section '{sectionName}' ");

        await VerifyAnySections(".govuk-task-list__item", taskName, status);
    }

    public async Task<string> GetAdditionalQuestion1TitleText()
    {
        var x = await page.Locator(AdditionalQuestion1Link).TextContentAsync();

        return x.RemoveMultipleSpace().TrimStart().TrimEnd();

    }
    public async Task<string> GetAdditionalQuestion2TitleText()
    {
        var x = await page.Locator(AdditionalQuestion2Link).TextContentAsync();

        return x.RemoveMultipleSpace().TrimStart().TrimEnd();
    }
}

