namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application
{
    public class StartApplication_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { NameRegex = new("Start application for:") })).ToBeVisibleAsync();
        public async Task<Application_Overview_Page> SelectApplicationAsAOUser(string application)
        {
            var rows = page.Locator($"tr:has-text('{application}')");
            if (await rows.CountAsync() == 0)
            {
                throw new Exception($"No applications found with the specified name: {application}");
            }           
            var firstRow = rows.First;            
            var link = firstRow.Locator("a.govuk-link").First;
            await link.ClickAsync();
            return await VerifyPageAsync(() => new Application_Overview_Page(context));
        }
        public async Task ValidateStatus(string status)
        {
            var statusValue = page.Locator("div.govuk-summary-list__row", new(){Has = page.Locator("dt.govuk-summary-list__key", new() { HasTextString = "Status" })}).Locator("dd.govuk-summary-list__value", new() { HasTextString = status });
            await Assertions.Expect(statusValue).ToBeVisibleAsync();
        }
        public async Task WithdrawTheApplication() 
        {   
            await page.GetByRole(AriaRole.Link, new() { Name = "Withdraw this application" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Application withdrawn");
        }
        public async Task<AO_Page> ClickOnManageFundingApplications()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Manage funding applications" }).ClickAsync();
            return await VerifyPageAsync(() => new AO_Page(context));
        }
    }
}
