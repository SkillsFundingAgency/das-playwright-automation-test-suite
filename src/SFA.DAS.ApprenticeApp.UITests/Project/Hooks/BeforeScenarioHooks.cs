using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Hooks
{
    [Binding]
    public class BeforeScenarioHooks(ScenarioContext context)
    {
        private class TestApprenticeUser : ApprenticeUser
        {

        }

        [BeforeScenario(Order = 10)]
        public async Task AppSetupHelpers()
        {
            var configSection = context.Get<ConfigSection>();
            var appUserConfig = configSection.GetConfigSection<ApprenticeAppUser>();

            var testUser = new TestApprenticeUser
            {
                Username = appUserConfig.Username
            };

            var userList = new List<ApprenticeUser> { testUser };

            await context.SetApprenticeAccountsPortalUser(userList);

            if (!context.ContainsKey(typeof(ApprenticeUser).FullName) && context.ContainsKey(typeof(TestApprenticeUser).FullName))
            {
                context.Set<ApprenticeUser>(context.Get<TestApprenticeUser>());
            }

            var objectContext = context.Get<ObjectContext>();
            objectContext.SetConsoleAndDebugInformation($"ApprenticeApp BeforeScenario setup complete. Account context verified for user: {appUserConfig.Username}");
        }

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