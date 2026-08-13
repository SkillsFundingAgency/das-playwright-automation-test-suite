namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;
public class RollOverSummary_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Rollover summary" })).ToBeVisibleAsync();
    public async Task ClickSubmitRollOverButton() => await page.Locator("button:has-text('Submit rollover')").ClickAsync();
    public async Task ValidateRollOverSuccessBanner()
    {
        var bannerLocator = page.GetByRole(AriaRole.Heading,new() { Name = "Rollover submitted" });
        try
        {
            await bannerLocator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 120000
            });
        }
        catch (TimeoutException)
        {
            throw new Exception(
                "Expected rollover success banner 'Rollover submitted' was not found");
        }
    }
}