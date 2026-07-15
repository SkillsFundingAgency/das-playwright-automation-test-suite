
namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class SettingsPage(ScenarioContext context) : AppBasePage(context)
    {
        private const string SettingsHeader = "h1.govuk-heading-xl";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(SettingsHeader)).ToContainTextAsync("Settings");
        }

        public async Task<string> SettingsPageTitleAsync()
            => await page.Locator(SettingsHeader).InnerTextAsync();
    }
}
