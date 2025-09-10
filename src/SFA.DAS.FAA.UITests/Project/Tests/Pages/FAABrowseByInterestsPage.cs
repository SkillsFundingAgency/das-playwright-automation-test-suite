namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAABrowseByInterestsPage(ScenarioContext context) : FAASignedInLandingBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#SelectedRouteIds")).ToContainTextAsync("Browse by your interests");

    public async Task<FAAWhatIsYourLocationPage> SelectCategoriesCheckBoxes()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Legal, finance and accounting" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Digital" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FAAWhatIsYourLocationPage(context));
    }
}


public class FAAWhatIsYourLocationPage(ScenarioContext context) : FAASignedInLandingBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is your location?");

    public async Task<FAASearchResultPage> EnterLocationDetails(string locationOptionText)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = locationOptionText }).CheckAsync();

        if (locationOptionText == "Enter a city or postcode")
        {
            await page.GetByRole(AriaRole.Combobox, new() { Name = "City or postcode" }).FillAsync("SW1A Westminster");

            await page.GetByLabel("Within").SelectOptionAsync(["40"]);
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        return await VerifyPageAsync(() => new FAASearchResultPage(context));
    }
}