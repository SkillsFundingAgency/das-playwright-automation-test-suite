using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages
{
    public class ApprenticeHomePage(ScenarioContext context) : CampaingnsBasePage(context)
    {
        protected override async Task VerifyPage() => await Assertions.Expect(driver.Page.Locator("h1")).ToContainTextAsync("Become an apprentice");
    }
}
