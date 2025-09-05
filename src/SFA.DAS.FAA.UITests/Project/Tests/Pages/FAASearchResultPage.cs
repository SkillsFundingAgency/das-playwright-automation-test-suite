using System.Text.RegularExpressions;

namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAASearchResultPage(ScenarioContext context) : FAASignedInLandingBasePage(context)
{
    //private static By VacancyName => By.ClassName("das-search-results__link");
    //private static By FavouriteIcon => By.CssSelector("[data-add-favourite=true]");
    //private static By SavedVacancyNavBarLink => By.LinkText("Saved vacancies");
    //private static By ApplyNow => By.CssSelector(".das-button--inline-link");
    //private static By FirstApplicationDisplayed => By.CssSelector("[id^='VAC'][id$='-vacancy-title']");


    public async Task VerifySuccessfulResults()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(new Regex("results? found"));
    }

    public async Task ClickSignout()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();
    }

    //public FAA_ApplicationOverviewPage SaveFromSearchResultsAndApplyForVacancy()
    //{
    //    var savedVacancyName = pageInteractionHelper.GetText(VacancyName);

    //    formCompletionHelper.Click(FavouriteIcon);
    //    formCompletionHelper.Click(SavedVacancyNavBarLink);
    //    formCompletionHelper.Click(ApplyNow);

    //    return new FAA_ApplicationOverviewPage(context);
    //}

    //public FAA_ApprenticeSummaryPage ClickFirstApprenticeshipThatCanBeAppliedFor()
    //{
    //    var vacancyTitle = pageInteractionHelper.GetText(FirstApplicationDisplayed);

    //    objectContext.Set("vacancytitle", vacancyTitle);

    //    formCompletionHelper.Click(FirstApplicationDisplayed);

    //    return new FAA_ApprenticeSummaryPage(context);
    //}
}
