﻿namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class TrainingProvidersPage(ScenarioContext context) : FATeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).
        ToContainTextAsync("Training providers for");

    public async Task RequestTrainingProvider() => await page.GetByRole(AriaRole.Link, new() { Name = "Ask if training providers can" }).ClickAsync();

    public async Task VerifyAndApplySingleFilters_ProviderPage()
    {
        await VerifyNoFiltersAreApplied();
        await VerifyDistanceFilterSelection("10 miles");
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hounslow (within 10 miles)");
        await ClearSpecificFilter("TW14 Hounslow (within 10 miles)");
        await VerifyNoFiltersAreApplied();
        await SelectApprenticeTravelDistance("10 miles");
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hounslow (within 10 miles)");
        await ClearSpecificFilter("TW14 Hounslow (within 10 miles)");
        await VerifyNoFiltersAreApplied();
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hounslow (within 10 miles)");
        await SelectApprenticeTravelDistance("20 miles");
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hounslow (within 20 miles)");
        await SelectApprenticeTravelDistance("100 miles");
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hounslow (within 100 miles)");
        await ClearSpecificFilter("TW14 Hounslow (within 100 miles)");
        await VerifyNoFiltersAreApplied();
        await SelectExcellentReviews_EmployerRating();
        await ApplyFilters();
        await VerifyFilterIsSet("Excellent");
        await ClearSpecificFilter("Excellent");
        await VerifyNoFiltersAreApplied();
        await SelectExcellentReviews_ApprenticeRating();
        await ApplyFilters();
        await VerifyFilterIsSet("Good");
        await ClearSpecificFilter("Good");
        await VerifyNoFiltersAreApplied();
        await SelectAchievementRateCheckbox("Excellent");
        await ApplyFilters();
        await VerifyFilterIsSet("Above 70%");
        await ClearSpecificFilter("Above 70%");
        await CheckAndVerifyCheckbox("filteritem-modes-filter-Workplace");
        await ApplyFilters();
        await VerifyFilterIsSet("At apprentice's workplace");
        await ClearSpecificFilter("At apprentice's workplace");
        await CheckAndVerifyCheckbox("filteritem-modes-filter-Provider");
        await ApplyFilters();
        await VerifyFilterIsSet("Day release");
        await VerifyFilterIsSet("Block release");
        await ClearSpecificFilter("Day release");
        await ClearSpecificFilter("Block release");
        await CheckAndVerifyCheckbox("filteritem-modes-filter-DayRelease");
        await ApplyFilters();
        await VerifyFilterIsSet("Day release");
        await ClearSpecificFilter("Day release");
        await CheckAndVerifyCheckbox("filteritem-modes-filter-BlockRelease");
        await ApplyFilters();
        await VerifyFilterIsSet("Block release");
        await ClearSpecificFilter("Block release");
        await ClearAllFilters();
        await VerifyNoFiltersAreApplied();
    }
    public async Task VerifyAndApplyMultipleFilters_ProviderPage()
    {

        await VerifyNoFiltersAreApplied();
        await VerifyDistanceFilterSelection("10 miles");
        await EnterApprenticeWorkLocation(fateDataHelper.PartialPostCode, fateDataHelper.PostCodeDetails);
        await SelectApprenticeTravelDistance("10 miles");
        await SelectAchievementRateCheckbox("Excellent");
        await CheckAndVerifyCheckbox("filteritem-modes-filter-Workplace");
        await CheckAndVerifyCheckbox("filteritem-modes-filter-Provider");
        await ApplyFilters();
        await VerifyFilterIsSet("TW14 Hounslow (within 10 miles)");
        await VerifyFilterIsSet("Above 70%");
        await VerifyFilterIsSet("Day release");
        await VerifyFilterIsSet("Block release");
        await VerifyFilterIsSet("At apprentice's workplace");
        await ClearAllFilters();
        await VerifyNoFiltersAreApplied();
    }
    public async Task SelectSortResultsDropdown(string optionValue)
    {
        var dropdown = page.Locator("#course-providers-orderby");

        await dropdown.SelectOptionAsync(new SelectOptionValue { Value = optionValue });
    }
    public async Task ClickFirstProviderLink()
    {
        var providerLink = page.Locator("a[id^='provider-']").First;
        await providerLink.ClickAsync();
    }
    public async Task VerifyProviderListed(string providerName, bool isListed)
    {
        var locator = page.Locator($"//a[@class='govuk-link' and normalize-space(text())='{providerName}']");
        var visible = await locator.IsVisibleAsync();

        if (isListed)
        {
            Assert.True(visible, $"Expected provider '{providerName}' to be listed, but it was not.");
        }
        else
        {
            Assert.False(visible, $"Expected provider '{providerName}' to NOT be listed, but it was.");
        }
    }
}
