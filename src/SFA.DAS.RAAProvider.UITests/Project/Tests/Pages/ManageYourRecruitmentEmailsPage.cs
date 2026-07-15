
namespace SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

public class ManageYourRecruitmentEmailsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage your recruitment emails");
    }
}
