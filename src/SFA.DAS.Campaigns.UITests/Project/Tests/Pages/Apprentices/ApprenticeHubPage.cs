using Microsoft.Playwright;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class ApprenticeHubPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    protected ILocator SetUpService => page.GetByRole(AriaRole.Link, new() { Name = "Getting an apprenticeship", Exact = false });

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Become an apprentice");

    public async Task VerifySubHeadings() => await VerifyLinks();
}