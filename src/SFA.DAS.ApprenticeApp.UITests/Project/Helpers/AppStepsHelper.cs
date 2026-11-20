using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;
using SFA.DAS.Framework.Hooks;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Helpers
{
    public class AppStepsHelper(ScenarioContext context) : FrameworkBaseHooks(context)
    {
        #region Constants & Locators

        private const string NotesNavLink = "a[href='/Tasks/Index']";
        private const string SkipTourNavLink = ".app-onboarding__screen:not([hidden]) .app-onboarding__skip, a.app-onboarding__skip";
        private const string HomeNavLink = "a.govuk-service-navigation__link[href='/Support/Index']";
        private const string AccountNavLink = "a[href*='/Account/YourAccount']";
        private const string AccountNotificationsLink = "a[href*='/Notifications']";
        private const string AccountSettingsLink = "a[href*='/Settings']";
        private const string AccountDetailsLink = "a:has-text('Your apprenticeship details')";

        #endregion

        #region Driver Property

        private IPage Page => context.Get<Driver>().Page;

        #endregion

        #region Authentication & Setup

        public async Task GoToHomePageAsync() => await new CookiePage(context).AccessHomePageAsync();

        public async Task<StubSignInPage> GoToStubSignInAsync() => await new HomePage(context).AppSignInAsync();

        public async Task SignInViaStubAsync()
        {
            await GoToStubSignInAsync();

            var activeUser = context.Get<ApprenticeAppUser>();

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

        #endregion

        #region Home Navigation

        public async Task VerifyOnHomeTabAsync()
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            string currentUrl = Page.Url;
            Console.WriteLine($"Current active browser URL is: {currentUrl}");

            await HandleOnboardingTourIfPresentAsync();

            if (!Page.Url.Contains("/Support/Index") && !Page.Url.EndsWith("/Support"))
            {
                throw new Exception($"Verification Failed: Expected to land on the Home view, but stranded on layout: {Page.Url}");
            }

            var homePage = new ApprenticeHomePage(context);
            await homePage.VerifyPage();
        }

        public async Task<ApprenticeHomePage> NavigateToHomeTabAsync()
        {
            if (Page.Url.Contains("/Support/Index") || Page.Url.EndsWith("/Support"))
            {
                Console.WriteLine("Already present on the Home layout. Skipping navigation click interaction.");
                return new ApprenticeHomePage(context);
            }

            await Page.Locator(HomeNavLink).ClickAsync();

            //await Page.WaitForURLAsync(url => url.Contains("/Support/Index") || url.EndsWith("/Support"),
            //    new PageWaitForURLOptions { Timeout = 15_000 });

            return new ApprenticeHomePage(context);
        }

        #endregion

        #region Notes & KSB Navigation

        public async Task<NotesBasePage> NavigateToNotesPageAsync()
        {
            var navLink = Page.Locator(NotesNavLink).First;
            await navLink.ClickAsync();
            return new NotesBasePage(context);
        }

        public bool IsOnKsbPage()
        {
            return Page.Url.Contains("/Ksb/Index") || Page.Url.EndsWith("/Ksb");
        }

        public async Task<KsbPage> NavigateToKsbPageAsync()
        {
            var ksbNavLink = Page.Locator("a[href='/Ksb/Index'], a:has-text('Your KSBs')").First;
            await ksbNavLink.ClickAsync();
            return new KsbPage(context);
        }

        public async Task ClickKsbSubTabAsync(string tabName)
        {
            var tabSelector = tabName switch
            {
                "Knowledge" => "a.tab-knowledge",
                "Skills" => "a.tab-skills",
                "Behaviours" => "a.tab-behaviours",
                _ => throw new ArgumentException($"Unknown KSB tab: {tabName}")
            };

            var tabLink = Page.Locator(tabSelector).First;
            await tabLink.ClickAsync();
        }

        public async Task<bool> IsKsbPanelVisibleAsync(string tabName)
        {
            var panelId = tabName switch
            {
                "Knowledge" => "#knowledge",
                "Skills" => "#skill",
                "Behaviours" => "#behaviour",
                _ => throw new ArgumentException($"Unknown KSB tab panel: {tabName}")
            };

            var panel = Page.Locator(panelId).First;
            return await panel.IsVisibleAsync();
        }

        #endregion

        #region Account & Sub-Menu Navigation

        public async Task<AccountPage> NavigateToAccountPageAsync()
        {
            await Page.Locator(AccountNavLink).ClickAsync();
            return new AccountPage(context);
        }

        public async Task<YourApprenticeshipDetailsPage> NavigateToYourApprenticeshipDetailsPageAsync()
        {
            await NavigateToAccountPageAsync();
            await Page.Locator(AccountDetailsLink).First.ClickAsync();
            return new YourApprenticeshipDetailsPage(context);
        }

        public async Task<NotificationsPage> NavigateToNotificationsPageAsync()
        {
            await NavigateToAccountPageAsync();
            await Page.Locator(AccountNotificationsLink).First.ClickAsync();
            return new NotificationsPage(context);
        }

        public async Task<SettingsPage> NavigateToSettingsPageAsync()
        {
            await NavigateToAccountPageAsync();
            await Page.Locator(AccountSettingsLink).First.ClickAsync();
            return new SettingsPage(context);
        }

        #endregion
    }
}