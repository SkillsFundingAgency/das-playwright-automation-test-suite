
using SFA.DAS.RAAProvider.UITests.Project.Helpers;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class ProviderSteps(ScenarioContext context)
    {
        private readonly ProviderStepHelper _providerStepsHelper = new(context);

        [Then(@"Provider can make the application successful")]
        public async Task ThenProviderCanMakeTheApplicationSuccessful() => await _providerStepsHelper.ApplicantSucessful();

        [Then(@"Provider can make the application unsuccessful")]
        public async Task ThenProviderCanMakeTheApplicationUnSuccessful() => await _providerStepsHelper.ApplicantUnsucessful();

        [Then(@"Provider can make the application interviewing with Employer")]
        public async Task ThenProviderCanMakeTheApplicationInterviewingWithEmployer() => await _providerStepsHelper.ApplicantInterviewing();

        [Then(@"Provider can make the application in review")]
        public async Task ThenProviderCanMakeTheApplicationInReview() => await _providerStepsHelper.ApplicantInReview();

        //[Then(@"Provider can make the application shared")]
        //public void ThenProviderCanMakeTheApplicationShared() => _providerStepsHelper.ApplicantShared();

        [Then(@"Provider can see the withdrawn application")]
        public async Task ThenProviderCanSeeTheWithdrawnApplication() => await _providerStepsHelper.ApplicantWithdrawn();
    }
}
