using Azure;
using Microsoft.Playwright;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages;
using SFA.DAS.Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class SignUpSteps(ScenarioContext context)
    {
        [Given(@"the employer navigates to Sign Up Page")]
        public async Task GivenTheEmployerNavigatesToSignUpPage()
        {
            var page = new CampaingnsHomePage(context);

            await page.AcceptCookies();

            var page2 = await page.GoToApprenticePage();

            var driver = context.Get<Driver>();

            await driver.GotoAsync($"{UrlConfig.CA_BaseUrl}/employers/understanding-apprenticeship-benefits-and-funding");

            await driver.FillAsync((p) => p.GetByLabel("What training course do you"), "soft");

            await driver.ClickAsync((p) => p.GetByRole(AriaRole.Option, new() { Name = "Software developer (Level 4)" }).ClickAsync(), "software developer");

            //await driver.FillAsync((p) => p.GetByLabel("How many roles do you have"), "2");

            await driver.Page.GetByLabel("Under £3 million").CheckAsync();

            await driver.Page.GetByLabel("How many roles do you have").FillAsync("2");

            await driver.Page.GetByRole(AriaRole.Button, new() { Name = "Calculate funding" }).ClickAsync();

            await Assertions.Expect(driver.Page.Locator("#funding")).ToContainTextAsync("Your estimated funding");




        }
    }
}
