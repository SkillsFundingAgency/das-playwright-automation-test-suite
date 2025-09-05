using SFA.DAS.FAA.UITests.Project.Pages.ApplicationOverview;

namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAA_ApprenticeSummaryPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#vacancy-title")).ToContainTextAsync(objectContext.Get("vacancyTitle"));

    //private static By ViewSubmittedApplicationLink => By.CssSelector("a[href*='Submitted']");

    //private static By SaveVacancyLink => By.XPath("//span[normalize-space()='Save vacancy']");

    //private static By SavedVacanciesNavBar => By.XPath("//a[normalize-space()='Saved vacancies']");
    //private static By SavedVacancyLink => By.CssSelector(".govuk-link.govuk-link--no-visited-state");
    //private static By VacancyName => By.CssSelector(".govuk-heading-l faa-vacancy__title");


    public async Task<FAA_ApplicationOverviewPage> Apply()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Apply for apprenticeship" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }

    //public FAA_SubmittedApplicationPage ViewSubmittedApplications()
    //{
    //    formCompletionHelper.Click(ViewSubmittedApplicationLink);
    //    return new FAA_SubmittedApplicationPage(context);
    //}

    //public FAA_ApprenticeSummaryPage ConfirmDraftVacancyDeletion()
    //{
    //    pageInteractionHelper.VerifyText(ApplyButton, "Continue your application");
    //    return this;
    //}

    //public FAA_ApprenticeSummaryPage SaveAndApplyForVacancy()
    //{
    //    formCompletionHelper.Click(SaveVacancyLink);
    //    formCompletionHelper.Click(SavedVacanciesNavBar);
    //    formCompletionHelper.ClickLinkByText(SavedVacancyLink, vacancyTitleDataHelper.VacancyTitle);
    //    return new FAA_ApprenticeSummaryPage(context);
    //}
}
