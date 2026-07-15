
using SFA.DAS.RAAProvider.UITests.Project.Helpers;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class NavigationSteps(ScenarioContext context)
    {
        private readonly RecruitmentProviderHomePageStepsHelper _recruitmentProviderHomePageStepsHelper = new(context);

        [Then("the Provider can navigate to Manage your recruitment emails page")]
        public async Task ThenTheProviderCanNavigateToManageYourRecruitmentEmailsPage() => await _recruitmentProviderHomePageStepsHelper.GoToManageYourRecruitmentEmailsPage();

        [Given(@"the Provider navigates to 'Recruit' Page")]
        [When(@"the Provider navigates to 'Recruit' Page")]
        public async Task WhenTheProviderNavigatesToPage() => await _recruitmentProviderHomePageStepsHelper.GoToRecruitmentProviderHomePage(true);

        [Then("the Provider can navigate to Reports page")]
        public async Task ThenTheProviderCanNavigateToReportsPage() => await _recruitmentProviderHomePageStepsHelper.GoToReportsPage();

        [Then("the Provider can navigate to apprentice requests page")]
        public async Task ThenTheProviderCanNavigateToApprenticeRequestsPage() => await _recruitmentProviderHomePageStepsHelper.GoToApprenticeRequestsPage();

        [Then(@"the Provider can navigate to manage funding page")]
        public async Task ThenTheProviderCanNavigateToManageFundingPage() => await _recruitmentProviderHomePageStepsHelper.GoToManageFundingPage();


        [Then("the Provider can navigate to manage your apprentices page")]
        public async Task ThenTheProviderCanNavigateToManageYourApprenticesPage() => await _recruitmentProviderHomePageStepsHelper.GoToManageYourApprenticesPage();


        [Then("the Provider can navigate to organisations and agreements page")]
        public async Task ThenTheProviderCanNavigateToOrganisationsAndAgreementsPage() => await _recruitmentProviderHomePageStepsHelper.GoToOrganisationsAndAgreementsPage();


        [Then("the Provider can navigate to recruit notification settings page")]
        public async Task ThenTheProviderCanNavigateToRecruitNotificationSettingsPage() => await _recruitmentProviderHomePageStepsHelper.GoToNotificationsViaSettingsPage();


        [Then("the Provider can navigate to change your sign in details settings page")]
        public void ThenTheProviderCanNavigateToChangeYourSignInDetailsSettingsPage() => _recruitmentProviderHomePageStepsHelper.GoToChangeYourSignInDetailsPage();
    }
}
