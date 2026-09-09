namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;
public class DoYouNeedToUpdateAnyData_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Do you need to update any data before starting?" })).ToBeVisibleAsync();
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();

}
