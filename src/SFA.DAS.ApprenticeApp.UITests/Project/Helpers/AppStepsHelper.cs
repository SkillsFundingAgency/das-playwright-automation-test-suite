using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;
using SFA.DAS.Framework.Hooks;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Helpers
{
    public class ApprenticeAppUser
    {
        public string Username { get; set; }
    }

    public class AppStepsHelper(ScenarioContext context) : FrameworkBaseHooks(context)
    {
        private const string TasksNavLink = "a.govuk-service-navigation__link[href='/Tasks/Index']";
        private const string ToDoTabNavLink = "a.app-tabs__tab.todo[href='#tasks-todo'][role='tab']";
        private const string DoneTabNavLink = "a.app-tabs__tab.done[href='#tasks-done'][role='tab']";
        private const string KsbNavLink = "nav.govuk-service-navigation a[href='/Ksb/Index']";
        private const string SkipTourNavLink = ".app-onboarding__screen:not([hidden]) .app-onboarding__skip, a.app-onboarding__skip";
        private const string SupportNavLink = "a[href='/Support/Index']";
        private const string NotificationsNavLink = "a[href='/Notifications/Index']";
        private const string AccountNavLink = "a[href*='/Account/YourAccount']";
        private const string YourProfileLink = "a.app-stack__link[href='/Profile/Index']";
        private const string SettingsLink = "a.app-stack__link[href='/Settings/Index']";

        private IPage Page => context.Get<Driver>().Page;

        public async Task GoToHomePageAsync() => await new CookiePage(context).AccessHomePageAsync();

        public async Task<StubSignInPage> GoToStubSignInAsync() => await new HomePage(context).AppSignInAsync();

        public async Task SignInViaStubAsync()
        {
            await GoToStubSignInAsync();

            var activeUser = context.Get<ApprenticeUser>();

            await new StubSignInPage(context).SignInAsync(activeUser.IdOrUserRef, activeUser.Username);

            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task HandleOnboardingTourIfPresentAsync()
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            try
            {
                var skipTourButton = Page.Locator(SkipTourNavLink).First;

                await skipTourButton.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 3_000
                });

                await skipTourButton.ClickAsync();
                Console.WriteLine("Tour overlay successfully bypassed via centralized locator.");

                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            }
            catch (Exception ex) when (ex is PlaywrightException || ex is TimeoutException)
            {
                Console.WriteLine("Skip tour button not visible or removed from app journey. Continuing test execution...");
            }
        }

        public async Task VerifyOnKsbsTabAsync()
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            string currentUrl = Page.Url;
            Console.WriteLine($"Current active browser URL is: {currentUrl}");

            await HandleOnboardingTourIfPresentAsync();

            if (!Page.Url.Contains("/Ksb/Index") && !Page.Url.EndsWith("/Ksb"))
            {
                throw new Exception($"Verification Failed: Expected to land on the KSB view, but stranded on layout: {Page.Url}");
            }

            var ksbPage = new KsbPage(context);
            await ksbPage.VerifyPage();
        }
        public async Task<TasksBasePage> NavigateToTasksPageAsync()
        {
            await Page.Locator(TasksNavLink).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15_000
            });
            await Page.Locator(TasksNavLink).ClickAsync();

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
            if (Page.Url.Contains("/Ksb/Index") || Page.Url.EndsWith("/Ksb"))
            {
                Console.WriteLine("Already present on the KSB layout. Skipping navigation click interaction.");
                return new KsbPage(context);
            }

            await Page.Locator(KsbNavLink).ClickAsync();

            await Page.WaitForURLAsync(url => url.Contains("/Ksb/Index") || url.EndsWith("/Ksb"),
                new PageWaitForURLOptions { Timeout = 15_000 });

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