using SFA.DAS.FATe.UITests.Project.Tests.Pages;

namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class SearchForTrainingProviderPage(ScenarioContext context) : FATeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).
        ToContainTextAsync("Search for a training provider");

    public async Task<SearchForTrainingProviderPage> SearchWithAUkprn()
    {
        await page.Locator("#SearchTerm").ClickAsync();
        await page.Locator("#SearchTerm").FillAsync(fateDataHelper.UKPRN);
        await SelectAutocompleteOption(fateDataHelper.ProviderDetails);
        await ClickContinue();
        return await VerifyPageAsync(() => new SearchForTrainingProviderPage(context));
    }
    public async Task<SearchForTrainingProviderPage> SearchWithoutAUKPRN()
    {
        await ClickContinue();
        var errorSummary = page.Locator(".govuk-error-summary__body");
        await Assertions.Expect(errorSummary).ToBeVisibleAsync();
        var errorMessage = page.Locator("text=Type a name or UKPRN and select a provider");
        return await VerifyPageAsync(() => new SearchForTrainingProviderPage(context));
    }
    public async Task<SearchForTrainingProviderPage> SearchWithAnInvalidUkprn()
    {
        await page.Locator("#SearchTerm").ClickAsync();
        await page.Locator("#SearchTerm").FillAsync(fateDataHelper.InvalidUKPRN);
        var assistiveHint = page.Locator("#SearchTerm__assistiveHint");
        var noResultsMessage = page.Locator("text='No results found'");
        return await VerifyPageAsync(() => new SearchForTrainingProviderPage(context));
    }
}
