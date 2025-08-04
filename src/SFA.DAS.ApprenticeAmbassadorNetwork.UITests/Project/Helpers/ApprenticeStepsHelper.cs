using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Models;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Helpers
{
    public class ApprenticeStepsHelper(ScenarioContext context)
    {
        private readonly SharedStepsHelper _sharedStepsHelper = new(context);

        public async Task<List<NetworkEventSearchResult>> GetAllSearchResults()
        {
            return await _sharedStepsHelper.GetAllSearchResults(new SearchNetworkEventsPage(context));
        }

        public void ClearEventTitleCache()
        {
            _sharedStepsHelper.ClearSearchResultsCache();
        }
    }
}
