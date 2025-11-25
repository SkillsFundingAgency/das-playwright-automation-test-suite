namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class ImportantDatesPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Closing and start date" : "Closing and start dates";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<DurationPage> EnterImportantDates()
    {
        await EnterDates();

        return await VerifyPageAsync(() => new DurationPage(context));
    }

    private async Task EnterDates()
    {
        var date = rAADataHelper.VacancyClosing;

        string day = date.Day.ToString();
        string month = date.Month.ToString();
        string year = date.Year.ToString();

        await page.GetByRole(AriaRole.Group, new() { Name = "Application closing date" }).GetByLabel("Day").FillAsync(day);

        await page.GetByRole(AriaRole.Group, new() { Name = "Application closing date" }).GetByLabel("Month").FillAsync(month);

        await page.GetByRole(AriaRole.Group, new() { Name = "Application closing date" }).GetByLabel("Year").FillAsync(year);

        date = rAADataHelper.VacancyStart;

        day = date.Day.ToString();
        month = date.Month.ToString();
        year = date.Year.ToString();

        await page.GetByRole(AriaRole.Group, new() { Name = "Apprenticeship start date" }).GetByLabel("Day").FillAsync(day);

        await page.GetByRole(AriaRole.Group, new() { Name = "Apprenticeship start date" }).GetByLabel("Month").FillAsync(month);

        await page.GetByRole(AriaRole.Group, new() { Name = "Apprenticeship start date" }).GetByLabel("Year").FillAsync(year);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
    }
}

public class DurationPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Duration and working hours");
    }

    public async Task<WageTypePage> EnterDuration()
    {
        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "How long is the whole" }).FillAsync(RAADataHelper.Duration);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Details of working week" }).FillAsync(rAADataHelper.WorkkingWeek);

        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "How many hours will the" }).FillAsync(RAADataHelper.WeeklyHours);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WageTypePage(context));
    }
}
