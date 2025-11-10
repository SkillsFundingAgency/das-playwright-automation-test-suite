

using SFA.DAS.QFAST.UITests.Project.Helpers;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form;

public class EditPage_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Edit Page" })).ToBeVisibleAsync();
    protected readonly QfastDataHelpers _qfastDataHelpers = context.Get<QfastDataHelpers>();

    public async Task<CreateQuestion_Page> CreateNewQuestion()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Create New Question" }).ClickAsync();
        return await VerifyPageAsync(() => new CreateQuestion_Page(context));
    }

    public async Task<EditSection_Page> GoToSection()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back to Section" }).ClickAsync();
        return await VerifyPageAsync(() => new EditSection_Page(context));
    }
}