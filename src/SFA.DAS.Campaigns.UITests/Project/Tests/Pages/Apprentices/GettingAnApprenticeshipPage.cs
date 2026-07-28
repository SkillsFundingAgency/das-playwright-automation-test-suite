namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class GettingAnApprenticeshipPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Getting an apprenticeship");

    public async Task VerifySubHeadings() => await VerifyLinks();
}