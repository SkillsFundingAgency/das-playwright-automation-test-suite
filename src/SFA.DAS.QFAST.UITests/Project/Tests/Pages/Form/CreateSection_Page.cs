using SFA.DAS.QFAST.UITests.Project.Helpers;


namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form;

public class CreateSection_Page(ScenarioContext context) : BasePage(context)
{
    protected readonly QfastDataHelpers _qfastDataHelpers = context.Get<QfastDataHelpers>();
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Create Section" })).ToBeVisibleAsync();

    public async Task <EditSection_Page>EnterSectionName()
    {
        await page.Locator("#Title").FillAsync(_qfastDataHelpers.SectionName);            
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        return await VerifyPageAsync(() => new EditSection_Page(context));
    }
}
