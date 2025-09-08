namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class JobsPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Jobs");

    protected override string SubmitSectionButton => ("button.govuk-button[type='submit']");

    public async Task<AddAJobPage> SelectYesAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAJobPage(context));
    }

    public async Task<FAA_ApplicationOverviewPage> SelectNoAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }
}

public class AddAJobPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a job");

    private static string JobTitle => ("#JobTitle");

    private static string EmployerName => ("#EmployerName");

    private static string JobDescription => ("#JobDescription");

    private static string StartDateMonth => ("#StartDateMonth");
    private static string StartDateYear => ("#StartDateYear");

    public async Task<JobsPage> SelectAJobAndContinue()
    {
        await page.Locator(JobTitle).FillAsync(faaDataHelper.WorkExperienceJobTitle);

        await page.Locator(EmployerName).FillAsync(faaDataHelper.WorkExperienceEmployer);

        await page.Locator(JobDescription).FillAsync(faaDataHelper.WorkExperienceMainDuties);

        await page.Locator(StartDateMonth).FillAsync(faaDataHelper.WorkExperienceStarted.Month.ToString());

        await page.Locator(StartDateYear).FillAsync(faaDataHelper.WorkExperienceStarted.Year.ToString());

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new JobsPage(context));
    }
}
