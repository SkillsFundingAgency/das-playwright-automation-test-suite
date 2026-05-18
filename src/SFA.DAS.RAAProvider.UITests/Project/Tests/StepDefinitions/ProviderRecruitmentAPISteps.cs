
using SFA.DAS.RAAProvider.UITests.Project.Helpers;
using SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;
using System;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class ProviderRecruitmentAPISteps(ScenarioContext context)
    {
        private readonly ProviderApiKeyStepsHelper _providerStepsHelper = new(context);
        private KeyforApiPage _keyforAPIPage = new(context);
        private ApiListPage _apiListPage = new(context);
        private ApprenticeshipServiceDevHubPage _apprenticeshipServiceDevHubPage = new(context);
        private DisplayAdvertAPIPage _displayAdvertAPIPage = new(context);
        private RecruitmentAPIPage _recruitmentAPIPage = new(context);

        [Then(@"the Provider views the recruitment API key")]
        public async Task ThenTheProviderViewsTheRecruitmentAPIKey() => await _providerStepsHelper.ViewRecruitmentApiKeyPage();

        [Given(@"the Provider renews the employer recruitment API key")]
        public async Task GivenTheProviderRenewsTheEmployerRecruitmentAPIKey()
        {
            await _providerStepsHelper.ViewRecruitmentApiKeyPage();

            var page = await _keyforAPIPage.ClickRenewKeyLink();

            var page1 = await page.RenewAPIKey();

            await page1.VerifyApikeyRenewed();
        }

        [Given(@"the Provider renews the employer recruitment API sandbox key")]
        public async Task GivenTheProviderRenewsTheEmployerRecruitmentAPISandboxKey()
        {
            await _providerStepsHelper.ViewRecruitmentApiSandboxKeyPage();

            var page = await _keyforAPIPage.ClickRenewKeyLink();

            var page1 = await page.RenewAPIKey();

            await page1.VerifyApikeyRenewed();
        }

        [Given(@"the Provider renews the employer display API key")]
        public async Task GivenTheProviderRenewsTheEmployerDisplayAPIKey()
        {
            await _providerStepsHelper.ViewDisplayApiKeyPage();

            var page = await _keyforAPIPage.ClickRenewKeyLink();

            var page1 = await page.RenewAPIKey();

            await page1.VerifyApikeyRenewed();
        }

        [Given(@"^the provider selects the developer get started page$")]
        public async Task GivenTheEmployerSelectsTheDeveloperGetStartedPage()
        {
            var page = new RecruitmentHomePage(context);

            await page.VerifyPage();

            var page1 = await page.NavigateToRecruitmentAPIs();

            _apprenticeshipServiceDevHubPage = await page1.ClickDeveloperGetStartedPageLink();
        }

        [When(@"^the provider selects '(.*)' link$")]
        public async Task WhenTheProviderSelectsLink(string linkName)
        {
            switch (linkName)
            {
                case "Display Advert API":
                    _displayAdvertAPIPage = await _apprenticeshipServiceDevHubPage.ClickDisplayAdvertApiLink();
                    break;
                case "Recruitment API":
                    _recruitmentAPIPage = await _apprenticeshipServiceDevHubPage.ClickRecruitmentApiLink();
                    break;
                default:
                    throw new ArgumentException($"Unknown link name: {linkName}");
            }
        }

        [Then(@"^the provider can view the '(.*)' page$")]
        public async Task ThenTheProviderCanViewThePage(string pageName)
        {
            switch (pageName)
            {
                case "Display advert API":
                    await _displayAdvertAPIPage.VerifyEndpointTitles();
                    break;
                case "Recruitment API":
                    await _recruitmentAPIPage.VerifyEndpointTitles();
                    break;
                case "API list":
                    var page = new ApiListPage(context);
                    await page.VerifyPage();
                    await page.VerifyDisplayAdvertApiText();
                    break;
                default:
                    throw new ArgumentException($"Unknown page name: {pageName}");
            }
        }

        [When(@"^the provider signs in to dev hub$")]
        public async Task WhenTheProviderSignsInToDevHub()
        {
            var page = await _apprenticeshipServiceDevHubPage.ClickDevHubSignInLink();

            await page.SignIn();
        }
    }
}
