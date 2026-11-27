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
        private const string PushNotificationPrompt = "#push-notification-prompt.app-modal--visible";
        private const string PushNotificationContinueButton = "#push-notification-prompt.app-modal--visible button[data-modal-close], #push-notification-prompt.app-modal--visible button:has-text('Continue')";
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

        public async Task SeedPushNotificationCookieAsync()
        {
            var appUri = new Uri(UrlConfig.ApprenticeApp_BaseUrl);

            await context.Get<Driver>().BrowserContext.AddCookiesAsync(
            [
                new Cookie
                {
                    Name = "SFA.ApprenticeApp.PushNotificationPrompt",
                    Value = "true",
                    Domain = appUri.Host,
                    Path = "/",
                    Secure = true,
                    SameSite = SameSiteAttribute.Lax
                }
            ]);
        }

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
            var skipTourButton = Page.Locator(SkipTourNavLink).First;

            try
            {
                await skipTourButton.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 3_000
                });
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Skip tour button not visible. Continuing test execution...");
                return;
            }

            await skipTourButton.ClickAsync();
            Console.WriteLine("Tour overlay successfully bypassed via centralized locator.");
        }

        public async Task HandlePushNotificationPromptIfPresentAsync()
        {
            var continueButton = Page.Locator(PushNotificationContinueButton).First;

            try
            {
                await continueButton.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 3_000
                });
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Push notification prompt not visible. Continuing test execution...");
                return;
            }

            await continueButton.ClickAsync();
            await Page.Locator(PushNotificationPrompt).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Hidden,
                Timeout = 3_000
            });
            Console.WriteLine("Push notification prompt dismissed via centralized locator.");
        }

        #endregion

        #region Home Navigation

        public async Task VerifyOnHomeTabAsync()
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

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

        public async Task<KsbPage> NavigateToKsbPageAsync()
        {
            var ksbNavLink = Page.Locator("a[href='/Ksb/Index'], a:has-text('Your KSBs')").First;
            await ksbNavLink.ClickAsync();
            return new KsbPage(context);
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