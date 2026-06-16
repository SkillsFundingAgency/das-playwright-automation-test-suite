using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Hooks
{
    [Binding]
    public class BeforeScenarioHooks(ScenarioContext context)
    {

        [BeforeScenario(Order = 10)]
        public async Task AppSetupHelpers() => await context.SetApprenticeAccountsPortalUser([ context.Get<ConfigSection>().GetConfigSection<ApprenticeAppUser>()]);

        [AfterScenario(Order = 10)]
        public async Task CleanUpTestData()
        {
            try
            {
                var tasksBasePage = new TasksBasePage(context);
                var appStepsHelper = new AppStepsHelper(context);

                await appStepsHelper.NavigateToTasksPageAsync();

                string taskToDelete = null;

                if (context.ContainsKey("UpdatedTaskName"))
                    taskToDelete = context["UpdatedTaskName"].ToString();
                else if (context.ContainsKey("CurrentTaskName"))
                    taskToDelete = context["CurrentTaskName"].ToString();

                if (!string.IsNullOrEmpty(taskToDelete))
                {
                    Console.WriteLine($"[CleanUp] Targeting main scenario task: '{taskToDelete}'");
                    await tasksBasePage.CleanUpTaskByTitleAsync(taskToDelete);
                }

                await tasksBasePage.SweepOrphanedTasksAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CleanUp Warning] Automated sweeper encountered an issue: {ex.Message}");
            }
        }
    }
}