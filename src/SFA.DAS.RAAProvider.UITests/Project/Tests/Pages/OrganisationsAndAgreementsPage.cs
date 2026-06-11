
namespace SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

public class OrganisationsAndAgreementsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("View employers and manage permissions");
    }
}
