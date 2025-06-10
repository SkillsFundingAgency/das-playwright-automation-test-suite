using System;

namespace SFA.DAS.EarlyConnectForms.UITests.Project.Tests.Steps
{
    [Binding]
    public class NonUcasStudentJourneyStepDefinitions(ScenarioContext context)
    {
        private readonly EarlyConnectStepsHelper _stepsHelper = new(context);

        [Given(@"I am on the landing page for a region")]
        public async Task GivenIAmOnTheLandingPageForARegion() => await _stepsHelper.GoToEarlyConnectHomePage();

        [Given(@"I selected (.*) Advisor Page")]
        public async Task GivenISelectedCountyAdvisorPage(string county)
        {
            switch (county)
            {
                case "Lancashire":
                    await _stepsHelper.GoToEarlyConnectLancashireAdvisorPage();
                    break;

                case "North East":
                    await _stepsHelper.GoToEarlyConnectNorthEastAdvisorPage();
                    break;

                case "London":
                    await _stepsHelper.GoToEarlyConnectLondonAdvisorPage();
                    break;

                default:
                    throw new ArgumentException("County link not found");
            }
        }

        [Given(@"I enter valid details")]
        public async Task GivenIEnterValidDetails()
        {
            await _stepsHelper.GoToEarlyConnectEmailPage();
            await _stepsHelper.GoToAddUniqueEmailAddressPage();
            await _stepsHelper.GoToCheckEmailAuthCodePage();
            await _stepsHelper.GoToWhatYourNamePage();
            await _stepsHelper.GoToWhatIsYourDateOfBirthPage();
            await _stepsHelper.GoToPostCodePage();
            await _stepsHelper.GoToWhatYourTelephonePage();
        }

        [Given(@"I answer the triage questions related to me")]
        public async Task GivenIAnswerTheTriageQuestionsRelatedToMe()
        {
            await _stepsHelper.GoToAreasOfWorkInterestPage();
            await _stepsHelper.GoToNameOfSchoolCollegePage();
            await _stepsHelper.GoToApprencticeshipLevelPage();
            await _stepsHelper.GoToHaveYouAppliedPage();
            await _stepsHelper.GoToAreaOfEnglandPage();
            await _stepsHelper.GoToSupportPage();
        }

        [Given(@"I enter invalid details for school autosearch")]
        public async Task GivenIEnterInvalidDetailsForSchoolAutosearch()
        {
            await _stepsHelper.GoToAreasOfWorkInterestPage();
            await _stepsHelper.GoToEnterNameOfSchoolCollegePage();
            await _stepsHelper.GoToApprencticeshipLevelPage();
            await _stepsHelper.GoToHaveYouAppliedPage();
            await _stepsHelper.GoToAreaOfEnglandPage();
            await _stepsHelper.GoToSupportPage();
        }

        [Then(@"I check my answers, accept and submit")]
        public async Task WhenICheckMyAnswersAcceptAndSubmit() => await _stepsHelper.GoToCheckYourAnswerPage();

    }
}
