
using SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAProvider.UITests.Project.Helpers;

[Binding]
public class ProviderStepHelper(ScenarioContext context)
{
    private readonly RecruitmentProviderHomePageStepsHelper _stepsHelper = new(context);

    internal async Task ApplicantSucessful() => await StepsHelper.ProviderApplicantSucessful(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantUnsucessful() => await StepsHelper.ProviderApplicantUnsucessful(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantSucessfulAndArchive() => await StepsHelper.ProviderApplicantSucessfulAndArchive(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantUnsucessfulAndArchive() => await StepsHelper.ProviderApplicantUnsucessfulAndArchive(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantInterviewing() => await StepsHelper.ProviderApplicantMarkForInterview(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantInReview() => await StepsHelper.ProviderApplicantInReview(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantWithdrawn() => await StepsHelper.ProviderApplicantWithdrawn(await SearchVacancyByVacancyReference());

    internal async Task ViewReferVacancy()
    {
        var page = await _stepsHelper.GoToRecruitmentProviderHomePage(true);
        await page.SearchReferAdvertTitle();
    }

    internal async Task VerifyWageType(string wageType) => await StepsHelper.ProviderVerifyWageType(await SearchVacancyByVacancyReference(), wageType);

    internal async Task ApplicantShared() => await StepsHelper.ApplicantShared(await SearchVacancyByVacancyReference());

    internal async Task ShareMutipleApplicants() => await StepsHelper.MultiShareApplicants(await SearchVacancyByVacancyReference());
    
    internal async Task MutipleApplicantsUnsucessful() => await StepsHelper.MultiApplicantsUnsucessful(await SearchVacancyByVacancyReference());

    internal async Task MutipleApplicantsUnsucessfulAndArchive() => await StepsHelper.MultiApplicantsUnsucessfulAndArchive(await SearchVacancyByVacancyReference());

    internal async Task CloseVacancy()
    {
        var page = await SearchVacancyByVacancyReferenceInNewTab();

        var page1 = await page.GoToVacancyManagePage();

        var page2 = await page1.CloseAdvert();

        await page2.YesCloseThisVacancy();
    }

    internal async Task ArchiveVacancy()
    {
        var page = await SearchVacancyByVacancyReferenceInNewTab();

        var page1 = await page.GoToVacancyManagePage();

        var page2 = await page1.ArchiveAdvert();

        await page2.YesArchiveThisVacancy();
    }

    private async Task<ProviderVacancySearchResultPage> SearchVacancyByVacancyReferenceInNewTab()
    {
        await _stepsHelper.GoToRecruitmentProviderHomePage(true);

        return await SearchVacancyByVacancyReference();
    }

    private async Task<ProviderVacancySearchResultPage> SearchVacancyByVacancyReference()
    {
        RecruitmentHomePage page;

        var driver = context.Get<Driver>();
        var playwrightPage = driver.Page;

        try
        {
            await Assertions.Expect(playwrightPage.Locator("h1")).ToContainTextAsync("Recruitment dashboard", new LocatorAssertionsToContainTextOptions { Timeout = 2000 });
            page = new RecruitmentHomePage(context);
        }
        catch
        {
            page = await _stepsHelper.GoToRecruitmentProviderHomePage(false);
        }

        return await page.SearchVacancyByVacancyReference();
    }
}
