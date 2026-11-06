namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class VolunteeringAndWorkExperiencePage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Volunteering and work experience");

    protected override string SubmitSectionButton => "button.govuk-button[type='submit']";

    public async Task CheckValidationErrorOnVolunteering()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        await CheckValidationErrors("Select if you want to add any volunteering or work experience");
    }
    public async Task<AddVolunteeringOrWorkExperiencePage> SelectYesAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddVolunteeringOrWorkExperiencePage(context));
    }

    public async Task<FAA_ApplicationOverviewPage> SelectNoAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }
}

public class AddVolunteeringOrWorkExperiencePage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add volunteering or work experience");

    private static string CompanyName => ("#CompanyName");

    private static string Description => ("#Description");

    private static string StartDateMonth => ("#StartDateMonth");

    private static string StartDateYear => ("#StartDateYear");

    public async Task CheckValidationErrorsOnAddWorkExperience()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        await CheckValidationErrors("Enter the company or organisation for this volunteering or work experience",
            "Enter what you did for this volunteering or work experience",
            "Enter a real date for the start date",
            "Select if you’re still doing this volunteering or work experience");
    }

    public async Task CheckValidationErrorOnExperienceWordCount()
    {
        await page.Locator(CompanyName).FillAsync(faaDataHelper.WorkExperienceEmployer);

        await page.Locator(Description).FillAsync(RandomDataGenerator.GenerateRandomWords(102));

        await page.Locator(StartDateMonth).FillAsync(faaDataHelper.WorkExperienceStarted.Month.ToString());

        await page.Locator(StartDateYear).FillAsync(faaDataHelper.WorkExperienceStarted.Year.ToString());

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        await CheckValidationErrors("What you did must be 100 words or less");
    }
    public async Task<VolunteeringAndWorkExperiencePage> SelectAVolunteeringAndWorkExperience()
    {
        await page.Locator(CompanyName).FillAsync(faaDataHelper.WorkExperienceEmployer);

        await page.Locator(Description).FillAsync(faaDataHelper.WorkExperienceMainDuties);

        await page.Locator(StartDateMonth).FillAsync(faaDataHelper.WorkExperienceStarted.Month.ToString());

        await page.Locator(StartDateYear).FillAsync(faaDataHelper.WorkExperienceStarted.Year.ToString());

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new VolunteeringAndWorkExperiencePage(context));
    }
}