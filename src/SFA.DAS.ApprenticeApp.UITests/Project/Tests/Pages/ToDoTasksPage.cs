
namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class ToDoTasksPage(ScenarioContext context) : AppBasePage(context)
    {
        private const string ToDoTasksHeader = "h1.govuk-heading-xl";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(ToDoTasksHeader)).ToContainTextAsync("Your tasks");
        }
    }
}
