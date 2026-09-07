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

    public async Task SelectManageProviders(string larsCode)
    {
        await  page.Locator($"//a[contains(@href, '{larsCode}')]").ClickAsync();
    }

    public async Task NavigateOnLarsCode(string larsCode)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var currentUrl = page.Url;
            if (!currentUrl.Contains($"{larsCode}"))
            {
                throw new Exception($"URL does not contain expected provider of LARS code {larsCode}");
            }
    }

    public async Task SearchProviderWithUKPRN(string UKPRN)
    {

        await page.Locator("#search-term-input").FillAsync(UKPRN);
        await ApplyFilter();
        
    }

    public async Task ThenTheUserIsAbleToVerifyTheProviderResults()
    {
        var results = page.Locator(".app-results-list__item");
        var resultCount = await results.CountAsync();

        if (resultCount == 0)
        {
            throw new Exception("No providers were found.");
        }
    }

    public async Task SelectProviderFilter(string filterName)
    {
        await page.GetByRole(AriaRole.Checkbox,
            new()
            {
                Name = filterName,
                Exact = true
            })
           .CheckAsync();
    }
}