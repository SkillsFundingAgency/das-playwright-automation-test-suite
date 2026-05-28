using Reqnroll;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class WelcomePage(ScenarioContext context) : AppBasePage(context)
    {
        private const string SkipTourLink = ".app-onboarding__screen:not([hidden]) .app-onboarding__skip";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(SkipTourLink)).ToBeVisibleAsync();
        }

        public async Task<TasksBasePage> StartNowAsync()
        {
            await page.Locator(SkipTourLink).ClickAsync();

            await page.WaitForURLAsync(url => url.Contains("/Ksb/Index"),
                new PageWaitForURLOptions { Timeout = 10_000 });

            await page.GotoAsync(page.Url.Replace("/Ksb/Index", "/Tasks/Index"));

            return await VerifyPageAsync(() => new TasksBasePage(context));
        }
    }
}