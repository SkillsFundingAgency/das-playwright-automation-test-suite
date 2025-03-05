namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public abstract class FATeBasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly FATeDataHelper fateDataHelper = context.Get<FATeDataHelper>();

    public async Task ClickContinue() => await page.Locator("#continue").ClickAsync();
    public async Task<ShortlistPage> ViewShortlist()
    {
        await page.Locator("#header-view-shortlist").ClickAsync();
        return await VerifyPageAsync(() => new ShortlistPage(context));
    }
    public async Task<FATeHomePage> ReturnToStartPage()
    {
        await page.Locator(".govuk-header__link.govuk-header__service-name").ClickAsync();
        return await VerifyPageAsync(() => new FATeHomePage(context));
    }
    public async Task<Search_TrainingCourses_ApprenticeworkLocationPage> ReturnToSearch_TrainingCourses_ApprenticeworkLocationPage()
    {
        await page.Locator("#home-breadcrumb").ClickAsync();
        return await VerifyPageAsync(() => new Search_TrainingCourses_ApprenticeworkLocationPage(context));
    }
    public async Task SelectAutocompleteOption(string optionText)
    {
        var autocompleteOption = page.Locator($"text={optionText}");
        await autocompleteOption.ClickAsync();
    }
    public async Task SelectJobCategory(string categoryName)
    {
        var checkboxLocator = page.Locator($"input.govuk-checkboxes__input[name='Categories'][value='{categoryName}']");
        var jobCategoriesShowButton = page.Locator("button[aria-label='Job categories , Show this section']");

        if (!await checkboxLocator.IsVisibleAsync())
        {
            await jobCategoriesShowButton.ClickAsync();
            await checkboxLocator.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        }
        await checkboxLocator.ScrollIntoViewIfNeededAsync();
        if (!await checkboxLocator.IsCheckedAsync())
        {
            await checkboxLocator.ClickAsync();
        }
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
    public async Task VerifyFilterIsSet(string filterText)
    {
        var filterLocator = page.Locator($"a.das-filter__tag.das-breakable:has-text('{filterText}')");
        await filterLocator.WaitForAsync();
        await Assertions.Expect(filterLocator).ToBeVisibleAsync();
    }
    public async Task VerifyCourseSearchResults(string resultsword)
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
    public async Task ClearAllFilters()
    {
        var clearFiltersLink = page.Locator("a.das-filter__selected-action.govuk-link");

        if (await clearFiltersLink.IsVisibleAsync())
        {
            await clearFiltersLink.ClickAsync();
            Console.WriteLine("All filters have been cleared.");
        }
        else
        {
            Console.WriteLine("No filters were applied, so nothing to clear.");
        }
    }
    public async Task VerifyTrainingProviderWithinDistance(int miles)
    {
        // Locator for all provider links
        var providerLinks = page.Locator("a.das-search-results__link");

        // Wait for at least one link to be visible
        await Assertions.Expect(providerLinks).ToBeVisibleAsync();

        // Iterate over all provider links and check if any of them contain the desired distance text
        var providerCount = await providerLinks.CountAsync();
        for (int i = 0; i < providerCount; i++)
        {
            var providerLink = providerLinks.Nth(i);

            // Check if the link contains the specific miles distance in its text
            var linkText = await providerLink.TextContentAsync();
            if (linkText.Contains($"{miles} miles"))
            {
                // If the distance is found, verify the link is visible and contains the correct distance text
                await Assertions.Expect(providerLink).ToBeVisibleAsync();
                await Assertions.Expect(providerLink).ToContainTextAsync($"{miles} miles");
                return; // Exit once we find the correct link
            }
        }

        // If no provider links were found with the specified miles, throw an exception
        throw new Exception($"No training provider link found with '{miles} miles'.");
    }

    public async Task VerifyJobCategoryResults(string jobCategory)
    {
        var jobCategoryLocator = page.Locator($"dd.das-definition-list__definition:has-text('{jobCategory}')");
        await Assertions.Expect(jobCategoryLocator).ToBeVisibleAsync();
        await Assertions.Expect(jobCategoryLocator).ToContainTextAsync(jobCategory);
    }
}
