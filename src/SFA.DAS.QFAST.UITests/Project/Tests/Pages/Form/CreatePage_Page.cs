using SFA.DAS.QFAST.UITests.Project.Helpers;


namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form
{
    public class CreatePage_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Create Page" })).ToBeVisibleAsync();
        protected readonly QfastDataHelpers _qfastDataHelpers = context.Get<QfastDataHelpers>();

        public async Task<EditPage_Page> EnterPageDetails()
        {
            await page.Locator("#Title").FillAsync(_qfastDataHelpers.PageName);
            await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
            return await VerifyPageAsync(() => new EditPage_Page(context));
        }
    }
}
