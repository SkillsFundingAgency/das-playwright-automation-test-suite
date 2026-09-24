using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;
using System.Linq;

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

    internal async Task ArchiveVacancy()
    {
        var page = await SearchVacancyByVacancyReferenceInNewTab();

        var page1 = await page.GoToVacancyManagePage();

        var page2 = await page1.ArchiveAdvert();

        await page2.YesArchiveThisVacancy();
    }

    internal async Task ApplicantUnsucessful() => await StepsHelper.ApplicantUnsucessful(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantUnsucessfulAndArchive() => await StepsHelper.ApplicantUnsucessfulAndArchive(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantInterviewing() => await StepsHelper.ApplicantMarkForInterview(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task SharedApplicantInterviewing() => await StepsHelper.SharedApplicantMarkForInterview(await NavigateToSharedAppVacancy());

    internal async Task SharedApplicantNotInterviewing() => await StepsHelper.SharedApplicantNotMarkForInterview(await NavigateToSharedAppVacancy());

    internal async Task ApplicantReview() => await StepsHelper.ApplicantInReview(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantSucessful() => await StepsHelper.ApplicantSucessful(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task ApplicantSucessfulAndArchive() => await StepsHelper.ApplicantSucessfulAndArchive(await SearchVacancyByVacancyReferenceInNewTab());
    internal async Task ApplicantWithdrawn() => await StepsHelper.ApplicantWithdrawn(await SearchVacancyByVacancyReferenceInNewTab());

    internal async Task VerifyWageType(string wageType) => await StepsHelper.VerifyWageType(await SearchVacancyByVacancyReference(), wageType);

    private async Task<EmployerVacancySearchResultPage> SearchVacancyByVacancyReferenceInNewTab()
    {
        await _rAAEmployerLoginHelper.GotoEmployerHomePage();

        return await SearchVacancyByVacancyReference();
    }

    private async Task<EmployerSharedApplicationsVacanciesListPage> NavigateToSharedAppVacancy()
    {
        await _rAAEmployerLoginHelper.GotoEmployerHomePage();

        var page = await _rAAEmployerLoginHelper.NavigateToRecruitmentHomePage();

        await page.GoToYourAdvertFromSharedApplications();

        return await SearchSharedAppVacancyByVacancyReference();
    }

    private async Task<EmployerVacancySearchResultPage> SearchVacancyByVacancyReference()
    {
        YourApprenticeshipAdvertsHomePage page;

        var driver = context.Get<Driver>();
        var playwrightPage = driver.Page;
        
        try
        {
            bool isRaaEpc = context.ScenarioInfo.Tags.Contains("raa-epc");
            string text = isRaaEpc ? "Adverts with shared applications" : "Recruitment dashboard";
            await Assertions.Expect(playwrightPage.Locator("h1")).ToContainTextAsync(text, new LocatorAssertionsToContainTextOptions { Timeout = 2000 });
            page = new YourApprenticeshipAdvertsHomePage(context, false); 
        }
        catch
        {
            page = await _rAAEmployerLoginHelper.NavigateToRecruitmentHomePage();
        }

        return await page.SearchAdvertByReferenceNumber();
    }

    private async Task<EmployerSharedApplicationsVacanciesListPage> SearchSharedAppVacancyByVacancyReference()
    {
        YourApprenticeshipAdvertsHomePage page;

        var driver = context.Get<Driver>();
        var playwrightPage = driver.Page;

        try
        {
            bool isRaaEpc = context.ScenarioInfo.Tags.Contains("raa-epc");
            string text = isRaaEpc ? "Adverts with shared applications" : "Recruitment dashboard";
            await Assertions.Expect(playwrightPage.Locator("h1")).ToContainTextAsync(text, new LocatorAssertionsToContainTextOptions { Timeout = 2000 });
            page = new YourApprenticeshipAdvertsHomePage(context, false);
        }
        catch
        {
            page = await _rAAEmployerLoginHelper.NavigateToRecruitmentHomePage();
        }

        return await page.SearchSharedAppVacancyByReferenceNumber();
    }
}
