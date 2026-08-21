using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class SignUpSteps(ScenarioContext context)
    {
        private readonly CampaignsStepsHelper _stepsHelper = new(context);
        private SignUpPage _signUpPage;

        [Given(@"^the employer navigates to Sign Up Page$")]
        public async Task GivenTheEmployerNavigatesToSignUpPage()
        {
            var page = await _stepsHelper.GoToEmployerHubPage();
            _signUpPage = await page.NavigateToSignUpPage();
        }

        [When(@"^the employer fills? (?:the )?Your [Dd]etails section$")]
        public async Task WhenTheEmployerFillYourDetailsSection() => await _signUpPage.YourDetails();

        [When(@"^selects company size ""([^""]*)""$")]
        public async Task WhenSelectsCompanySize(string companySize) => await _signUpPage.SelectCompanySize(companySize);

        [When(@"Your Company by selecting radiobutton Less than Ten employees$")]
        public async Task WhenYourCompanyBySelectingRadiobuttonLessThanTenEmployees() => await _signUpPage.SelectCompanySize("10");

        [When(@"Your Company by selecting radiobutton Between Ten and FourtyNine employees$")]
        public async Task WhenYourCompanyBySelectingRadiobuttonBetweenTenAndFourtyNineEmployees() => await _signUpPage.SelectCompanySize("10 and 49");

        [When(@"Your Company by selecting radiobutton Between Fifty and TwoFourtyNine employees$")]
        public async Task WhenYourCompanyBySelectingRadiobuttonBetweenFiftyAndTwoFourtyNineEmployees() => await _signUpPage.SelectCompanySize("50 and 249");

        [When(@"Your Company by selecting radiobutton Over TwoHandredAndFifty employees$")]
        public async Task WhenYourCompanyBySelectingRadiobuttonOverTwoHandredAndFiftyEmployees() => await _signUpPage.SelectCompanySize("250");

        [Then(@"^an employer registers interest$")]
        public async Task ThenAnEmployerRegistersInterest() => await _signUpPage.RegisterInterest();
    }
}