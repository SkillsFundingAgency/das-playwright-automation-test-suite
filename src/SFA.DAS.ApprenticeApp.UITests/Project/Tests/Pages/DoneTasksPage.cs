using Reqnroll;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class DoneTasksPage(ScenarioContext context) : AppBasePage(context)
    {
        private const string DoneTasksHeader = "h1.govuk-heading-xl";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(DoneTasksHeader)).ToContainTextAsync("Your tasks");
        }
    }
}
