using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using Reqnroll;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Hooks
{
    [Binding]
    public class CmadBaseHooks(ScenarioContext context)
    {
        [BeforeScenario(Order = 10)]
        public async Task SetUpCmadScenarioContext()
        {
            var configSection = context.Get<ConfigSection>();
            var appUserConfig = configSection.GetConfigSection<ApprenticeAppUser>();
            var cmadUser = new AppCMADUser
            {
                Username = appUserConfig.Username,
                IdOrUserRef = appUserConfig.IdOrUserRef
            };

            var userList = new List<ApprenticeUser> { cmadUser };
            await context.SetApprenticeAccountsPortalUser(userList);

            if (!context.ContainsKey(typeof(ApprenticeUser).FullName))
            {
                context.Set<ApprenticeUser>(cmadUser);
            }

            if (!context.ContainsKey(typeof(AppCMADUser).FullName))
            {
                context.Set<AppCMADUser>(cmadUser);
            }
            var objectContext = context.Get<ObjectContext>();
            objectContext.SetConsoleAndDebugInformation($"CMAD BeforeScenario setup complete. Account context verified for user: {cmadUser.Username}");
        }
    }
}