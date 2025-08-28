using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate
{
    public class ManageTrainingProviderInformationPage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1"))
                .ToContainTextAsync("Manage training provider information");
        }

        public async Task<SearchForTrainingProviderPage> GoToSearchForTrainingProvider()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Search for a training provider" }).ClickAsync();
            return await VerifyPageAsync(() => new SearchForTrainingProviderPage(context));
        }

        public async Task<UKPRNPage> GoToAddNewTrainingProvider()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Add a new training provider" }).ClickAsync();
            return await VerifyPageAsync(() => new UKPRNPage(context));
        }

        public async Task<UKPRNAllowListPage> GoToAddUkprnToAllowList()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Add a UKPRN to the allow list" }).ClickAsync();
            return await VerifyPageAsync(() => new UKPRNAllowListPage(context));
        }

    }
}
