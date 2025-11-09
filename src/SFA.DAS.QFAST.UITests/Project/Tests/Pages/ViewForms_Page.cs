

using SFA.DAS.QFAST.UITests.Project.Helpers;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages
{
    public class ViewForms_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("View Forms");
        protected readonly QfastDataHelpers _qfastDataHelpers = context.Get<QfastDataHelpers>();

        public async Task<CreateNewForm_Page> ClickCreateNewFormButton()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Create New Form" }).ClickAsync();
            return await VerifyPageAsync(() => new CreateNewForm_Page(context));
        }

    }
}
