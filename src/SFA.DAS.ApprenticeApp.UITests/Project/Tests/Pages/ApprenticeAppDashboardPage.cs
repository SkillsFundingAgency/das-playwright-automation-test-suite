using Reqnroll;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class ApprenticeAppDashboardPage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToHaveTextAsync("Welcome to the Your Apprenticeship app");
        }
    }
}
