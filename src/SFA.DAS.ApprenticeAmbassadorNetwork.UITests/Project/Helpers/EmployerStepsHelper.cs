using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Models;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Helpers
{
    public class EmployerStepsHelper(ScenarioContext context)
    {
        private readonly SharedStepsHelper _sharedStepsHelper = new(context);

        public async Task<List<NetworkEventSearchResult>> GetAllSearchResults()
        {
            var manageEvents = new SearchNetworkEventsPage(context);

            return await _sharedStepsHelper.GetAllSearchResults(manageEvents);
        }

        public void ClearEventTitleCache()
        {
            _sharedStepsHelper.ClearSearchResultsCache();
        }
    }
}
