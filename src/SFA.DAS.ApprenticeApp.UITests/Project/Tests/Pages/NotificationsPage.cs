
namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class NotificationsPage(ScenarioContext context) : AppBasePage(context)
    {
        private const string NotificationsHeader = "h1.govuk-heading-xl";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(NotificationsHeader)).ToContainTextAsync("Notifications");
        }

        public async Task<string> NotificationsPageTitleAsync()
            => await page.Locator(NotificationsHeader).InnerTextAsync();
    }
}
