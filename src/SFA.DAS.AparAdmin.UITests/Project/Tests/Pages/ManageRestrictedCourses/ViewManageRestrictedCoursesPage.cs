using Microsoft.Playwright;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;
using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;

public class ViewMangeRestrictedCoursesPage(ScenarioContext context)
    : BasePage(context)
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

    public async Task ApplyFilter()
    {
        await page.Locator("#filters-submit").ClickAsync();

        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task VerifySelectedFilter(string trainingType)
    {
        var selectedFilters = page.Locator(".das-filter__selected-filters");

        var selectedFilter = selectedFilters.GetByText(
            trainingType,
            new() { Exact = true });

        await Assertions.Expect(selectedFilter).ToBeVisibleAsync();
    }

    public async Task ClearAllFilters()
    {
        var clearFilters = page.Locator(".das-filter__selected-action");

        if (await clearFilters.CountAsync() > 0)
        {
            await clearFilters.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }
    }

    public async Task VerifyNoFiltersSelected()
    {
        var selectedFilters = page.Locator(".das-filter__tag");
        await Assertions.Expect(selectedFilters).ToHaveCountAsync(0);
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