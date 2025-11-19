namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application
{
    public class ApplicationOverview_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Application overview" })).ToBeVisibleAsync();
        public async Task DeletButtonIsVisible()
        {
            var deleteButton = page.Locator("a.govuk-button--warning:has-text(\"Delete application\")");
            await Assertions.Expect(deleteButton).ToBeVisibleAsync();
        }
        public async Task<AO_Page> ClickBackToViewAllApplications()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to view all applications" }).ClickAsync();
            return await VerifyPageAsync(() => new AO_Page(context));
        }
        public async Task<TestSection_Page> ClickTestSection()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Test Section" }).ClickAsync();
            return await VerifyPageAsync(() => new TestSection_Page(context));
        }
        public async Task VerifyApplicationIsCompleted()
        {
            var status = page.Locator("div.govuk-task-list__status");
            try
            {
                await Assertions.Expect(status).ToHaveTextAsync("Completed");
            }
            catch (Exception)
            {
                string actual = (await status.InnerTextAsync()).Trim();
                throw new Exception($"Expected status 'Completed' but found '{actual}'.");
            }
        }
        public async Task ClickSubmitApplication()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Submit application" }).ClickAsync();            
        }
        public async Task<Confirmation_Page> ClickAcceptAndSubmit()
        {            
            await page.GetByRole(AriaRole.Button, new() { Name = "Accept and submit" }).ClickAsync();  
            return await VerifyPageAsync(() => new Confirmation_Page(context));
        }
        public async Task<AO_Page> ClickBackToDashboard()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to dashboard" }).ClickAsync();            
            return await VerifyPageAsync(() => new AO_Page(context));
        }
    }
}
