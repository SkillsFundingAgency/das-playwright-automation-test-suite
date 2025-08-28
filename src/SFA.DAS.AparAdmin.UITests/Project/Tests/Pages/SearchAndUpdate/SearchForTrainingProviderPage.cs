namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate
{
    public class SearchForTrainingProviderPage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1"))
                .ToContainTextAsync("Search for a training provider");
        }

        public async Task<ProviderDetailsPage> SearchProvider(string providerNameOrUkprn)
        {
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Start typing the provider’s name or UKPRN" })
                      .FillAsync(providerNameOrUkprn);

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" })
                      .ClickAsync();

            return await VerifyPageAsync(() => new ProviderDetailsPage(context));
        }
    }
}
