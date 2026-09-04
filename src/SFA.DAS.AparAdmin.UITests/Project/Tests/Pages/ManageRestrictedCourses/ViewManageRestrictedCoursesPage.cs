using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;

public class ViewMangeRestrictedCoursesPage(ScenarioContext context)
    : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("View and manage restricted courses");
    }

    public async Task SearchCourse(string courseName)
    {
        await page.Locator("#search-term-input").FillAsync(courseName);
        await ApplyFilter();
    }

    public async Task SelectTrainingType(string trainingType)
    {
        await page.GetByRole(AriaRole.Checkbox,
            new()
            {
                Name = trainingType,
                Exact = true
            })
           .CheckAsync();
    }

    public async Task VerifyCourseResults()
    {
        var results = page.Locator(".app-results-list__item");
        var resultCount = await results.CountAsync();

        if (resultCount == 0)
        {
            throw new Exception("No restricted course results were displayed.");
        }
    }
}