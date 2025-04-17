

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class PagesPage(ScenarioContext context) : DfeAdminStartPage(context)
    {
        private ILocator StartButton => page.GetByText("Start now");



    }
}
