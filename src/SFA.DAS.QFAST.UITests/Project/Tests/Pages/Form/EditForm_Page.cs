using SFA.DAS.QFAST.UITests.Project.Helpers;


namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Form;

public class EditForm_Page(ScenarioContext context) : BasePage(context)
{
    protected readonly QfastDataHelpers _qfastDataHelpers = context.Get<QfastDataHelpers>();
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Edit Form");

    public async Task<CreateSection_Page> ValidateStatusAsDraftAndClickOnCreateNewSection()
    {
        var statusLocator = page.Locator("#Status");
        await Assertions.Expect(statusLocator).ToHaveValueAsync(_qfastDataHelpers.DraftStatus);
        await page.GetByRole(AriaRole.Link, new() { Name = "Create new section" }).ClickAsync();
        return await VerifyPageAsync(() => new CreateSection_Page(context));

    }

    public async Task<EditForm_Page> PublishForm()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Publish form" }).ClickAsync();
        var successMessage = page.Locator("div.govuk-notification-banner--success h3.govuk-notification-banner__heading");
        await Assertions.Expect(successMessage).ToContainTextAsync("The form has been published.");
        return await VerifyPageAsync(() => new EditForm_Page(context));
    }

    public async Task<ViewForms_Page> GoToForms()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back to Forms" }).ClickAsync();
        return await VerifyPageAsync(() => new ViewForms_Page(context));
    }
}