namespace SFA.DAS.FAA.UITests.Project.Tests.Pages.ApplicationOverview;

public class ApplicationSubmittedPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Application submitted");

    public async Task ClickSignOut()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();
      
    }
}

