using SFA.DAS.EmployerPortal.UITests.Project.Pages;
namespace SFA.DAS.EmployerPortal.UITests.Project.Tests.Pages;
public class MyTransferApplicationsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("My applications");
    }
}
