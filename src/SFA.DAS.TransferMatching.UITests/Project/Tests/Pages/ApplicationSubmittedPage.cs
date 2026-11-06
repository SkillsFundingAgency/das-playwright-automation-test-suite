using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ApplicationSubmittedPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Application submitted";
    protected override ILocator PageHeader => page.Locator(".govuk-panel__title");

    public async Task<HomePage> ContinueToMyAccount() =>
        await FollowLink("Go to my account", () => new HomePage(context));
}
