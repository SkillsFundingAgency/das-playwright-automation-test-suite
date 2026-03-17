using SFA.DAS.FATe.UITests.Project.Tests.Pages;

namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class Search_TrainingCourses_ApprenticeworkLocationPage(ScenarioContext context) : FATeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).
        ToContainTextAsync("Find training if you’re an employer");

    public async Task<ApprenticeshipTrainingCoursesPage> SearchWithCourseOnly()
    {
        await page.Locator("#keyword-input").ClickAsync();
        await page.Locator("#keyword-input").FillAsync(fateDataHelper.PartialCourseName);
        await ClickContinue();
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }
    public async Task<ApprenticeshipTrainingCoursesPage> SearchWithCourseNoResults()
    {
        await page.Locator("#keyword-input").ClickAsync();
        // Use a unique search term to ensure no real courses match (make test deterministic)
        var uniqueSearchTerm = Guid.NewGuid().ToString("N");
        // store the generated term so later steps can verify the filter tag
        objectContext.Set("NoResultsSearchTerm", uniqueSearchTerm);
        await page.Locator("#keyword-input").FillAsync(uniqueSearchTerm);
        await ClickContinue();
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }
    public async Task<ApprenticeshipTrainingCoursesPage> SearchWithCourseAndApprenticeWorkLocation()
    {
        await page.Locator("#keyword-input").ClickAsync();
        await page.Locator("#keyword-input").FillAsync(fateDataHelper.Course);
        await page.Locator("#search-location").ClickAsync();
        await page.Locator("#search-location").FillAsync(fateDataHelper.Location);
        await ClickContinue();
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }
    public async Task<ApprenticeshipTrainingCoursesPage> SearchWithApprenticeWorkLocation()
    {
        await page.Locator("#search-location").ClickAsync();
        await page.Locator("#search-location").FillAsync(fateDataHelper.Location);
        await SelectAutocompleteOption(fateDataHelper.LocationDetails);
        await ClickContinue();
        //Team discusising to keep this or not in the url once confirmed I will remove or update this
        //var currentUrl = page.Url;
        //if (!currentUrl.Contains("Distance=10"))
        //{
        //    throw new Exception("The URL does not contain the required Distance=10 parameter.");
        //}
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }
    public async Task<ApprenticeshipTrainingCoursesPage> SearchWithoutCourseAndApprenticeWorkLocation()
    {
        await ClickContinue();
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }
    public async Task<SearchForTrainingProviderPage> AccessSearchForTrainingProvider()
    {
        await page.Locator("text=search for a training provider").ClickAsync();
        return await VerifyPageAsync(() => new SearchForTrainingProviderPage(context));
    }
    public async Task<ApprenticeshipTrainingCoursesPage> BrowseAllCourses()
    {
        await page.Locator("text=Browse all courses").ClickAsync();
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }
    public async Task<ApprenticeshipTrainingCoursesPage> SelectTrainingTypes(
     params TrainingType[] trainingTypes)
    {
        foreach (var type in trainingTypes)
        {
            var checkboxLocator = type switch
            {
                TrainingType.ApprenticeshipUnits =>
                    page.Locator("[id='filteritem-training-types-Apprenticeship units']"),

                TrainingType.FoundationApprenticeships =>
                    page.Locator("[id='filteritem-training-types-Foundation apprenticeships']"),

                TrainingType.Apprenticeships =>
                    page.Locator("[id='filteritem-training-types-Apprenticeships']"),

                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

            await checkboxLocator.CheckAsync();
        }

        await ClickContinue();

        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }

}
