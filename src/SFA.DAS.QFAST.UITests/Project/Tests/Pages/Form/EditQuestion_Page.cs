
using SFA.DAS.QFAST.UITests.Project.Helpers;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form;

public class EditQuestion_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Edit Question" })).ToBeVisibleAsync();
    protected readonly QfastDataHelpers _qfastDataHelpers = context.Get<QfastDataHelpers>();

    public async Task<EditPage_Page> SaveQuestion()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save question", Exact = true }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Back to Page" }).ClickAsync();
        return await VerifyPageAsync(() => new EditPage_Page(context));

    }

    public async Task<EditPage_Page> GoToPage()
    {            
        await page.GetByRole(AriaRole.Link, new() { Name = "Back to Page" }).ClickAsync();
        return await VerifyPageAsync(() => new EditPage_Page(context));
    }
}
