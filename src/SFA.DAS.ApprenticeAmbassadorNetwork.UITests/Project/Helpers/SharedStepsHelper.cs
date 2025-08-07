using System.Collections.Generic;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Models;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Helpers
{
    public class SharedStepsHelper(ScenarioContext context)
    {
        private readonly string SearchResultsKey = "AanAdminStepsHelper.SearchResultsKey";

        public async Task<List<NetworkEventSearchResult>> GetAllSearchResults(SearchEventsBasePage searchEventsPage)
        {
            if (context.ContainsKey(SearchResultsKey))
            {
                return context.Get<List<NetworkEventSearchResult>>(SearchResultsKey);
            }

            await searchEventsPage.VerifyPage();

            var eventTitles = await searchEventsPage.GetSearchResults();

            while (await searchEventsPage.HasNextPage())
            {
                await searchEventsPage.ClickNextPage();

                var titles = await searchEventsPage.GetSearchResults();

                eventTitles.AddRange(titles);
            }

            context.Add(SearchResultsKey, eventTitles);
            return eventTitles;
        }

        public void ClearSearchResultsCache()
        {
            if (context.ContainsKey(SearchResultsKey))
            {
                context.Remove(SearchResultsKey);
            }
        }
    }
}
