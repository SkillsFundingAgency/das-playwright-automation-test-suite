using SFA.DAS.FAA.UITests.Project.Pages.ApplicationOverview;
using SFA.DAS.RAA.DataGenerator.Project.Helpers;

namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public abstract class FAABasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly FAADataHelper faaDataHelper = context.Get<FAADataHelper>();
    protected readonly FAAUserNameDataHelper fAAUserNameDataHelper = context.Get<FAAUserNameDataHelper>();
    protected readonly AdvertDataHelper advertDataHelper = context.GetValue<AdvertDataHelper>();
    protected readonly VacancyTitleDatahelper vacancyTitleDataHelper = context.Get<VacancyTitleDatahelper>();
    #endregion

    //protected override By ContinueButton => By.CssSelector("#main-content .govuk-button");

    protected virtual string SubmitSectionButton => ("button.govuk-button[id='submit-button']");

    public async Task<FAA_ApplicationOverviewPage> SelectSectionCompleted()
    {
        await page.GetByRole(AriaRole.Radio).First.CheckAsync();

        await page.Locator(SubmitSectionButton).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }

    //protected void GoToVacancyInFAA()
    //{
    //    var vacancyRef = objectContext.GetVacancyReference();

    //    var uri = new Uri(new Uri(UrlConfig.FAA_BaseUrl), $"apprenticeship/VAC{vacancyRef}");

    //    tabHelper.GoToUrl(uri.AbsoluteUri);
    //}

    //protected FAASearchResultPage SearchUsingVacancyTitle()
    //{
    //    var uri = new Uri(new Uri(UrlConfig.FAA_BaseUrl), $"apprenticeships?SearchTerm={vacancyTitleDataHelper.VacancyTitle}");

    //    tabHelper.GoToUrl(uri.AbsoluteUri);

    //    return new(context);
    //}
}
