using Microsoft.Playwright;
using NUnit.Framework;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using SFA.DAS.ConfigurationBuilder;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class SignUpSteps(ScenarioContext context)
    {
        [Given("the trace is started")]
        public async Task GivenTheTraceIsStarted()
        {
            var driver = context.Get<Driver>();

            await driver.BrowserContext.Tracing.StartAsync(new()
            {
                Title = context.ScenarioInfo.Title,
                Screenshots = true,
                Snapshots = true
            });
        }


        [Given("the trace is stopped")]
        public async Task GivenTheTraceIsStopped()
        {
            var driver = context.Get<Driver>();

            var tracefileName = $"TRACEDATA_{DateTime.Now:HH-mm-ss-fffff}.zip";

            var tracefilePath = $"{context.Get<ObjectContext>().GetDirectory()}/{tracefileName}";

            await driver.BrowserContext.Tracing.StopAsync(new()
            {
                Path = tracefilePath
            });

            TestContext.AddTestAttachment(tracefilePath, tracefileName);
        }

        [Given(@"the employer navigates to Sign Up Page")]
        public async Task GivenTheEmployerNavigatesToSignUpPage()
        {
            var page = new CampaingnsHomePage(context);

            await page.AcceptCookieAndAlert();

            await page.GoToApprenticePage();

            var driver = context.Get<Driver>();

            await driver.Page.GotoAsync($"{UrlConfig.CA_BaseUrl}/employers/understanding-apprenticeship-benefits-and-funding");

            await driver.Page.GetByLabel("What training course do you").FillAsync("soft");

            await driver.Page.GetByRole(AriaRole.Option, new() { Name = "Software developer (Level 4)" }).ClickAsync();

            await driver.Page.GetByLabel("Under £3 million").CheckAsync();

            await driver.Page.GetByLabel("How many roles do you have").FillAsync("2");

            await driver.Page.GetByRole(AriaRole.Button, new() { Name = "Calculate funding" }).ClickAsync();

            await Assertions.Expect(driver.Page.Locator("#funding")).ToContainTextAsync("Your estimated funding");
        }
    }
}
