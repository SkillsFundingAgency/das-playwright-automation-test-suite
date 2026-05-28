using Reqnroll;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;
using SFA.DAS.Framework;
using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Helpers
{
    /// <summary>
    /// Playwright replacement for the Selenium AppStepsHelper.
    /// Extends FrameworkBaseHooks to get access to Navigate() and the Driver.
    /// Navigation methods now return Tasks and use async/await throughout.
    /// </summary>
    public class AppStepsHelper(ScenarioContext context) : FrameworkBaseHooks(context)
    {
        // Selectors - CSS selectors are identical to the Selenium version
        private const string TasksNavLink      = "a.govuk-service-navigation__link[href='/Tasks/Index']";
        private const string ToDoTabNavLink    = "a.app-tabs__tab.todo[href='#tasks-todo'][role='tab']";
        private const string DoneTabNavLink    = "a.app-tabs__tab.done[href='#tasks-done'][role='tab']";
        private const string KsbNavLink        = "a[href='/Ksb/Index']";
        private const string SupportNavLink    = "a[href='/Support/Index']";
        private const string NotificationsNavLink = "a[href='/Notifications/Index']";
        private const string AccountNavLink    = "a[href*='/Account/YourAccount']";
        private const string YourProfileLink   = "a.app-stack__link[href='/Profile/Index']";
        private const string SettingsLink      = "a.app-stack__link[href='/Settings/Index']";

        private IPage Page => context.Get<Driver>().Page;

        // ── Login flow ────────────────────────────────────────────────────────

        public async Task GoToHomePageAsync()
            => await new CookiePage(context).AccessHomePageAsync();

        public async Task<StubSignInPage> GoToStubSignInAsync()
            => await new HomePage(context).AppSignInAsync();

        public async Task<WelcomePage> GoToWelcomePageAsync()
        {
            var appUser = context.Get<ConfigSection>().GetConfigSection<ApprenticeAppUserConfig>();
            return await new StubSignInPage(context).SignInAsync(appUser.Username);
        }

        public async Task<TasksBasePage> GoToTasksPageAsync()
            => await new WelcomePage(context).StartNowAsync();

        // ── Navigation helpers ────────────────────────────────────────────────

        public async Task<TasksBasePage> NavigateToTasksPageAsync()
        {
            await Page.Locator(TasksNavLink).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15_000
            });
            await Page.Locator(TasksNavLink).ClickAsync();

            // Playwright WaitForURL replaces the manual Thread.Sleep polling loop
            await Page.WaitForURLAsync(
                url => url.Contains("/Tasks/Index") || url.EndsWith("/Tasks"),
                new PageWaitForURLOptions { Timeout = 15_000 });

            return new TasksBasePage(context);
        }

        public async Task<TasksBasePage> NavigateToToDoTabAsync()
        {
            await Page.Locator(ToDoTabNavLink).ClickAsync();
            return new TasksBasePage(context);
        }

        public async Task<DoneTasksPage> NavigateToDoneTabAsync()
        {
            await Page.Locator(DoneTabNavLink).ClickAsync();
            return new DoneTasksPage(context);
        }

        public async Task<KsbPage> NavigateToKsbPageAsync()
        {
            await Page.Locator(KsbNavLink).ClickAsync();
            return new KsbPage(context);
        }

        public async Task<SupportGuidancePage> NavigateToSupportGuidancePageAsync()
        {
            await Page.Locator(SupportNavLink).ClickAsync();
            return new SupportGuidancePage(context);
        }

        public async Task<NotificationsPage> NavigateToNotificationsPageAsync()
        {
            await Page.Locator(NotificationsNavLink).ClickAsync();
            return new NotificationsPage(context);
        }

        public async Task<AccountPage> NavigateToAccountPageAsync()
        {
            await Page.Locator(AccountNavLink).ClickAsync();
            return new AccountPage(context);
        }

        public async Task<YourProfilePage> NavigateToYourProfilePageAsync()
        {
            await Page.Locator(YourProfileLink).ClickAsync();
            return new YourProfilePage(context);
        }

        public async Task<SettingsPage> NavigateToSettingsPageAsync()
        {
            await Page.Locator(SettingsLink).ClickAsync();
            return new SettingsPage(context);
        }
    }
}
