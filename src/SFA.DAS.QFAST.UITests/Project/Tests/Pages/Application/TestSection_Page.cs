namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application
{
    public class TestSection_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Test Section" })).ToBeVisibleAsync();
        public async Task<TestPage_Page> ClickTestPage()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Test Page" }).ClickAsync();
            return await VerifyPageAsync(() => new TestPage_Page(context));
        }
        public async Task<Application_Overview_Page> ClickBackToViewApplication()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to view application" }).ClickAsync();
            return await VerifyPageAsync(() => new Application_Overview_Page(context));
        }
    }
}
