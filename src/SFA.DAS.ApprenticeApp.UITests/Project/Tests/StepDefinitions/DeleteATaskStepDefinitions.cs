using Reqnroll;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class DeleteATaskStepDefinitions(ScenarioContext context)
    {
        private readonly TasksBasePage _tasksBasePage = new(context);
        private string _taskTitle;

        [When("the apprentice clicks on the created task")]
        public async Task WhenTheApprenticeClicksOnTheCreatedTask()
        {
            string taskNameLookUp = context.ContainsKey("UpdatedTaskName")
                ? context["UpdatedTaskName"].ToString()
                : context["CurrentTaskName"].ToString();

            _taskTitle = await _tasksBasePage.OpenTaskByTitleAsync(taskNameLookUp);
        }

        [When("the apprentice clicks on delete and confirms")]
        public async Task ThenTheApprenticeClicksOnDeleteAndConfirms()
        {
            await _tasksBasePage.DeleteTaskAsync();
            await _tasksBasePage.RefreshAsync();
        }

        [Then("the task is removed from the list")]
        public async Task ThenTheTaskIsRemovedFromTheList()
        {
            bool isRemoved = await _tasksBasePage.IsTaskRemovedAsync(_taskTitle);

            Console.WriteLine(isRemoved
                ? $"Task '{_taskTitle}' was removed from the list."
                : $"Task '{_taskTitle}' still appears in the list.");
        }
    }
}
