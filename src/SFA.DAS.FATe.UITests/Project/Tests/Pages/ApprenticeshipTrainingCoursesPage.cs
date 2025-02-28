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
}
