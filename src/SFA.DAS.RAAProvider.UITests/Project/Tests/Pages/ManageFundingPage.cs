
namespace SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

public class ManageFundingPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Funding for non-levy employers");
    }
}
