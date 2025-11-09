using SFA.DAS.QFAST.UITests.Project.Helpers;


namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form
{
    public class EditSection_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Edit Section" })).ToBeVisibleAsync();
        protected readonly QfastDataHelpers _qfastDataHelpers = context.Get<QfastDataHelpers>();

        public async Task<CreatePage_Page> CreateNewPage()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Create New Page" }).ClickAsync();
            return await VerifyPageAsync(() => new CreatePage_Page(context));

        }

        public async Task<EditForm_Page> GoToForm()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to Form" }).ClickAsync();
            return await VerifyPageAsync(() => new EditForm_Page(context));
        }
        
    }
}
