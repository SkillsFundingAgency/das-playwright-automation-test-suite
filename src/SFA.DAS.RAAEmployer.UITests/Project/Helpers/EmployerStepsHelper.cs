namespace SFA.DAS.RAAEmployer.UITests.Project.Helpers;

public class EmployerStepsHelper(ScenarioContext context)
{
    private readonly RAAEmployerLoginStepsHelper _rAAEmployerLoginHelper = new(context);

    internal async Task<EmployerVacancySearchResultPage> YourAdvert()
    {
        await _rAAEmployerLoginHelper.GotoEmployerHomePage();

        var page = await _rAAEmployerLoginHelper.NavigateToRecruitmentHomePage();

        return await page.SearchAdvertByReferenceNumber();
    }

    internal async Task EditVacancyDates()
    {
        var page = await SearchVacancyByVacancyReferenceInNewTab();

        var page1 = await page.GoToVacancyManagePage();

        var page2 = await page1.EditAdvert();

        await page2.EnterVacancyDates();
    }

    internal async Task CloseVacancy()
    {
        var page = await SearchVacancyByVacancyReferenceInNewTab();

        var page1 = await page.GoToVacancyManagePage();

        var page2 = await page1.CloseAdvert();

        await page2.YesCloseThisVacancy();
    }

    internal async Task ApplicantUnsucessful() => await StepsHelper.ApplicantUnsucessful(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantInterviewing() => await StepsHelper.ApplicantMarkForInterview(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantReview() => await StepsHelper.ApplicantInReview(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantSucessful() => await StepsHelper.ApplicantSucessful(await SearchVacancyByVacancyReferenceInNewTab());
    internal async Task ApplicantWithdrawn() => await StepsHelper.ApplicantWithdrawn(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task VerifyWageType(string wageType) => await StepsHelper.VerifyWageType(await SearchVacancyByVacancyReference(), wageType);

    private async Task<EmployerVacancySearchResultPage> SearchVacancyByVacancyReferenceInNewTab()
    {
        await _rAAEmployerLoginHelper.GotoEmployerHomePage();

        return await SearchVacancyByVacancyReference();
    }

    private async Task<EmployerVacancySearchResultPage> SearchVacancyByVacancyReference()
    {
        var page = await _rAAEmployerLoginHelper.NavigateToRecruitmentHomePage();

        return await page.SearchAdvertByReferenceNumber();
    }
}
