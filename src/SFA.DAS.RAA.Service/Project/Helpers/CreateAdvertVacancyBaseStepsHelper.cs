using SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

namespace SFA.DAS.RAA.Service.Project.Helpers
{
    public abstract class CreateAdvertVacancyBaseStepsHelper
    {
        protected static string NotStarted => "Not started";

        protected static string Completed => "Completed";

        protected static string InProgress => "In progress";

        protected static string NotRequired => "Not required";

        public bool optionalFields;

        public CreateAdvertVacancyBaseStepsHelper() => optionalFields = false;

        protected abstract Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipAdvertOrVacancy();
        protected abstract Task<CreateAnApprenticeshipAdvertOrVacancyPage> AdvertOrVacancySummary(CreateAnApprenticeshipAdvertOrVacancyPage page);

        protected abstract Task<CreateAnApprenticeshipAdvertOrVacancyPage> EmploymentDetails(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, string locationType, string wageType);

        protected abstract Task<CreateAnApprenticeshipAdvertOrVacancyPage> SkillsAndQualifications(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage);

        protected abstract Task<CreateAnApprenticeshipAdvertOrVacancyPage> AboutTheEmployer(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, string employername, bool disabilityConfidence, bool isApplicationMethodFAA);
        protected abstract Task<CreateAnApprenticeshipAdvertOrVacancyPage> Application(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage, bool enterQuestion1, bool enterQuestion2);

        protected static async Task<WhatDoYouWantToCallThisAdvertPage> NavigateToAdvertTitle(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage) => await createAdvertPage.AdvertTitle();

        protected static async Task<VacancyReferencePage> CheckAndSubmitAdvert(CreateAnApprenticeshipAdvertOrVacancyPage createAdvertPage) 
        {
            var page = await createAdvertPage.CheckYourAnswers();

            return await SubmitAndSetVacancyReference(page);
        }
        
        protected static async Task<VacancyReferencePage> SubmitAndSetVacancyReference(CheckYourAnswersPage checkYourAnswersPage) 
        {
            var page = await checkYourAnswersPage.SubmitAdvert();

            await page.SetVacancyReference();

            return page;
        }

        protected async Task<VacancyReferencePage> CreateANewAdvertOrVacancy(string employername, string locationType, bool disabilityConfidence, string wageType, bool isApplicationMethodFAA, bool isProvider, bool enterQuestion1, bool enterQuestion2)
        {
            var createAdvertPage = await CreateAnApprenticeshipAdvertOrVacancy();

            await createAdvertPage.VerifyAdvertSummarySectionStatus(isProvider ? InProgress : NotStarted);

            createAdvertPage = await AdvertOrVacancySummary(createAdvertPage);

            await createAdvertPage.VerifyAdvertSummarySectionStatus(Completed);

            await createAdvertPage.VerifyEmploymentDetailsSectionStatus(NotStarted);

            createAdvertPage = await EmploymentDetails(createAdvertPage, locationType, wageType);

            await createAdvertPage.VerifyEmploymentDetailsSectionStatus(Completed);

            await createAdvertPage.VerifySkillsandqualificationsSectionStatus(NotStarted);

            createAdvertPage = await SkillsAndQualifications(createAdvertPage);

            await createAdvertPage.VerifySkillsandqualificationsSectionStatus(Completed);

            await createAdvertPage.VerifyAbouttheemployerSectionStatus(NotStarted);

            createAdvertPage = await AboutTheEmployer(createAdvertPage, employername, disabilityConfidence, isApplicationMethodFAA);

            await createAdvertPage.VerifyAbouttheemployerSectionStatus(Completed);

            if(isApplicationMethodFAA)
            {
                await createAdvertPage.VerifyApplicationSectionStatus(NotStarted);

                createAdvertPage = await Application(createAdvertPage, enterQuestion1, enterQuestion2);

                await createAdvertPage.VerifyApplicationSectionStatus(Completed);
            }
            else
            {
                await createAdvertPage.VerifyApplicationSectionStatus(NotRequired);
            }

            await createAdvertPage.VerifyCheckandsubmityouradvertSectionStatus(InProgress);

            return await CheckAndSubmitAdvert(createAdvertPage);
        }
    }
}