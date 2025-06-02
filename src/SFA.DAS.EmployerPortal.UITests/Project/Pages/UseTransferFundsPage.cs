using SFA.DAS.EmployerPortal.UITests.Project.Pages;
namespace SFA.DAS.EmployerPortal.UITests.Project.Tests.Pages;

public class UseTransferFundsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Use transfer funds from");
    }
}