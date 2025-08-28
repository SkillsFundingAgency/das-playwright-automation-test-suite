namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

public class SuccessPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Success");
    }
}
