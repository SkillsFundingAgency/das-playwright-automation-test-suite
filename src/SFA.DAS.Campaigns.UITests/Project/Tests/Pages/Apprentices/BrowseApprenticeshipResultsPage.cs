namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class BrowseApprenticeshipResultsPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Results");
}
