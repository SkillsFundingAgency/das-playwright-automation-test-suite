using Reqnroll;
using SFA.DAS.Framework;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class CookiePage(ScenarioContext context) : AppBasePage(context)
    {
        private const string AcceptCookiesButton = "input[name='action:AcceptCookies']";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1.govuk-heading-l")).ToBeVisibleAsync();
        }

        public async Task<HomePage> AccessHomePageAsync()
        {
            await Navigate(UrlConfig.ApprenticeApp_BaseUrl);
            await page.Locator(AcceptCookiesButton).ClickAsync();
            return new HomePage(context);
        }
    }
}
