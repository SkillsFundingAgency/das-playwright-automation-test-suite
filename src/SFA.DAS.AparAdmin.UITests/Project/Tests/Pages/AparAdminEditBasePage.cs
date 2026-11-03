using System;
using System.Collections.Generic;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

public abstract class AparAdminEditBasePage(ScenarioContext context) : AparAdminBasePage(context)
{
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
    
    public async Task SelectCourse(string courseName)
    {
        await page.Locator("#SelectedLarsCode").FillAsync(courseName);
        bool valid = await page.Locator("#SelectedLarsCode__option--0").IsVisibleAsync();
        if(valid)
        {
            await page.Locator("#SelectedLarsCode__option--0").ClickAsync();
        }
        await page.Locator(".govuk-button").ClickAsync();
    }

    public async Task ClickChangeCourseDateDetails(string larsCode)
    {
        await page.Locator($"[href*='{larsCode}']").ClickAsync();
    }

    public async Task SelectOptionEditRemove(string editType)
    {
        string radioId = editType switch
        {
            "Edits" => "selected-option-change",
            "Removes"  => "selected-option-remove",
             _        => throw new ArgumentException($"Unknown message type: {editType}")
        };
        await  page.Locator($"#{radioId}").ClickAsync();
    }

    public async Task ErrorMessageForWrongInput()
    {
        await Assertions.Expect(page.Locator(".govuk-error-summary__title")).ToContainTextAsync("There is a problem");
    }

    public async Task AssertBanner(string keyWord, string bannerMessage)
    {
        var parent = page.Locator("li", new PageLocatorOptions { HasTextString = keyWord });
        var listItem = parent;
        string banner = bannerMessage switch
        {
            "Last start date added" => ".govuk-tag--orange",
            "Closed to new starts"  => ".govuk-tag--grey",
            "Open to new starts"   => ".govuk-tag--green",
             _        => throw new ArgumentException($"Unknown message type: {bannerMessage}")
        };
        await Assertions.Expect(listItem.Locator(banner)).ToContainTextAsync(bannerMessage);   
    }
}