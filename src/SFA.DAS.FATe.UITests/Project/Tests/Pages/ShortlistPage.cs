using System.Configuration.Provider;
using SFA.DAS.FATe.UITests.Project.Tests.Pages;

namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class ShortlistPage(ScenarioContext context) : FATeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).
        ToContainTextAsync("Shortlisted training providers");
    public async Task IsProviderInShortlistAsync(string providerName)
    {
        await page.GetByRole(AriaRole.Heading, new() { Name = providerName }).ClickAsync();
    }
    public async Task GoToTrainingProvidersPage()
    {
        await page.GoBackAsync();
    }
}