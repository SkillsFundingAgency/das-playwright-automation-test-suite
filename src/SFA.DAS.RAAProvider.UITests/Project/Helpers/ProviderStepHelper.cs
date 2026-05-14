
using SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAProvider.UITests.Project.Helpers;

[Binding]
public class ProviderStepHelper(ScenarioContext context)
{
    private readonly RecruitmentProviderHomePageStepsHelper _stepsHelper = new(context);

    internal async Task ApplicantSucessful() => await StepsHelper.ProviderApplicantSucessful(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantUnsucessful() => await StepsHelper.ProviderApplicantUnsucessful(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantInterviewing() => await StepsHelper.ProviderApplicantMarkForInterview(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantInReview() => await StepsHelper.ProviderApplicantInReview(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantWithdrawn() => await StepsHelper.ProviderApplicantWithdrawn(await SearchVacancyByVacancyReference());

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
