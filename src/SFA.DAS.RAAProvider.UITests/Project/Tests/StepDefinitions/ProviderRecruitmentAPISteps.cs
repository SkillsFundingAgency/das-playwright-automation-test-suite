
using SFA.DAS.RAAProvider.UITests.Project.Helpers;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class ProviderRecruitmentAPISteps(ScenarioContext context)
    {
        private readonly ProviderApiKeyStepsHelper _providerStepsHelper = new(context);

        [Then(@"the Provider views the recruitment API key")]
        public async Task ThenTheProviderViewsTheRecruitmentAPIKey() => await _providerStepsHelper.ViewRecruitmentApiKeyPage();
    }
}
