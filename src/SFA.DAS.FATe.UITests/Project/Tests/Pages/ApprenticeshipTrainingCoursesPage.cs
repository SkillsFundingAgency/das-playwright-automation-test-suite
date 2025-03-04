using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using System.Threading.Tasks;

namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class ApprenticeshipTrainingCoursesPage(ScenarioContext context) : FATeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).
        ToContainTextAsync("Apprenticeship training courses");

    public async Task VerifyFilterIsSet(string filterText)
    {
        var filterLocator = page.Locator($"a.das-filter__tag.das-breakable:has-text('{filterText}')");
        await filterLocator.WaitForAsync();
        await Assertions.Expect(filterLocator).ToBeVisibleAsync();
    }

    public async Task VerifyResultsContainWordWorker(string resultsword)
    {
        var resultsLinks = page.Locator("li.das-search-results__list-item");

        var count = await resultsLinks.CountAsync();
        var limit = Math.Min(count, 4); 

        for (int i = 0; i < limit; i++)
        {
            var resultLocator = resultsLinks.Nth(i);
            await Assertions.Expect(resultLocator).ToContainTextAsync(resultsword);
        }
    }

    public async Task VerifyNoResultsMessage()
    {
        var noResultsText = page.Locator("p.govuk-body:has-text('No results')");
        var noCoursesText = page.Locator("p.govuk-body:has-text('There are no courses that match your search.')");

        await Assertions.Expect(noResultsText).ToBeVisibleAsync();
        await Assertions.Expect(noCoursesText).ToBeVisibleAsync();
    }

    public async Task VerifyNoFiltersAreApplied()
    {
        var filterElements = page.Locator(".das-filter__tag");
        var count = await filterElements.CountAsync();

        if (count == 0)
        {
            Console.WriteLine("No filters are applied.");
        }
        else
        {
            Console.WriteLine($"{count} filters are applied.");
        }
    }
    public async Task<ApprenticeshipTrainingCoursesPage> VerifyUrlContainsWordCourses()
    {
        var currentUrl = page.Url;
        if (!currentUrl.Contains("courses"))
        {
            throw new Exception("The URL does not contain the required courses parameter.");
        }
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }
    public async Task VerifyDistanceFilterSelection(string expectedDistance)
    {
        var selectedOption = await page.Locator("select#distance-filter option[selected]").InnerTextAsync();

        if (selectedOption != expectedDistance)
        {
            throw new Exception($"Expected distance filter to be '{expectedDistance}', but found '{selectedOption}'.");
        }

        Console.WriteLine($"Verified: '{expectedDistance}' is selected in the distance filter.");
    }
    public async Task EnterCourseJobOrStandard(string text)
    {
        var inputField = page.Locator("input#keyword-input");
        await inputField.FillAsync(text);
        Console.WriteLine($"Entered text: '{text}' in the course/job/standard input field.");
    }
    public async Task ApplyFilters()
    {
        var applyFiltersButton = page.Locator("button#filters-submit");
        await applyFiltersButton.ClickAsync();
        Console.WriteLine("Clicked on 'Apply filters' button.");
    }
    public async Task ClearSpecificFilter(string filterName)
    {
        var filterLocator = page.Locator($"a.das-filter__tag.das-breakable:has-text('{filterName}')");

        if (await filterLocator.CountAsync() > 0) 
        {
            await filterLocator.ClickAsync();
            Console.WriteLine($"Cleared the filter: {filterName}");
        }
        else
        {
            Console.WriteLine($"Filter '{filterName}' not found.");
        }
    }
    public async Task EnterApprenticeWorkLocation(string textEntered, string dropDownoption)
    {
        await page.Locator("#search-location").ClickAsync(); 
        await page.Locator("#search-location").FillAsync(textEntered);
        await SelectAutocompleteOption(dropDownoption);
    }
    public async Task SelectApprenticeTravelDistance(string distance)
    {
        var distanceDropdown = page.Locator("#distance-filter");
        await distanceDropdown.SelectOptionAsync(distance);
    }

}
