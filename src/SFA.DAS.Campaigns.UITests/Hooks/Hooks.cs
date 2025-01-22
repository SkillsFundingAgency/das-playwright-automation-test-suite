namespace SFA.DAS.Campaigns.UITests.Hooks
{
    [Binding]
    public class Hooks(ScenarioContext context)
    {
        [BeforeScenario(Order = 30)]
        public async Task SetUpHelpers()
        {
            var driver = context.Get<Driver>();

            context.Set(new CampaignsDataHelper());

            await driver.Page.GotoAsync(UrlConfig.CA_BaseUrl);
        }
    }
}