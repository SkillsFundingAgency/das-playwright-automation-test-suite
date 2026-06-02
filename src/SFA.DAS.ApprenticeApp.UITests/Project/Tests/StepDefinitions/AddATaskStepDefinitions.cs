using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;
using Reqnroll;
using NUnit.Framework;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class AddATaskStepDefinitions(ScenarioContext context)
    {
        private readonly TasksBasePage _tasksBasePage = new(context);
        private readonly AppStepsHelper _appStepsHelper = new(context);

        private string _toDoTaskName;
        private string _doneTaskName;

        [When("the apprentice adds, edits, and then deletes a to do task")]
        public async Task WhenTheApprenticeAddsEditsAndDeletesAToDoTask()
        {
            // 1. ADD PHASE
            await _appStepsHelper.NavigateToTasksPageAsync();
            await _tasksBasePage.RefreshAsync();

            _toDoTaskName = _tasksBasePage.GenerateTaskName();
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

            // 2. EDIT PHASE (Passing down tracked context string value)
            await _tasksBasePage.ClickOnTaskAsync(_toDoTaskName);
            await _tasksBasePage.EditTaskAndConfirmAsync(_toDoTaskName);
            await _tasksBasePage.RefreshAsync();

            // 3. DELETE PHASE
            string updatedName = context["UpdatedTaskName"].ToString();
            await _tasksBasePage.ClickOnTaskAsync(updatedName);
            await _tasksBasePage.DeleteTaskAndConfirmAsync();
            await _tasksBasePage.RefreshAsync();
        }

        [When("the apprentice has clicked on the done tasks tab")]
        public async Task WhenTheApprenticeUserIsOnTheDoneTasksPage()
        {
            await _appStepsHelper.NavigateToTasksPageAsync();
            await _tasksBasePage.RefreshAsync();
            await _tasksBasePage.ClickDoneTabAsync();
        }

        [When("the apprentice adds, edits, and then deletes a done task")]
        public async Task WhenTheApprenticeAddsEditsAndDeletesADoneTask()
        {
            // 1. ADD PHASE
            await _appStepsHelper.NavigateToTasksPageAsync();
            await _tasksBasePage.RefreshAsync();
            await _appStepsHelper.NavigateToDoneTabAsync();

            _doneTaskName = _tasksBasePage.GenerateTaskName();
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

            // 2. EDIT PHASE
            await _tasksBasePage.ClickOnTaskAsync(_doneTaskName);
            await _tasksBasePage.EditTaskAndConfirmAsync(_doneTaskName);
            await _tasksBasePage.RefreshAsync();

            // 3. DELETE PHASE
            string updatedName = context["UpdatedTaskName"].ToString();
            await _tasksBasePage.ClickOnTaskAsync(updatedName);
            await _tasksBasePage.DeleteTaskAndConfirmAsync();
            await _tasksBasePage.RefreshAsync();
        }

        [Then("the task is completely removed from the list")]
        public async Task ThenTheTaskIsCompletelyRemovedFromTheList()
        {
            string finalTargetTaskName = context.ContainsKey("UpdatedTaskName")
                ? context["UpdatedTaskName"].ToString()
                : (_toDoTaskName ?? _doneTaskName);

            bool isPresent = await _tasksBasePage.IsTaskAddedAsync(finalTargetTaskName);

            Assert.IsFalse(
                isPresent,
                $"Housekeeping Verification Failed: Task layout item '{finalTargetTaskName}' is still visible inside the UI viewport!");
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