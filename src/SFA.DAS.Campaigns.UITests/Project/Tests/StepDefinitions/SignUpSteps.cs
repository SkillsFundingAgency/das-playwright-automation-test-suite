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

        [When(@"^Your Company by selecting radiobutton Less than Ten employees$")]
        public async Task WhenYourCompanyBySelectingRadiobuttonLessThanTenEmployees() => await _signUpPage.SelectCompanySizeOption1();

        [When(@"^Your Company by selecting radiobutton Between Ten and FourtyNine employees$")]
        public async Task WhenYourCompanyBySelectingRadiobuttonBetweenTenAndFourtyNineEmployees() => await _signUpPage.SelectCompanySizeOption2();

        [When(@"^Your Company by selecting radiobutton Between Fifty and TwoFourtyNine employees$")]
        public async Task WhenYourCompanyBySelectingRadiobuttonBetweenFiftyAndTwoFourtyNineEmployees() => await _signUpPage.SelectCompanySizeOption3();

        [When(@"^Your Company by selecting radiobutton Over TwoHundredandFifty employees$")]
        public async Task WhenYourCompanyBySelectingRadiobuttonOverTwoHundredandFiftyEmployees() => await _signUpPage.SelectCompanySizeOption4();

        [Then(@"^an employer registers interest$")]
        public async Task ThenAnEmployerRegistersInterest() => await _signUpPage.RegisterInterest();
    }
}