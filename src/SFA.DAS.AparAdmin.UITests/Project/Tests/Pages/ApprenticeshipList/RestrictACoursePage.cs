using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class RestrictACoursesPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
       await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search for the course you want to restrict");
    }

    public async Task SelectCourse(string courseName)
    {
        await page.Locator("#SelectedLarsCode").FillAsync(courseName);
        bool valid = await page.Locator("#SelectedLarsCode__option--0").IsVisibleAsync();
        if(valid)
        {
            await page.Locator("#SelectedLarsCode__option--0").ClickAsync();

        }
        await page.Locator("#continue").ClickAsync();
    }

    public async Task ConfirmSelection()
    {
        await page.GetByRole(AriaRole.Button, new(){Name = "Confirm restriction",Exact = true}).ClickAsync();
    }

    public async Task ErrorMessage()
    {
        await Assertions.Expect(page.Locator(".govuk-error-summary__title")).ToContainTextAsync("There is a problem");
    }

    public async Task EnterDate(string fullDate)
    {
        string[] parts = fullDate.Split('-', StringSplitOptions.RemoveEmptyEntries);
        string day = parts[0];
        string month = parts[1];
        string year = parts[2];
        await page.Locator("#last-date-starts-day").FillAsync(day);
        await page.Locator("#Month").FillAsync(month);
        await page.Locator("#Year").FillAsync(year);
    }
}