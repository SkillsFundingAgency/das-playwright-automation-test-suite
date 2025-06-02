using SFA.DAS.EmployerPortal.UITests.Project.Pages;
namespace SFA.DAS.EmployerPortal.UITests.Project.Tests.Pages;

public class ApplicationDetailsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("application details");
    }
}
