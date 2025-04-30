namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class LevyDeclarationsPage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h2")).ToContainTextAsync("Levy declarations");
}