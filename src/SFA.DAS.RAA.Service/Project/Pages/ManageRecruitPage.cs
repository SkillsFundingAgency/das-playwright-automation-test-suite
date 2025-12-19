using SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

namespace SFA.DAS.RAA.Service.Project.Pages;

public class ManageRecruitPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Manage Advert" : "Manage vacancy";

        await Assertions.Expect(page.Locator("#vacancy-header")).ToContainTextAsync(PageTitle);
    }

    //protected static By CloseAdvertActionSelector => By.CssSelector("a[href*='/close']");

    public async Task<CloneVacancyDatesPage> CloneAdvert()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Clone advert" }).ClickAsync();

        return await VerifyPageAsync(() => new CloneVacancyDatesPage(context));
    }

    public async Task<CloneVacancyDatesPage> CloneVacancy()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Clone advert" }).ClickAsync();

        return await VerifyPageAsync(() => new CloneVacancyDatesPage(context));
    }

    public async Task<EditVacancyDatesPage> EditAdvert()
    {
        await page.Locator("dl div").Filter(new() { HasText = "Closing date Possible start" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new EditVacancyDatesPage(context));
    }

    public async Task<CloseVacancyPage> CloseAdvert()
    {
        await page.Locator("a[href*='/close']").ClickAsync();

        return await VerifyPageAsync(() => new CloseVacancyPage(context));
    }
}

public class CloseVacancyPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Are you sure you want to close this advert on Find an apprenticeship?" : "Are you sure you want to close this vacancy on Find an apprenticeship?";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ManageCloseVacancyPage> YesCloseThisVacancy()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, close this advert now" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageCloseVacancyPage(context));
    }
}

public class ManageCloseVacancyPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? $"Advert VAC{objectContext.GetVacancyReference()} - '{rAADataHelper.VacancyTitle}' has been closed."
            : $"Vacancy VAC{objectContext.GetVacancyReference()} - '{rAADataHelper.VacancyTitle}' has been closed.";

        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync(PageTitle);
    }
}


public abstract class VacancyDatesBasePage(ScenarioContext context) : RaaBasePage(context)
{
    protected static string ClosingDay => "#ClosingDay";
    protected static string ClosingMonth => "#ClosingMonth";
    protected static string ClosingYear => "#ClosingYear";
    protected static string StartDateDay => "#StartDay";
    protected static string StartDateMonth => "#StartMonth";
    protected static string StartDateYear => "#StartYear";

    protected async Task ClosingDate(DateTime date) => await EditDates(date, ClosingDay, ClosingMonth, ClosingYear);

    protected async Task StartDate(DateTime date) => await EditDates(date, StartDateDay, StartDateMonth, StartDateYear);

    private async Task EditDates(DateTime date, string dayselector, string monthselector, string yearselector)
    {
        string day = date.Day.ToString();
        string month = date.Month.ToString();
        string year = date.Year.ToString();

        await page.Locator(dayselector).FillAsync(day);
        await page.Locator(monthselector).FillAsync(month);
        await page.Locator(yearselector).FillAsync(year);

    }
}

public class EditVacancyDatesPage(ScenarioContext context) : VacancyDatesBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Edit advert dates" : "Edit vacancy dates";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<EmployerVacancySearchResultPage> EnterVacancyDates()
    {
        await ClosingDate(rAADataHelper.EditedVacancyClosing);

        await StartDate(rAADataHelper.EditedVacancyStart);

        await page.GetByRole(AriaRole.Button, new() { Name = "Update advert" }).ClickAsync();

        await Assertions.Expect(page.GetByLabel("Success").Locator("h3")).ToContainTextAsync("have been updated.");

        return await VerifyPageAsync(() => new EmployerVacancySearchResultPage(context));
    }

    //public ProviderVacancySearchResultPage EnterProviderVacancyDates()
    //{
    //    ClosingDate(rAADataHelper.EditedVacancyClosing);
    //    StartDate(rAADataHelper.EditedVacancyStart);
    //    Continue();
    //    return new ProviderVacancySearchResultPage(context);
    //}
}


public class CloneVacancyDatesPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Does the new advert have the same closing date and start date?" : "Does the new vacancy have the same closing date and start date?";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ConfimCloneVacancyDatePage> SelectYes()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        await StoreMultipleLocationsFlag();

        return await VerifyPageAsync(() => new ConfimCloneVacancyDatePage(context));
    }

    public async Task StoreMultipleLocationsFlag()
    {
        var dtElements = await page.Locator("dt.app-summary-list__key").AllTextContentsAsync();

        bool multipleLocations = false;
        foreach (var dt in dtElements)
        {
            if (dt.Trim() == "Locations")
            {
                multipleLocations = true;
                break;
            }
        }
        context["multipleLocations"] = multipleLocations;
    }
}

public class ConfimCloneVacancyDatePage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Advert succesfully cloned" : "Vacancy succesfully cloned";

        await Assertions.Expect(page.GetByLabel("Success")).ToContainTextAsync(PageTitle);

    }

    public async Task<WhatDoYouWantToCallThisAdvertPage> UpdateTitle()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   advert title" }).ClickAsync();

        return await VerifyPageAsync(() => new WhatDoYouWantToCallThisAdvertPage(context));
    }
}
