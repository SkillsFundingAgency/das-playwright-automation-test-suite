using Microsoft.Playwright;
using SFA.DAS.Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Campaigns.UITests.Hooks
{
    [Binding]
    public class Hooks(ScenarioContext context)
    {
        [BeforeScenario(Order = 30)]
        public async Task SetUpHelpers()
        {
            var driver = context.Get<Driver>();

            await driver.GotoAsync(UrlConfig.EmployerApprenticeshipService_BaseUrl);

            await driver.ClickAsync((p) => p.GetByRole(AriaRole.Button, new() { Name = "Create account" }).ClickAsync(), "Eas Landing");

            await driver.FillAsync((p) => p.GetByLabel("ID"), "582d8508-f17d-4bef-aec1-6a9751fccea5");

            await driver.FillAsync((p) => p.GetByLabel("Email"), "Viewer@l38cxwya.mailosaur.net");

            await driver.SubmitAsync((p) => p.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync(), "Eas Sign in");

            await driver.GotoAsync(UrlConfig.CA_BaseUrl);
        }

        [AfterScenario(Order = 30)]
        public async Task Screenshot()
        {
            var driver = context.Get<Driver>();

            await driver.ScreenshotAsync(true);
        }
    }
}