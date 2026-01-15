using SFA.DAS.RAA.Service.Project.Pages;

namespace SFA.DAS.RAA.Service.Project.Helpers;

public class StepsHelper(ScenarioContext context)
{
    public ScenarioContext Context { get; } = context;

    //public static async Task VerifyWageType(ProviderVacancySearchResultPage providerVacancySearchResultPage, string wageType)
    //    => providerVacancySearchResultPage.NavigateToViewAdvertPage().VerifyWageType(wageType);
    //public static async Task ApplicantInReview(ProviderVacancySearchResultPage providerVacancySearchResultPage)
    //    => providerVacancySearchResultPage.NavigateToManageApplicant().MarkApplicantInReview();
    //public static async Task InterviewApplicant(ProviderVacancySearchResultPage providerVacancySearchResultPage)
    //    => providerVacancySearchResultPage.NavigateToManageApplicant().MarkApplicantInterviewWithEmployer();     
    //public static async Task ApplicantSucessful(ProviderVacancySearchResultPage providerVacancySearchResultPage)
    //    => providerVacancySearchResultPage.NavigateToManageApplicant().ProviderMakeApplicantSucessful().ConfirmSuccessful();
    //public static async Task ApplicantWithdrawn(ProviderVacancySearchResultPage providerVacancySearchResultPage)
    //    => providerVacancySearchResultPage.CheckApplicantStatus("Withdrawn");
    //public static async Task MultiShareApplicants(ProviderVacancySearchResultPage providerVacancySearchResultPage)
    //   => providerVacancySearchResultPage.NavigateToManageApplicants().ProviderShareApplicantsWithEmployer().ConfirmMultipleApplicationsSharing();
    //public static async Task ApplicantShared(ProviderVacancySearchResultPage providerVacancySearchResultPage)
    //   => providerVacancySearchResultPage.NavigateToManageApplicant().ProviderShareApplicantWithEmployer().ConfirmSharing();

    //public static async Task ApplicantUnsucessful(ProviderVacancySearchResultPage providerVacancySearchResultPage)
    //    => providerVacancySearchResultPage.NavigateToManageApplicant().ProviderMakeApplicantUnsucessful().FeedbackForUnsuccessful().ConfirmUnsuccessful();
    //public static async Task MultiApplicantsUnsucessful(ProviderVacancySearchResultPage providerVacancySearchResultPage)
    //    => providerVacancySearchResultPage.NavigateToManageAllApplicants().ProviderMakeAllSelectedApplicantsUnsucessful().FeedbackForMultipleUnsuccessful().ConfirmUnsuccessful();
    public static async Task ApplicantInReview(EmployerVacancySearchResultPage employerVacancySearchResultPage)
    {
        var page = await employerVacancySearchResultPage.NavigateToManageApplicant();
        
        await page.MarkApplicantInReview();
    }

    public static async Task ApplicantMarkForInterview(EmployerVacancySearchResultPage employerVacancySearchResultPage)
    {
        var page = await employerVacancySearchResultPage.NavigateToManageApplicant();

        await page.MarkApplicantAsInterviewing();

    }

    public static async Task ApplicantUnsucessful(EmployerVacancySearchResultPage employerVacancySearchResultPage)
    {
        var page = await employerVacancySearchResultPage.NavigateToManageApplicant();

        var page1 = await page.MakeApplicantUnsucessful();

        await page1.NotifyApplicant();
    }
    public static async Task ApplicantSucessful(EmployerVacancySearchResultPage employerVacancySearchResultPage)
    {
        var page = await employerVacancySearchResultPage.NavigateToManageApplicant();

        var page1 = await page.MakeApplicantSucessful();

        await page1.NotifyApplicant();
    }
    public static async Task ApplicantWithdrawn(EmployerVacancySearchResultPage employerVacancySearchResultPage)
    {
        await employerVacancySearchResultPage.CheckApplicantStatus("Withdrawn");
    }

    public static async Task VerifyWageType(EmployerVacancySearchResultPage employerVacancySearchResultPage, string wageType)
    {
        var page = await employerVacancySearchResultPage.NavigateToViewAdvertPage();

        await page.VerifyEmployerWageType(wageType);
    }

}
