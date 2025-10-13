

namespace SFA.DAS.ProvideFeedback.UITests.Project.Tests.Pages;

public abstract class EmployerFeedbackBasePage(ScenarioContext context) : BasePage(context)
{
    protected static string Labels => (".multiple-choice label");

    protected async Task OpenFeedbackUsingSurveyCode()
    {
        var url = UriHelper.GetAbsoluteUri(UrlConfig.ProviderFeedback_BaseUrl, objectContext.GetUniqueSurveyCode());

        await Navigate(url);
    }

    protected async Task SelectOptionAndContinue() => await SelectOptionAndContinue(Labels);

    protected async Task SelectOptionAndContinue(string selector)
    {
        var x = await page.Locator(selector).Filter(new LocatorFilterOptions { Visible = true }).AllTextContentsAsync();

        objectContext.SetDebugInformation($"list found - {x.ToString(",")}");

        List<string> checkboxList = [.. x.Where(y => !string.IsNullOrEmpty(y))];

        objectContext.SetDebugInformation($"list found - {checkboxList.ToString(",")}");

        for (int i = 0; i <= 2; i++)
        {
            var randomoption = RandomDataGenerator.GetRandomElementFromListOfElements(checkboxList);

            await page.GetByText(randomoption).ClickAsync();

            checkboxList.Remove(randomoption);
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}

//Can be used in other scenario. please do not delete
public static class AsyncExtensions
{
    public static async Task<IEnumerable<T>> WhereAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        var results = new List<T>();
        foreach (var item in source)
        {
            if (await predicate(item))
            {
                results.Add(item);
            }
        }
        return results;
    }
}
