
namespace SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

public class ReportsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Reports");
    }
}
