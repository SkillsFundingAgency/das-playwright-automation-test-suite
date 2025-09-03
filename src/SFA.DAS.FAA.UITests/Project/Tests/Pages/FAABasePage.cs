using SFA.DAS.Framework;
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

    //protected virtual By SubmitSectionButton => By.CssSelector("button.govuk-button[id='submit-button']");

    //public FAA_ApplicationOverviewPage SelectSectionCompleted()
    //{
    //    SelectRadioOptionByForAttribute("IsSectionCompleted");

    //    formCompletionHelper.Click(SubmitSectionButton);

    //    return new(context);
    //}

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
