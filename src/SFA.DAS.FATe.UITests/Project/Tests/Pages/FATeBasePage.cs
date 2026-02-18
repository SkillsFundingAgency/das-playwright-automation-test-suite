using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public abstract class FATeBasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly FATeDataHelper fateDataHelper = context.Get<FATeDataHelper>();
    public async Task ClickContinue()
    {
        var continueButton = page.Locator("#filters-submit");

        if (!await continueButton.IsVisibleAsync())
        {
            continueButton = page.Locator("#continue");
        }
        if (!await continueButton.IsVisibleAsync())
        {
            continueButton = page.Locator(".govuk-button");
        }

        await continueButton.ClickAsync();
    }
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
    public async Task<ApprenticeshipTrainingCoursesPage> ReturnToCourseSearchResults()
    {
        await page.Locator("#courses-breadcrumb").ClickAsync();
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }
    public async Task<ApprenticeshipTrainingCourseDetailsPage> ReturnToCourseDetail()
    {
        await page.Locator("#course-detail-breadcrumb").ClickAsync();
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCourseDetailsPage(context));
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
    public async Task SelectApprenticeshipLevel(string levelName)
    {
        var checkbox = page.GetByRole(AriaRole.Checkbox, new() { Name = levelName });

        if (!await checkbox.IsVisibleAsync())
        {
            var expandButton = page.GetByRole(AriaRole.Button, new() { Name = "Apprenticeship level , Show" });
            if (await expandButton.IsVisibleAsync())
            {
                await expandButton.ClickAsync();
                await checkbox.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            }
        }
        if (!await checkbox.IsCheckedAsync())
        {
            await checkbox.CheckAsync();
        }
    }
    public async Task SelectApprenticeshipType(string typeName)
    {
        var checkbox = page.Locator($"input.govuk-checkboxes__input[name='ApprenticeshipTypes'][value='{typeName}']");
        var expandButton = page.GetByRole(AriaRole.Button, new() { Name = "Training type , Show" });

        if (!await checkbox.IsVisibleAsync())
        {
            if (await expandButton.IsVisibleAsync())
            {
                await expandButton.ClickAsync();
                await checkbox.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            }
        }

        if (!await checkbox.IsCheckedAsync())
        {
            await checkbox.CheckAsync();
        }
    }

    public async Task VerifyApprenticeshipLevelResult(string level)
    {
        var levelLocator = page.Locator("dt.das-definition-list__title:has-text('Apprenticeship level') + dd.das-definition-list__definition");
        await Assertions.Expect(levelLocator.First).ToBeVisibleAsync();
        await Assertions.Expect(levelLocator.First).ToContainTextAsync($"Level {level}");
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
        var filterLocator = page.Locator($"a.das-filter__tag.das-breakable:has-text(\"{filterName}\")");

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
        var filterLocator = page.GetByRole(AriaRole.Link, new() { Name = filterText, Exact = true});
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
        var providerLinks = page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex($"View \\d+ training providers within {miles} miles") });

        int count = await providerLinks.CountAsync();
        if (count == 0)
        {
            throw new Exception($"No provider links found containing text: 'View X training providers within {miles} miles'.");
        }
        await Assertions.Expect(providerLinks).ToHaveCountAsync(count);
    }
    public async Task VerifyJobCategoryResults(string jobCategory)
    {
        var jobCategoryLocator = page.Locator($"dt.das-definition-list__title:has-text('Job Category') + dd.das-definition-list__definition");
        await Assertions.Expect(jobCategoryLocator.First).ToBeVisibleAsync();
        await Assertions.Expect(jobCategoryLocator.First).ToContainTextAsync(jobCategory);
    }
    public async Task VerifyAllResultsHaveFoundationTag()
    {
        var foundationTags = page.Locator("li.das-search-results__list-item strong.govuk-tag");

        int count = await foundationTags.CountAsync();
        Assert.True(count > 0, "No search results found with a Foundation tag.");

        for (int i = 0; i < count; i++)
        {
            var tag = foundationTags.Nth(i);
            await Assertions.Expect(tag).ToBeVisibleAsync();
            await Assertions.Expect(tag).ToHaveTextAsync("Foundation");
        }
    }
    public async Task VerifyPaginationLinks(List<int> pageNumbers)
    {
        foreach (var pageNumber in pageNumbers)
        {
            var pageLink = page.Locator($".das-flex-space-around.app-pagination-nav.das-pagination-links a:has-text('{pageNumber}')");
            await pageLink.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var currentUrl = page.Url;
            if (!currentUrl.Contains($"PageNumber={pageNumber}"))
            {
                throw new Exception($"URL does not contain expected PageNumber={pageNumber}");
            }
        }
        var nextLink = page.Locator(".das-flex-space-around.app-pagination-nav.das-pagination-links a:has-text('Next »')");
        if (await nextLink.IsVisibleAsync())
        {
            await nextLink.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
        var previousLink = page.Locator(".das-flex-space-around.app-pagination-nav.das-pagination-links a:has-text('« Previous')");
        if (await previousLink.IsVisibleAsync())
        {
            await previousLink.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
    }
    public async Task<(int ProviderCount, TrainingProvidersPage Page)> ViewTrainingProvidersForCourse(string courseId)
    {
        string linkId = $"standard-{courseId}";
        var linkLocator = page.Locator($"a.das-search-results__link#standard-{courseId}:has-text('View')");

        if (!await linkLocator.IsVisibleAsync())
            throw new Exception($"Could not find the provider link for course ID: {courseId}");

        var linkText = await linkLocator.TextContentAsync();
        var match = Regex.Match(linkText ?? "", @"\d+");
        if (!match.Success)
            throw new Exception("Could not extract the provider count from link text.");

        int providerCount = int.Parse(match.Value);

        await linkLocator.ClickAsync();

        var nextPage = await VerifyPageAsync(() => new TrainingProvidersPage(context));
        return (providerCount, nextPage);
    }
    public async Task VerifyProvidersCount(int expectedCount)
    {
        var text = await page.Locator("p.govuk-body.govuk-\\!\\-font-weight-bold.govuk-\\!\\-margin-0").TextContentAsync();
        int actualCount = int.Parse(Regex.Match(text ?? "", @"\d+").Value);

        if (actualCount != expectedCount)
            throw new Exception($"Expected: {expectedCount}, but found: {actualCount}");

        Console.WriteLine($"✅ Provider count verified: {actualCount}");
    }
    public async Task SelectExcellentReviews_EmployerRating()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Reviews From 2024 to 2025 ," }).ClickAsync();
        await page.Locator("#filteritem-employer-ratings-filter-Excellent").CheckAsync();
    }
    public async Task SelectExcellentReviews_ApprenticeRating()
    {
        await page.Locator("#filteritem-apprentice-ratings-filter-Good").CheckAsync();
    }
    public async Task SelectAchievementRateCheckbox(string ratingValue)
    {
        var accordionButton = page.GetByRole(AriaRole.Button, new() { Name = "Achievement rate From 2023 to 2024" });
        if (await accordionButton.GetAttributeAsync("aria-expanded") == "false")
        {
            await accordionButton.ClickAsync();
        }
        var checkboxLocator = page.Locator($"#filteritem-qar-filter-{ratingValue}");
        await checkboxLocator.CheckAsync();
    }
    public async Task CheckAndVerifyCheckbox(string checkboxId)
    {
        var checkbox = page.Locator($"#{checkboxId}");
        await checkbox.CheckAsync();

        var isChecked = await checkbox.IsCheckedAsync();
        if (!isChecked)
        {
            throw new InvalidOperationException($"Checkbox with id '{checkboxId}' was not checked successfully.");
        }
    }
    public async Task VerifyDefaultSortOrder_AchievementRate()
    {
        var sortDropdown = page.Locator("#course-providers-orderby");
        var selectedValue = await sortDropdown.InputValueAsync();

        if (selectedValue != "AchievementRate")
        {
            throw new InvalidOperationException("The default sort order is not set to 'Achievement rate'.");
        }
    }
    public async Task VerifyAchievementRatesDescendingAsync()
    {
        var rateElements = page.Locator("dd.govuk-summary-list__value");
        var count = await rateElements.CountAsync();

        if (count == 0)
        {
            throw new InvalidOperationException("No achievement rate elements found on the page.");
        }

        var rates = new List<double>();

        for (int i = 0; i < count; i++)
        {
            var textContent = await rateElements.Nth(i).TextContentAsync();
            if (!string.IsNullOrWhiteSpace(textContent))
            {
                if (textContent.Contains("No achievement rate - not enough data", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("No achievement rate data available for this provider.");
                    continue;
                }

                var match = Regex.Match(textContent, @"(\d+(\.\d+)?)%");
                if (match.Success && double.TryParse(match.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double percentage))
                {
                    rates.Add(percentage);
                }
                else
                {
                    Console.WriteLine($"Failed to parse percentage from text: '{textContent}'.");
                }
            }
        }

        if (rates.Count == 0)
        {
            Console.WriteLine("No valid achievement rates were parsed from the page.");
            return;
        }
        for (int i = 0; i < rates.Count - 1; i++)
        {
            if (rates[i] < rates[i + 1])
            {
                throw new InvalidOperationException($"Achievement rates are not in descending order. Rate {rates[i]}% is followed by {rates[i + 1]}%.");
            }
        }
    }
    public async Task<string> AddProviderToShortlist(string ukprn)
    {
        var buttonSelector = $"#add-to-shortlist-{ukprn}";
        var button = page.Locator(buttonSelector);
        await button.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

        var providerNameLocator = page.Locator($"#provider-{ukprn}");
        await providerNameLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

        var providerName = await providerNameLocator.InnerTextAsync();

        await button.ClickAsync();
        await button.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Hidden });

        return providerName.Trim();
    }
    public async Task ClickRemoveFromShortlistAsync()
    {
        var removeButton = page.GetByRole(AriaRole.Button, new() { Name = "Remove from shortlist" });
        await removeButton.ClickAsync();
    }
    public async Task ViewTrainingProvidersLink()
    {
        var link = page.GetByRole(AriaRole.Link, new() { Name = "View training providers for this course" });
        await link.ClickAsync();
    }

    public async Task ClickViewShortlistAsync()
    {
        var viewShortlistLink = page.GetByRole(AriaRole.Link, new() { Name = "View shortlist" });
        await viewShortlistLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await viewShortlistLink.ClickAsync();
    }
    public async Task AddToShortList_TrainingProviderPage()
    {
        var button = page.GetByRole(AriaRole.Button, new() { Name = "Add to shortlist" });
        await button.ClickAsync();
        await button.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Detached });
    }
    public async Task SelectEmployerProviderRatingAsync(string optionValue)
    {     var dropdown = page.Locator("#course-providers-orderby");
        await dropdown.SelectOptionAsync(new SelectOptionValue { Value = optionValue });
    }
    public async Task VerifyEmployerReviewsSortedAsync(string reviews)
    {
        var reviewElements = page.Locator(".das-rating");

        var employerReviews = new List<(double Rating, ILocator Element)>();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var count = await reviewElements.CountAsync();

        for (int i = 0; i < count; i++)
        {
            var reviewElement = reviewElements.Nth(i);
            var reviewText = await reviewElement.Locator(".das-text--muted").InnerTextAsync();

            if (reviewText.Contains(reviews))
            {
                var ratingText = await reviewElement.Locator(".das-rating__label--Excellent").InnerTextAsync();
                double rating = 0;

                if (ratingText.Contains("Excellent"))
                    rating = 4;  // 4 stars
                else if (ratingText.Contains("Good"))
                    rating = 3;  // 3 stars
                else if (ratingText.Contains("Poor"))
                    rating = 2;  // 2 stars
                else if (ratingText.Contains("Very poor"))
                    rating = 1;  // 1 star

                employerReviews.Add((rating, reviewElement));
            }
        }

        if (employerReviews.Count == 0)
        {
            Console.WriteLine("No employer reviews found.");
            return; 
        }

        var sortedEmployerReviews = employerReviews.OrderByDescending(r => r.Rating).ToList();

        for (int i = 0; i < sortedEmployerReviews.Count; i++)
        {
            var currentReview = sortedEmployerReviews[i];
            var currentReviewElement = currentReview.Element;
            var currentRating = currentReview.Rating;

            if (i < sortedEmployerReviews.Count - 1)
            {
                var (Rating, Element) = sortedEmployerReviews[i + 1];
                var nextReviewElement = Element;
                var nextRating = Rating;

                if (currentRating < nextRating)
                {
                    throw new InvalidOperationException($"Employer reviews are not sorted correctly. Rating {currentRating} is followed by {nextRating}.");
                }
            }
        }
    }
}
