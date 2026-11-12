using SFA.DAS.RAA.DataGenerator.Project;

namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public abstract class FAABasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly FAADataHelper faaDataHelper = context.Get<FAADataHelper>();
    protected readonly FAAUserNameDataHelper fAAUserNameDataHelper = context.Get<FAAUserNameDataHelper>();
    protected readonly AdvertDataHelper advertDataHelper = context.GetValue<AdvertDataHelper>();
    protected readonly VacancyTitleDatahelper vacancyTitleDataHelper = context.Get<VacancyTitleDatahelper>();
    #endregion

    protected bool IsFoundationAdvert => context.ContainsKey("isFoundationAdvert") && (bool)context["isFoundationAdvert"];

    //protected override By ContinueButton => By.CssSelector("#main-content .govuk-button");

    protected virtual string SubmitSectionButton => ("button.govuk-button[id='submit-button']");

    public async Task CheckFoundationTag()
    {
        await Assertions.Expect(page.Locator(".govuk-tag--pink")).ToContainTextAsync("Foundation");
    }

    public async Task<FAA_ApplicationOverviewPage> SelectSectionCompleted()
    {
        await page.GetByRole(AriaRole.Radio).First.CheckAsync();

        await page.Locator(SubmitSectionButton).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }

    protected async Task GoToVacancyInFAA()
    {
        var vacancyRef = objectContext.GetVacancyReference();

        var uri = new Uri(new Uri(UrlConfig.FAA_BaseUrl), $"apprenticeship/VAC{vacancyRef}");

        await Navigate(uri.AbsoluteUri);
    }

    protected async Task<FAASearchResultPage> SearchUsingVacancyTitle()
    {
        var uri = new Uri(new Uri(UrlConfig.FAA_BaseUrl), $"apprenticeships?SearchTerm={vacancyTitleDataHelper.VacancyTitle}");

        await Navigate(uri.AbsoluteUri);

        return await VerifyPageAsync(() => new FAASearchResultPage(context));
    }
}
