namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

public class AparAssessorApplicationsHomePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("RoATP assessor applications");
    }
}
