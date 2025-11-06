namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

    public class SearchforATrainingProviderPage(ScenarioContext context) :  BasePage(context)
    {
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search for a training provider");
    }

    public async Task<SearchforATrainingProviderPage> ClickSearchForATrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Search for a training provider" }).ClickAsync();
        return await VerifyPageAsync(() => new SearchforATrainingProviderPage(context));
    }
    public async Task<ManageTrainingProviderInformationPage> GoBackToManageTrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back" }).ClickAsync();
        return await VerifyPageAsync(() => new ManageTrainingProviderInformationPage(context));
    }
    public async Task<ProviderDetailsPage> EnterProviderDetailAndSearch(string ukprn, string providerdetails)
    {
        await page.Locator("#SearchTerm").ClickAsync();
        await page.Locator("#SearchTerm").FillAsync(ukprn);
        await SelectAutocompleteOption(providerdetails);
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDetailsPage(context));
    }
    public async Task SelectAutocompleteOption(string optionText)
    {
        var autocompleteOption = page.Locator($"text={optionText}");
        await autocompleteOption.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
        await autocompleteOption.ClickAsync();
    }
}