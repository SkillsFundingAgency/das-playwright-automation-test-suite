using SFA.DAS.QFAST.UITests.Project.Helpers;


namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form;

public class CreateQuestion_Page(ScenarioContext context) : BasePage(context)
{      
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Create Question" })).ToBeVisibleAsync();
    protected readonly QfastDataHelpers _qfastDataHelpers = context.Get<QfastDataHelpers>();

    public async Task<EditQuestion_Page> EnterFirstQuestionDetails()
    {
        await page.Locator("#Title").FillAsync(_qfastDataHelpers.FirstQuestionTitle);
        await page.GetByLabel("Short text").ClickAsync();
        await page.GetByLabel("Optional").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        return await VerifyPageAsync(() => new EditQuestion_Page(context));
    }

    public async Task<EditQuestion_Page> EnterSecondQuestionDetails()
    {
        await page.Locator("#Title").FillAsync(_qfastDataHelpers.SecondQuestionTitle);
        await page.GetByLabel("Long text").ClickAsync();
        await page.GetByLabel("Mandatory").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        return await VerifyPageAsync(() => new EditQuestion_Page(context));
    }

    public async Task<EditQuestion_Page> EnterThirdQuestionDetails()
    {
        await page.Locator("#Title").FillAsync(_qfastDataHelpers.ThirdQuestionTitle);
        await page.GetByLabel("Number").ClickAsync();
        await page.GetByLabel("Mandatory").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        return await VerifyPageAsync(() => new EditQuestion_Page(context));
    }

    public async Task<EditQuestion_Page> EnterFourthQuestionDetails()
    {
        await page.Locator("#Title").FillAsync(_qfastDataHelpers.FourthQuestionTitle);
        await page.GetByLabel("Date").ClickAsync();
        await page.GetByLabel("Optional").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        return await VerifyPageAsync(() => new EditQuestion_Page(context));
    }

    public async Task<EditQuestion_Page> EnterFifthQuestionDetails()
    {
        await page.Locator("#Title").FillAsync(_qfastDataHelpers.FifthQuestionTitle);
        await page.GetByLabel("User selects single option from a list of options").ClickAsync();
        await page.GetByLabel("Optional").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        return await VerifyPageAsync(() => new EditQuestion_Page(context));
    }

    public async Task<EditQuestion_Page> EnterSixthQuestionDetails()
    {
        await page.Locator("#Title").FillAsync(_qfastDataHelpers.SixthQuestionTitle);
        await page.GetByLabel("User selects one or more options from a list of options").ClickAsync();
        await page.GetByLabel("Mandatory").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        return await VerifyPageAsync(() => new EditQuestion_Page(context));
    }
    public async Task<EditQuestion_Page> EnterSeventhQuestionDetails()
    {
        await page.Locator("#Title").FillAsync(_qfastDataHelpers.SeventhQuestionTitle);
        await page.GetByLabel("File upload").ClickAsync();
        await page.GetByLabel("Optional").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        return await VerifyPageAsync(() => new EditQuestion_Page(context));
    }

}
