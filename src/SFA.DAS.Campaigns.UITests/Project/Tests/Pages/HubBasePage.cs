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
                await page.Locator("a.fiu-card__link").Filter(new() { HasText = fiuCardHeading }).ClickAsync();

                objectContext.SetDebugInformation($"Clicked fiu card - '{fiuCardHeading}'");

                var nextPageheading = fiuCardHeading switch
                {
                    var s when s.Contains("Career starter apprenticeships") => "Career starter apprenticeships",
                    var s when s.Contains("Who can do") => "Check who can do apprenticeship training",
                    var s when s.Contains("Explore funding") => "Find funding and support",
                    var s when s.Contains("Your responsibilities") => "Check what you’re responsible for",
                    var s when s.Contains("See what’s worked") => "See what other employers have to say",
                    var s when s.Contains("Plan your apprentice") => "Plan what's next for your apprentice",
                    _ => fiuCardHeading
                };

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
