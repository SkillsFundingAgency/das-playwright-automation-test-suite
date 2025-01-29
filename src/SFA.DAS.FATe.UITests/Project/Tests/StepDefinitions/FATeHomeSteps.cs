using SFA.DAS.FATe.UITests.Project.Tests.Pages;

namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class FATeHomeSteps(ScenarioContext context)
    {
        private readonly FATeStepsHelper _stepsHelper = new(context);

        private FATeHomePage _fATeHomePage;

        [Given(@"the user navigates to FATe Home page and verifies the content")]
        public async Task TheUserNavigatesToFATeHomePageAndVerifiesTheContent() => await _stepsHelper.GoToFATeHomePage();

    }
}
