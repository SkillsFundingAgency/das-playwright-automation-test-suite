using System;
using System.Collections.Generic;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

public abstract class AparAdminBasePage(ScenarioContext context) : BasePage(context)
{
    #region Pagination
    public async Task VerifyPaginationLinks(List<int> pageNumbers)
    {
        foreach (var pageNumber in pageNumbers)
        {
            var pageLink = page.Locator($".navigation-flexbox a:has-text('{pageNumber}')");
            await pageLink.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var currentUrl = page.Url;
            if (!currentUrl.Contains($"PageNumber={pageNumber}"))
            {
                throw new Exception($"URL does not contain expected PageNumber={pageNumber}");
            }
        }
        var nextLink = page.Locator(".navigation-flexbox a:has-text('Next »')");
        if (await nextLink.IsVisibleAsync())
        {
            await nextLink.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
        var previousLink = page.Locator(".navigation-flexbox a:has-text('« Previous')");
        if (await previousLink.IsVisibleAsync())
        {
            await previousLink.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
    }
    #endregion

    #region Filters
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

    public async Task SelectFilter(string filterChoice)
    {
        await page.GetByRole(AriaRole.Checkbox,
            new()
            {
                Name = filterChoice,
                Exact = true
            })
           .CheckAsync();
    }

    public async Task SearchFunctionality(string searchKeyWord)
    {
        await page.Locator("#search-term-input").FillAsync(searchKeyWord);
        await ApplyFilter();
    }

    public async Task VerifyResults()
    {
        var results = page.Locator(".app-results-list__item");
        var resultCount = await results.CountAsync();

        if (resultCount == 0)
        {
            throw new Exception("No results were displayed.");
        }
    }

    public async Task VerifyNoFiltersSelected()
    {
        var selectedFilters = page.Locator(".das-filter__tag");
        await Assertions.Expect(selectedFilters).ToHaveCountAsync(0);
    }
    #endregion
}