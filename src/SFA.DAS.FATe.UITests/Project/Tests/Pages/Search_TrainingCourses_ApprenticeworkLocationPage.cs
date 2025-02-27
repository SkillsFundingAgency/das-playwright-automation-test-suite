using SFA.DAS.FATe.UITests.Project.Tests.Pages;

namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class Search_TrainingCourses_ApprenticeworkLocationPage(ScenarioContext context) : FATeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).
        ToContainTextAsync("Search for apprenticeship training courses and training providers");

    public async Task<ApprenticeshipTrainingCoursesPage> SearchWithCourseOnly()
    {
        await page.Locator("#CourseTerm").ClickAsync();
        await page.Locator("#CourseTerm").FillAsync(fateDataHelper.PartialCourseName);
        await ClickContinue();
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }

    public async Task<ApprenticeshipTrainingCoursesPage> SearchWithCourseNoResults()
    {
        await page.Locator("#CourseTerm").ClickAsync();
        await page.Locator("#CourseTerm").FillAsync(fateDataHelper.NoResultsCourseName);
        await ClickContinue();
        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }

    public async Task<ApprenticeshipTrainingCoursesPage> SearchWithCourseAndApprenticeWorkLocation()
    {
        await page.Locator("#CourseTerm").ClickAsync();
        await page.Locator("#CourseTerm").FillAsync(fateDataHelper.Course);
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
        var currentUrl = page.Url;
        if (!currentUrl.Contains("Distance=10"))
        {
            throw new Exception("The URL does not contain the required Distance=10 parameter.");
        }
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

}
