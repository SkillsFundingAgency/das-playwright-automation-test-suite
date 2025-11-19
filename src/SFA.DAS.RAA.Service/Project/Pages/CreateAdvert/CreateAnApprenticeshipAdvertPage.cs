namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public partial class CreateAnApprenticeshipAdvertOrVacancyPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Create an apprenticeship advert" : "Create an apprenticeship vacancy";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task ReturnToApplications() => await page.GetByRole(AriaRole.Link, new() { Name = "Return to your applications" }).ClickAsync();

    //public async Task ReturnToDashoard() => formCompletionHelper.ClickElement(ReturnToDashboardSelector);

    public async Task<CheckYourAnswersPage> CheckYourAnswers()
    {
        await NavigateToTask(Checkandsubmityouradvert, Checkandsubmityouradvert_1);

        return await VerifyPageAsync(() => new CheckYourAnswersPage(context));
    }

    public async Task<WhichEmployerNameDoYouWantOnYourAdvertPage> EmployerName()
    {
        await NavigateToTask(Abouttheemployer, Abouttheemployer_1);

        return await VerifyPageAsync(() => new WhichEmployerNameDoYouWantOnYourAdvertPage(context));
    }

    public async Task<DesiredSkillsPage> Skills()
    {
        await NavigateToTask(Skillsandqualifications, Skillsandqualifications_1);

        return await VerifyPageAsync(() => new DesiredSkillsPage(context));
    }

    public async Task<FutureProspectsPage> FutureProspects()
    {
        await NavigateToTask(Skillsandqualifications, FutureProspects_1);

        return await VerifyPageAsync(() => new FutureProspectsPage(context));
    }

    public async Task<ImportantDatesPage> ImportantDates()
    {
        await NavigateToTask(Employmentdetails, Employmentdetails_1);

        return await VerifyPageAsync(() => new ImportantDatesPage(context));
    }

    public async Task<WhatDoYouWantToCallThisAdvertPage> AdvertTitle()
    {
        await NavigateToAdvertTitle();

        return await VerifyPageAsync(() => new WhatDoYouWantToCallThisAdvertPage(context));
    }

    public async Task NavigateToAdvertTitle() => await NavigateToTask(AdvertOrVacancysummary, AdvertOrVacancysummary_1);

    public async Task<AdditionalQuestionsPage> EnterAdditionalQuestionsForApplicants()
    {
        await NavigateToTask(Application, Application_1);

        return await VerifyPageAsync(() => new AdditionalQuestionsPage(context));
    }
    public async Task<SelectOrganisationPage> EnterAdvertOrganisaition()
    {
        await NavigateToTask(AdvertOrVacancysummary, AdvertOrVacancysummary_2);

        return await VerifyPageAsync(() => new SelectOrganisationPage(context));
    }
    public async Task<ApprenticeshipTrainingPage> EnterAdvertTrainingCourse()
    {
        await NavigateToTask(AdvertOrVacancysummary, AdvertOrVacancysummary_3);

        return await VerifyPageAsync(() => new ApprenticeshipTrainingPage(context));
    }

    public async Task<WhatDoYouWantToCallThisAdvertPage> EnterAdvertTrainingProvider()
    {
        await NavigateToTask(AdvertOrVacancysummary, Advertsummary_4);

        return await VerifyPageAsync(() => new WhatDoYouWantToCallThisAdvertPage(context));
    }

    public async Task<WhatDoYouWantToCallThisAdvertPage> EnterAdvertSummary()
    {
        await NavigateToTask(AdvertOrVacancysummary, AdvertOrVacancysummary_5);

        return await VerifyPageAsync(() => new WhatDoYouWantToCallThisAdvertPage(context));
    }

    public async Task<WhatDoYouWantToCallThisAdvertPage> EnterAdvertAbout()
    {
        await NavigateToTask(AdvertOrVacancysummary, AdvertOrVacancysummary_6);

        return await VerifyPageAsync(() => new WhatDoYouWantToCallThisAdvertPage(context));
    }

    public async Task VerifyAdvertSummarySectionStatus(string status) => await VerifySectionStatus(AdvertOrVacancysummary, status);

    public async Task VerifyEmploymentDetailsSectionStatus(string status) => await VerifySectionStatus(Employmentdetails, status);

    public async Task VerifySkillsandqualificationsSectionStatus(string status) => await VerifySectionStatus(Skillsandqualifications, status);

    public async Task VerifyAbouttheemployerSectionStatus(string status) => await VerifySectionStatus(Abouttheemployer, status);
    public async Task VerifyApplicationSectionStatus(string status) => await VerifySectionStatus(Application, status);

    public async Task VerifyCheckandsubmityouradvertSectionStatus(string status) => await VerifySectionStatus(Checkandsubmityouradvert, status);

    public async Task VerifySectionStatus(string sectionName, string status)
    {
        objectContext.SetDebugInformation($"verifying status is '{status}' for section '{sectionName}' ");

        await VerifyAnySections(".das-task-list__section", sectionName, status);
    }

    private async Task NavigateToTask(string sectionName, string taskName)
    {
        objectContext.SetDebugInformation($"Navigating to task - '{taskName}' under section '{sectionName}' ");

        await page.GetByRole(AriaRole.Link, new() { Name = taskName }).ClickAsync();
    }
}
