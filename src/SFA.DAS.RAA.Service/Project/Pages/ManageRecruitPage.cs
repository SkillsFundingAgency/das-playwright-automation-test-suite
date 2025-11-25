using SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

namespace SFA.DAS.RAA.Service.Project.Pages;

public class ManageRecruitPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Manage Advert" : "Manage vacancy";

        await Assertions.Expect(page.Locator("#vacancy-header")).ToContainTextAsync(PageTitle);
    }

    //protected static By EditAdvertActionSelector => By.CssSelector("a[href*='/edit-dates']");
    //protected static By CloseAdvertActionSelector => By.CssSelector("a[href*='/close']");

    public async Task<CloneVacancyDatesPage> CloneAdvert()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Clone advert" }).ClickAsync();

        return await VerifyPageAsync(() => new CloneVacancyDatesPage(context));
    }

    //public CloneVacancyDatesPage CloneVacancy()
    //{
    //    formCompletionHelper.ClickLinkByText("Clone vacancy");
    //    return new CloneVacancyDatesPage(context);
    //}

    //public EditVacancyDatesPage EditAdvert()
    //{
    //    formCompletionHelper.ClickElement(EditAdvertActionSelector);
    //    return new EditVacancyDatesPage(context);
    //}

    //public CloseVacancyPage CloseAdvert()
    //{
    //    formCompletionHelper.ClickElement(CloseAdvertActionSelector);
    //    return new CloseVacancyPage(context);
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
