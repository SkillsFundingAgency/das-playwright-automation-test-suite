using SFA.DAS.FATe.UITests.Project.Tests.Pages;

namespace SFA.DAS.FATe.UITests.Helpers
{
    public class FATeStepsHelper(ScenarioContext context)
    {
        public async Task<FATeHomePage> GoToFATeHomePage() => await new FATeHomePage(context).AcceptCookieAndAlert();

    }
}
