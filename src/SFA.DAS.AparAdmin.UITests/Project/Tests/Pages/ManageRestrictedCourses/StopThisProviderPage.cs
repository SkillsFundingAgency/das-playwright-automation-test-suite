using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;

public class StopThisProviderPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-caption-xl")).ToContainTextAsync("last start date");
    }

    public async Task EnterDate(string fullDate)
    {
        string[] parts = fullDate.Split('-', StringSplitOptions.RemoveEmptyEntries);
        string day = parts[0];
        string month = parts[1];
        string year = parts[2];
        await page.Locator("#Day").FillAsync(day);
        await page.Locator("#Month").FillAsync(month);
        await page.Locator("#Year").FillAsync(year);
        await page.Locator("[type='submit']").ClickAsync();
    }

    public async Task ChangeRestriction(string actionToTake)
    {
        await page.GetByRole(AriaRole.Radio,
            new()
            {
                Name = actionToTake,
                Exact = true
            })
           .CheckAsync();
        await page.Locator("[type='submit']").ClickAsync();
    }
    
    public async Task LastStartDateErrorMessage(string message)
    {
        await Assertions.Expect(page.Locator(".govuk-error-summary__body")).ToContainTextAsync(message);
    }
}