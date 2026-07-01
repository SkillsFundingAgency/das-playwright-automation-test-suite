namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class ApprenticeshipTrainingCoursesPage(ScenarioContext context) : FATeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).
        ToContainTextAsync("Training courses");
    public async Task VerifyNoResultsMessage()
    {
        var noResultsVisible = await page.Locator("p.govuk-body:has-text('No results')").IsVisibleAsync();
        var noCoursesVisible = await page.Locator("p.govuk-body:has-text('There are no courses that match your search.')").IsVisibleAsync();

        if (!noResultsVisible && !noCoursesVisible)
        {
            throw new Exception("Expected a no-results message but none were found.");
        }
    }

    public async Task<ApprenticeshipTrainingCoursesPage> AddAProviderToShortlist()
    {
        await page.GetByRole(AriaRole.Heading, new() { Name = "Accounts or finance assistant" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "View providers for this course" }).ClickAsync();
        await page.Locator("#add-to-shortlist-10003193").ClickAsync();
        await Assertions.Expect(page.Locator("#remove-from-shortlist-10003193")).ToContainTextAsync("Remove from shortlist");

        await page.Locator("#add-to-shortlist-10036143").ClickAsync();
        await Assertions.Expect(page.Locator("#remove-from-shortlist-10036143")).ToContainTextAsync("Remove from shortlist");
        await page.GetByRole(AriaRole.Link, new() { Name = "View shortlist" }).ClickAsync();
        await page.Locator("summary").ClickAsync();
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Accounts or finance assistant (level 2)");
        await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync("HUGH BAIRD COLLEGE");
        await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync("SOUTH GLOUCESTERSHIRE AND STROUD COLLEGE");

        await page.GetByRole(AriaRole.Link, new() { Name = "Search" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Browse all courses" }).ClickAsync();
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }

    public async Task AddProviderToShortlistUsingLocation()
    {
        await page.GetByRole(AriaRole.Combobox, new() { Name = "Learner's work location" }).FillAsync("L20 7EW");
        await page.GetByRole(AriaRole.Option, new() { Name = "L20 7EW" }).ClickAsync();
        await page.GetByLabel("Learner can travel").SelectOptionAsync(new[] { "2" });
        await page.Locator("#filters-submit-top").ClickAsync();
        await page.GetByRole(AriaRole.Heading, new() { Name = "Accounts or finance assistant" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "View providers for this course" }).ClickAsync();
        await page.Locator("#add-to-shortlist-10003193").ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "View shortlist" }).ClickAsync();
    }

    public async Task SearchApprenticeshipInApprenticeshipTrainingCoursesPage(string searchTerm)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Course" }).FillAsync(searchTerm);

        await page.Locator("#filters-submit").ClickAsync();
    }

    public async Task<Search_TrainingCourses_ApprenticeworkLocationPage> VerifyNoresultStartAnewSearchLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "start a new search" }).ClickAsync();
        return await VerifyPageAsync(() => new Search_TrainingCourses_ApprenticeworkLocationPage(context));
    }
    public async Task<ApprenticeshipTrainingCoursesPage> VerifyUrlContainsWordCourses()
    {
        var currentUrl = page.Url;
        if (!currentUrl.Contains("courses"))
        {
            throw new Exception($"Expected 'courses' in the URL, but found: {currentUrl}");
        }
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }
    public async Task VerifyAndApplySingleFilters()
    {
        await VerifyNoFiltersAreApplied();
        await VerifyDistanceFilterSelection("10 miles");
        await EnterCourseJobOrStandard("Professional");
        await ApplyFilters();
        await VerifyFilterIsSet("Professional");
        await ClearSpecificFilter("Professional");
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hillingdon (within 10 miles)");
        await ClearSpecificFilter("TW14 Hillingdon (within 10 miles)");
        await VerifyNoFiltersAreApplied();
        await SelectApprenticeTravelDistance("10 miles");
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hillingdon (within 10 miles)");
        await ClearSpecificFilter("TW14 Hillingdon (within 10 miles)");
        await VerifyNoFiltersAreApplied();
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hillingdon (within 10 miles)");
        await SelectApprenticeTravelDistance("20 miles");
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hillingdon (within 20 miles)");
        await SelectApprenticeTravelDistance("100 miles");
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hillingdon (within 100 miles)");
        await ClearSpecificFilter("TW14 Hillingdon (within 100 miles)");
        await VerifyNoFiltersAreApplied();
        await SelectJobCategory("Agriculture, environmental and animal care");
        await ApplyFilters();
        await VerifyFilterIsSet("Agriculture, environmental and animal care");
        await ClearSpecificFilter("Agriculture, environmental and animal care");
        await SelectApprenticeshipLevel("Level 2");
        await ApplyFilters();
        await VerifyFilterIsSet("Level 2");
        await ClearSpecificFilter("Level 2");
        await VerifyNoFiltersAreApplied();
    }
    public async Task ApplyMultipleFilters_ClearAtOnce()
    {
        await VerifyNoFiltersAreApplied();
        await VerifyDistanceFilterSelection("10 miles");
        await EnterCourseJobOrStandard("Professional");
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await SelectApprenticeTravelDistance("100 miles");
        await SelectJobCategory("Agriculture, environmental and animal care");
        await SelectJobCategory("Care services");
        await SelectJobCategory("Digital");
        await SelectApprenticeshipLevel("Level 2");
        await ApplyFilters();
        await VerifyFilterIsSet("Professional");
        await VerifyFilterIsSet("TW14 Hillingdon (within 100 miles)");
        await VerifyFilterIsSet("Agriculture, environmental and animal care");
        await VerifyFilterIsSet("Care services");
        await VerifyFilterIsSet("Digital");
        await VerifyFilterIsSet("Level 2");
        await ClearAllFilters();
        await VerifyNoFiltersAreApplied();
    }
    public async Task ApplyCourseFilterAndVerifyResultsForProfessional()
    {
        await VerifyNoFiltersAreApplied();
        await VerifyDistanceFilterSelection("10 miles");
        await EnterCourseJobOrStandard("professional");
        await ApplyFilters();
        await VerifyFilterIsSet("professional");
        await VerifyCourseSearchResults("professional");
        await ClearAllFilters();
    }
    public async Task ApplyFoundationStandardsFilterAndVerifyResultsForFoundationStandards()
    {
        await VerifyNoFiltersAreApplied();
        await SelectApprenticeshipType("FoundationApprenticeship");
        await ApplyFilters();
        await VerifyFilterIsSet("Foundation apprenticeships");
        await VerifyAllResultsHaveFoundationTag();
        await ClearAllFilters();
    }

    public async Task ApplyFoundationStandardsFilter()
    {
        await VerifyNoFiltersAreApplied();
        await SelectApprenticeshipType("FoundationApprenticeship");
        await ApplyFilters();
    }
    public async Task ApplyLocationFilterAndVerifyResultsForTW14_50miles()
    {
        await SelectApprenticeTravelDistance("50 miles");
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await SelectApprenticeTravelDistance("50 miles");
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hillingdon (within 50 miles)");
        await VerifyTrainingProviderWithinDistance(50);
        await ClearAllFilters();
    }
    public async Task ApplyJobcategoriesFilterAndVerifyResults_ProtectiveServices()
    {
        await VerifyNoFiltersAreApplied();
        await SelectJobCategory("Protective services");
        await ApplyFilters();
        await VerifyFilterIsSet("Protective services");
        await VerifyJobCategoryResults("Protective services");
        await ClearAllFilters();
    }
    public async Task SelectCourseByName(string courseName)
    {
        var courseLink = page
            .Locator("a.das-search-results__link")
            .Filter(new() { HasText = courseName });

        await Assertions.Expect(courseLink).ToBeVisibleAsync();
        await courseLink.ClickAsync();
    }

    public async Task<ApprenticeshipTrainingCourseDetailsPage> SelectFirstTrainingResult(string title)
    {
        var firstLinkText = await page.GetByRole(AriaRole.Link, new() { Name = title, Exact = false }).Filter(new() { Has = page.Locator("span.das-no-wrap") }).First.TextContentAsync();

        objectContext.SetTrainingCourseName(firstLinkText);

        objectContext.SetDebugInformation($"selected '{firstLinkText}' course");

        await page.GetByRole(AriaRole.Link, new() { Name = firstLinkText, Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipTrainingCourseDetailsPage(context));
    }
}
