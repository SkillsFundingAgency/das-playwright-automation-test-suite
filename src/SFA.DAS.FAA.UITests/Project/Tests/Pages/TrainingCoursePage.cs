using SFA.DAS.FAA.UITests.Project.Pages.ApplicationOverview;

namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class TrainingCoursePage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Training courses");

    protected override string SubmitSectionButton => "button.govuk-button[type='submit']";

    public async Task<AddATrainingCoursePage> SelectYesAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddATrainingCoursePage(context));
    }

    public async Task<FAA_ApplicationOverviewPage> SelectNoAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }

    public new async Task<FAA_ApplicationOverviewPage> SelectSectionCompleted()
    {
        return await base.SelectSectionCompleted();
    }
}

public class AddATrainingCoursePage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a training course");

    public async Task<TrainingCoursePage> SelectATrainingCourseAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Course name" }).FillAsync(faaDataHelper.TrainingCoursesCourseTitle);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Year" }).FillAsync($"{faaDataHelper.TrainingCoursesTo.Year}");

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingCoursePage(context));
    }
}
