using Microsoft.Playwright;
using SFA.DAS.Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages
{
    public class CampaingnsHomePage(ScenarioContext context) : CampaingnsBasePage(context)
    {
        protected override async Task VerifyPage() => await Assertions.Expect(context.Get<Driver>().Page.GetByRole(AriaRole.Link, new() { Name = "Apprentices", Exact = true })).ToBeVisibleAsync();

        public async Task AcceptCookies()
        {
            await driver.ClickAsync((p) => p.GetByRole(AriaRole.Button, new() { Name = "Accept all cookies" }).ClickAsync(), "Accept cookie");

            await driver.ClickAsync((p) => p.Locator("#fiu-cb-close-accept").ClickAsync(), "close cookie button");
        }

        public async Task<ApprenticeHomePage> GoToApprenticePage()
        {
            await driver.SubmitAsync((p) => p.GetByRole(AriaRole.Link, new() { Name = "Learn more becoming an" }).ClickAsync(), "learn more to be an apprentice");

            return new ApprenticeHomePage(context);
        }
    }
}
