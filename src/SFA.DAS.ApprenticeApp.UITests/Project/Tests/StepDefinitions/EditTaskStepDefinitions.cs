using Reqnroll;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class EditTaskStepDefinitions(ScenarioContext context)
    {
        private readonly TasksBasePage _tasksBasePage = new(context);

        [When("the apprentice clicks on edit task, edits and confirms")]
        public async Task WhenTheApprenticeClicksOnEditTaskEditsAndConfirms()
        {
            string updatedName = TasksBasePage.GenerateTaskName();

            await _tasksBasePage.SetTaskTitleAsync(updatedName);

            context["UpdatedTaskName"] = updatedName;

            await _tasksBasePage.ClickSaveAndContinueAsync();
        }
    }
}
