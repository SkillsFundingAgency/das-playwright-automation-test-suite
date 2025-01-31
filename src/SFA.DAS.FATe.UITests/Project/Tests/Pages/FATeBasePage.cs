namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public abstract class FATeBasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly FATeDataHelper fateDataHelper = context.Get<FATeDataHelper>();

    public async Task ClickContinue() => await page.Locator("#continue").ClickAsync();
    public async Task<ShortlistPage> ViewShortlist()
    {
        await page.Locator("#header-view-shortlist").ClickAsync();
        return await VerifyPageAsync(() => new ShortlistPage(context));
    }
    public async Task<FATeHomePage> ReturnToStartPage()
    {
        await page.Locator(".govuk-header__link.govuk-header__service-name").ClickAsync();
        return await VerifyPageAsync(() => new FATeHomePage(context));
    }
    public async Task<Search_TrainingCourses_ApprenticeworkLocationPage> ReturnToSearch_TrainingCourses_ApprenticeworkLocationPage()
    {
        await page.Locator("#home-breadcrumb").ClickAsync();
        return await VerifyPageAsync(() => new Search_TrainingCourses_ApprenticeworkLocationPage(context));
    }
    public async Task SelectAutocompleteOption(string optionText)
    {
        var autocompleteOption = page.Locator($"text={optionText}");
        await autocompleteOption.ClickAsync();
    }
    public async Task GoBack()
    {
        var backLink = page.Locator("a.govuk-back-link");
        await backLink.ClickAsync();
    }


}
