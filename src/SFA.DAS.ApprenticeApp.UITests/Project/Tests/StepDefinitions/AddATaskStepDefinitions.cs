using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class AddATaskStepDefinitions(ScenarioContext context)
    {
        private readonly TasksBasePage _tasksBasePage = new(context);
        private readonly AppStepsHelper _appStepsHelper = new(context);

        private string _toDoTaskName;
        private string _doneTaskName;

        [When("the apprentice adds a new to do task")]
        public async Task WhenTheApprenticeAddsANewTask()
        {
            await _appStepsHelper.NavigateToTasksPageAsync();
            await _tasksBasePage.RefreshAsync();

            _toDoTaskName = TasksBasePage.GenerateTaskName();
            context["CurrentTaskName"] = _toDoTaskName;

            await _tasksBasePage.AddTaskAsync(
                isToDo: true,
                title: _toDoTaskName,
                date: DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy"),
                time: "12:00",
                ksb: "KSB",
                ksbId: "1",
                categoryValue: "Assignment",
                status: "Status",
                note: "Note");

            await _tasksBasePage.RefreshAsync();
        }

        [When("the apprentice has clicked on the done tasks tab")]
        public async Task WhenTheApprenticeUserIsOnTheDoneTasksPage()
        {
            await _appStepsHelper.NavigateToTasksPageAsync();
            await _tasksBasePage.RefreshAsync();
            await _tasksBasePage.ClickDoneTabAsync();
        }

        [When("the apprentice adds a new done task")]
        public async Task WhenTheApprenticeAddsANewDoneTask()
        {
            await _appStepsHelper.NavigateToTasksPageAsync();
            await _tasksBasePage.RefreshAsync();
            await _appStepsHelper.NavigateToDoneTabAsync();

            _doneTaskName = TasksBasePage.GenerateTaskName();
            context["CurrentTaskName"] = _doneTaskName;

            await _tasksBasePage.AddTaskAsync(
                isToDo: false,
                title: _doneTaskName,
                date: DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy"),
                time: "12:00",
                ksb: "KSB",
                ksbId: "1",
                categoryValue: "Assignment",
                status: "Status",
                note: "Note");

            await _tasksBasePage.RefreshAsync();
        }

        [Then("the task is added to the to do tasks list")]
        public async Task ThenTheTaskIsAddedToTheTaskList()
        {
            Assert.IsTrue(
                await _tasksBasePage.IsTaskAddedAsync(_toDoTaskName),
                "Task was not added successfully.");
        }

        [Then("the task is added to the done tasks list")]
        public async Task ThenTheTaskIsAddedToTheDoneTasksList()
        {
            Assert.IsTrue(
                await _tasksBasePage.IsTaskAddedAsync(_doneTaskName),
                "The task was not added to the done tasks list.");
        }
    }
}
