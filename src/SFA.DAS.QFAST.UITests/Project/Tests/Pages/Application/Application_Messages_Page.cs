namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application
{
    public class Application_Messages_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Application messages" })).ToBeVisibleAsync();
        public async Task SelectMessageType(string MessageType)
        {
            await page.FillAsync("#MessageText", "test");
            await page.SelectOptionAsync("#SelectedMessageType", new[] {MessageType});            
        }
        public async Task<Application_Messages_Page> ClickOnPreviewButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Preview" }).ClickAsync();
            return await VerifyPageAsync(() => new Application_Messages_Page(context));
        }
        public async Task<Application_Messages_Page> ClickOnConfirmMessageButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm message" }).ClickAsync();
            return await VerifyPageAsync(() => new Application_Messages_Page(context));
        }
        public async Task<Application_Details_Page> ClickBackLinkOnApplicationMessagePage() {
            await page.GetByRole(AriaRole.Link,new() { Name = "Back", Exact = true }).ClickAsync();
            return await VerifyPageAsync(() => new Application_Details_Page(context));
        }
    }
}
