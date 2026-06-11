
using SFA.DAS.RAAProvider.UITests.Project.Helpers;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class ProviderSteps(ScenarioContext context)
    {
        private readonly ProviderStepHelper _providerStepsHelper = new(context);

        [Then(@"Provider can make the application successful")]
        public async Task ThenProviderCanMakeTheApplicationSuccessful() => await _providerStepsHelper.ApplicantSucessful();

        [Then(@"Provider can make the application successful and archive the vacancy")]
        public async Task ThenProviderCanMakeTheApplicationSuccessfulAndArchive() => await _providerStepsHelper.ApplicantSucessfulAndArchive();

        [Then(@"Provider can make the application unsuccessful and archive the vacancy")]
        public async Task ThenProviderCanMakeTheApplicationUnsuccessfulAndArchive() => await _providerStepsHelper.ApplicantUnsucessfulAndArchive();

        [Then(@"Provider can make the application unsuccessful")]
        public async Task ThenProviderCanMakeTheApplicationUnSuccessful() => await _providerStepsHelper.ApplicantUnsucessful();

        [Then(@"Provider can make the application interviewing with Employer")]
        public async Task ThenProviderCanMakeTheApplicationInterviewingWithEmployer() => await _providerStepsHelper.ApplicantInterviewing();

        [Then(@"Provider can make the application in review")]
        public async Task ThenProviderCanMakeTheApplicationInReview() => await _providerStepsHelper.ApplicantInReview();

        [Then(@"Provider can make the application shared")]
        public async Task ThenProviderCanMakeTheApplicationShared() => await _providerStepsHelper.ApplicantShared();

        [Then(@"Provider can share multiple applications")]
        public async Task ThenProviderCanShareMultipleApplications() => await _providerStepsHelper.ShareMutipleApplicants();

        [Then(@"Provider can see the withdrawn application")]
        public async Task ThenProviderCanSeeTheWithdrawnApplication() => await _providerStepsHelper.ApplicantWithdrawn();

        [Then(@"Provider can view the refered vacancy")]
        public async Task ThenProviderCanViewTheReferedVacancy() => await _providerStepsHelper.ViewReferVacancy();

        [Then(@"the Provider verify '(.*)' the wage option selected in the Preview page")]
        public async Task ThenTheProviderVerifyTheWageOptionSelectedInThePreviewPage(string wageType) => await _providerStepsHelper.VerifyWageType(wageType);

        [Then(@"Provider can make multiple applications unsuccessful")]
        public async Task ThenProviderCanMakeMultipleApplicationsUnsuccessful() => await _providerStepsHelper.MutipleApplicantsUnsucessful();

        [Then(@"Provider can make multiple applications unsuccessful and archive the vacancy")]
        public async Task ThenProviderCanMakeMultipleApplicationsUnsuccessfulAndArchive() => await _providerStepsHelper.MutipleApplicantsUnsucessfulAndArchive();

        [Then(@"^the Provider can close the vacancy$")]
        public async Task ThenTheEmployerCanCloseTheVacancy() => await _providerStepsHelper.CloseVacancy();

        [Then(@"^the Provider can archive the vacancy$")]
        public async Task ThenTheProviderCanArchiveTheVacancy() => await _providerStepsHelper.ArchiveVacancy();
    }
}
