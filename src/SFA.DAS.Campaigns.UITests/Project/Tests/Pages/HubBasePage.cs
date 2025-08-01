namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

public abstract class HubBasePage(ScenarioContext context) : CampaingnsHeaderBasePage(context)
{
    protected async Task<T> VerifyFiuCards<T>(Func<Task<T>> func)
    {
        List<Exception> exceptions = [];

        T result = default;

        var fiuCardsHeading = await GetFiuCardsHeadings();

        foreach (var fiuCardHeading in fiuCardsHeading)
        {
            try
            {
                await page.Locator($"a.fiu-card__link:has-text('{fiuCardHeading}')").ClickAsync();

                objectContext.SetDebugInformation($"Clicked fiu card - '{fiuCardHeading}'");

                var nextPageheading = fiuCardHeading.Contains("Career starter apprenticeships") ? "Career starter apprenticeships" : fiuCardHeading;

                var campaingnsDynamicFiuPage = new CampaingnsDynamicFiuPage(context, nextPageheading);

                await campaingnsDynamicFiuPage.VerifyPageAsync();

                objectContext.SetDebugInformation($"Should be loaded - '{nextPageheading}' ");
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
            finally
            {
                result = await func.Invoke();
            }
        }

        if (exceptions.Count > 0) throw new Exception(exceptions.ExceptionToString());

        return result;
    }

    private async Task<IReadOnlyList<string>> GetFiuCardsHeadings()
    {
        return await page.Locator("a.fiu-card__link").AllTextContentsAsync();
    }
}
