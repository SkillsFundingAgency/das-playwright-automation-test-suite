using SFA.DAS.QFAST.UITests.Project.Helpers;
namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application;
public class AO_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("a.govuk-button:has-text(\"Start new application\")")).ToBeVisibleAsync();
    protected readonly QfastDataHelpers qfastDataHelpers = context.Get<QfastDataHelpers>();
    protected readonly Application_Overview_Page applicationOverview_Page = new(context);
    protected readonly TestPage_Page testPage_Page = new(context);
    protected readonly TestSection_Page testSection_Page = new(context);
    public async Task SubmitApplication()
    {
        await ClickStartNewApplicationButton();
        await StartApplication(qfastDataHelpers.FormName);
        await VerifyErrorMessageForEmptyApplicationDetails();
        await EnterApplicationDetailsAndSubmit();
        await applicationOverview_Page.DeletButtonIsVisible();
        await applicationOverview_Page.ClickTestSection();
        await testSection_Page.ClickTestPage();
        await testPage_Page.SubmitTheAnswer();
        await testSection_Page.ClickBackToViewApplication();
        await applicationOverview_Page.VerifyApplicationIsCompleted();
        // at this stage AO user can still delete the application
        await applicationOverview_Page.DeletButtonIsVisible();
        await applicationOverview_Page.ClickSubmitApplication();
        await applicationOverview_Page.ClickAcceptAndSubmit();
        await applicationOverview_Page.ClickBackToDashboard();
    }
    public async Task<AvailableForms_Page> ClickStartNewApplicationButton()
    {
        await page.Locator("a.govuk-button:has-text(\"Start new application\")").ClickAsync();
        return await VerifyPageAsync(() => new AvailableForms_Page(context));
    }
    public async Task<StartApplication_Page> StartApplication(string formTitle)
    {
        var headings = page.Locator("h2", new() { HasTextString = formTitle });
        if (await headings.CountAsync() == 0)
            throw new InvalidOperationException($"Form '{formTitle}' not found.");
        var heading = page.Locator("h2", new() { HasTextString = formTitle }).First;
        var startButton = heading.Locator("xpath=following-sibling::a[.//text()[normalize-space()='Start application']][1]");
        await startButton.ClickAsync();
        return await VerifyPageAsync(() => new StartApplication_Page(context));
    }
    public async Task<StartApplication_Page> VerifyErrorMessageForEmptyApplicationDetails()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        var formError = page.Locator("a[href='#Name']");
        var descriptionError = page.Locator("a[href='#Owner']");
        await Assertions.Expect(formError).ToContainTextAsync("The Name field is required.");
        await Assertions.Expect(descriptionError).ToContainTextAsync("The Owner field is required.");
        return await VerifyPageAsync(() => new StartApplication_Page(context));
    }
    public async Task<Application_Overview_Page> EnterApplicationDetailsAndSubmit()
    {
        await page.GetByLabel("Qualification title").FillAsync(qfastDataHelpers.QualificationTitle);
        await page.GetByLabel("Application owner").FillAsync(qfastDataHelpers.ApplicationOwner);
       // await page.GetByLabel("Qualification number (optional)").FillAsync(qfastDataHelpers.QualificationNumber);
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        return await VerifyPageAsync(() => new Application_Overview_Page(context));
    }
}