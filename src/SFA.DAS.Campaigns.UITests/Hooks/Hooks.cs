using Microsoft.Playwright;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using TechTalk.SpecFlow;
using SFA.DAS.ConfigurationBuilder;

namespace SFA.DAS.Campaigns.UITests.Hooks
{
    [Binding]
    public class Hooks(ScenarioContext context)
    {
        private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

        [BeforeScenario(Order = 30)]
        public async void SetUpHelpers()
        {
            var page = context.Get<IPage>();

            page.Request += (_, request) => _objectContext.SetDebugInformation("Request sent: " + request.Url);

            void listener(object sender, IRequest request)
            {
                _objectContext.SetDebugInformation("Request finished: " + request.Url);
            };

            page.RequestFinished += listener;


            await page.GotoAsync(UrlConfig.EmployerApprenticeshipService_BaseUrl);

            _objectContext.SetDebugInformation("******");

            await context.Get<IPage>().ScreenshotAsync(new()
            {
                Path = context.Get<ObjectContext>().GetDirectory() + "/1.png",
                FullPage = true,
            });

            await page.GetByRole(AriaRole.Button, new() { Name = "Create account" }).ClickAsync();

            _objectContext.SetDebugInformation("******");

            await page.GetByLabel("ID").FillAsync("das");

            _objectContext.SetDebugInformation("******");

            await page.GetByLabel("Email").FillAsync("as");

            await context.Get<IPage>().ScreenshotAsync(new()
            {
                Path = context.Get<ObjectContext>().GetDirectory() + "/2.png",
                FullPage = true,
            });

            // click login 

            _objectContext.SetDebugInformation("******");

            await page.GotoAsync(UrlConfig.CA_BaseUrl);

        }
    }
}