
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.RAAProvider.UITests.Project.Helpers;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class NavigationSteps(ScenarioContext context)
    {
        private readonly RecruitmentProviderHomePageStepsHelper _recruitmentProviderHomePageStepsHelper = new(context);

        [Then("the Provider can navigate to Manage your recruitment emails page")]
        public async Task ThenTheProviderCanNavigateToManageYourRecruitmentEmailsPage() => await _recruitmentProviderHomePageStepsHelper.GoToManageYourRecruitmentEmailsPage();

    }
}
