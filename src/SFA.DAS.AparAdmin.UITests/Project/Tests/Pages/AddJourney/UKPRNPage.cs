namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;

public class UKPRNPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("What is the organisation's UKPRN?");
    }
    public async Task<ManageTrainingProviderInformationPage> GoBackToManageTrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back" }).ClickAsync();
        return await VerifyPageAsync(() => new ManageTrainingProviderInformationPage(context));
    }
    public async Task<OrganisationsDetailPage> EnterProviderDetailAndSearch(string ukprn)
    {
        await page.Locator("#Ukprn").ClickAsync();
        await page.Locator("#Ukprn").FillAsync(ukprn);
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new OrganisationsDetailPage(context));
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
