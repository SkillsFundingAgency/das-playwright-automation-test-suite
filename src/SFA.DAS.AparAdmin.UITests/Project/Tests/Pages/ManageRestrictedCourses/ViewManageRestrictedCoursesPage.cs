using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;
public class ViewMangeRestrictedCoursesPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("View and manage restricted courses");
    } 
    public async Task<ViewManageRestrictedCoursesPage> SearchCourse()
    {
        await page.Locator("#search-term-input").FillAsync("Leadership");
        await ClickSubmit();
        return await VerifyPageAsync(() => new ViewManageRestrictedCoursesPage(context));
    }    
    public async Task<ViewManageRestrictedCoursesPage> SearchCourseandFilter()
    {
        await page.Locator("#search-term-input").FillAsync("Leadership");
        await page.check($"input[type='checkbox'][value='Apprenticeship']");
        await ApplyFilter();
        return await VerifyPageAsync(() => new ViewManageRestrictedCoursesPage(context));
    }    

    private async ApplyFilter()
    {
        await page.Locator($"#filters-submit").ClickAsync();
    }

    public async Task<ViewManageREstrictedCoursesPage> ApplyCourseAndVerifyResult()
    {
        var courseList = page.Locator(".app-results-list__item");
        await expect(listItems.filter(item => item.text.contains("Leadership")).toBeTruthy());
    }
}
