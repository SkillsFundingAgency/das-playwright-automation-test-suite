namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application
{
    public class Application_Details_Page (ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Application details" })).ToBeVisibleAsync();
        public async Task<Application_Details_Page> SelectApplicationAsQfauOfqualAndIfateUser(string application)
        {
            var rows = page.Locator($"tr:has-text('{application}')");
            if (await rows.CountAsync() == 0)
            {
                throw new Exception($"No applications found with the specified name: {application}");
            }
            var firstRow = rows.First;
            var link = firstRow.Locator("a.govuk-link").First;
            await link.ClickAsync();
            return await VerifyPageAsync(() => new Application_Details_Page(context));
        }
        public async Task<Application_Details_Page> ShareApplicaitonWithOfqualUser()
        {
            await page.GetByRole(AriaRole.Link, new(){ Name = "Share with Skills England" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new(){ Name = "Confirm" }).ClickAsync();
            return await VerifyPageAsync(() => new Application_Details_Page(context));
        }
        public async Task<Application_Details_Page> ShareApplicaitonWithIfatelUser()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Share with Ofqual" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
            return await VerifyPageAsync(() => new Application_Details_Page(context));
        }
        public async Task<Application_Messages_Page> ClickOnViewMessagesLink()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "View messages" }).ClickAsync();
            return await VerifyPageAsync(() => new Application_Messages_Page(context));
        }
        public async Task<RequestForFundign_Page> ClickBackLinkOnApplicationDetailsPage()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();
            return await VerifyPageHelper.VerifyPageAsync(() => new RequestForFundign_Page(context));
        }
    }
}
