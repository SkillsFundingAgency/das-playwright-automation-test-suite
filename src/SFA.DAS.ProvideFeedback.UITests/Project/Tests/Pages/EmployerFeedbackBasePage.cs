using Microsoft.Playwright;
using SFA.DAS.Framework;
using System.Linq;

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
        var x = await page.Locator(selector).AllTextContentsAsync();


        List<string> checkboxList = [.. x.Where(y => !string.IsNullOrEmpty(y))];

        for (int i = 0; i <= 2; i++)
        {
            var randomoption = RandomDataGenerator.GetRandomElementFromListOfElements(checkboxList);

            await page.GetByText(randomoption).ClickAsync();

            checkboxList.Remove(randomoption);
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}