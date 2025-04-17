

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class SectionsPage(ScenarioContext context) : DfeAdminStartPage(context)
    {
        private ILocator StartButton => page.GetByText("Start now");


        public override async Task VerifyPage() => await Task.CompletedTask;


    }
}
