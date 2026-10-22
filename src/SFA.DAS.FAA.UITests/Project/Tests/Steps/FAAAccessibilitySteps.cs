namespace SFA.DAS.FAA.UITests.Project.Tests.Steps;

[Binding]
public class FAAAccessibilitySteps(ScenarioContext context)
{
    private readonly FAAStepsHelper _faaStepsHelper = new(context);

    [Then(@"^the apprentice can view and update their personal details in settings, including a validation error$")]
    public async Task ThenTheApprenticeCanViewAndUpdateTheirPersonalDetailsInSettings() =>
        await _faaStepsHelper.ViewAndUpdatePersonalDetailsInSettings();

    [Then(@"^the applicant can save and remove a randomly selected vacancy from search results$")]
    public async Task ThenTheApplicantCanSaveAndRemoveARandomlySelectedVacancyFromSearchResults() =>
        await _faaStepsHelper.SaveAndRemoveARandomlySelectedVacancy();

    [Then(@"^the apprentice answers the equality questions prompted after submitting an application$")]
    public async Task ThenTheApprenticeAnswersTheEqualityQuestionsPromptedAfterSubmittingAnApplication() =>
        await _faaStepsHelper.AnswerEqualityQuestionsAfterSubmittingApplication();

    [Then(@"^the apprentice can view and change their equality question answers in settings$")]
    public async Task ThenTheApprenticeCanViewAndChangeTheirEqualityQuestionAnswersInSettings() =>
        await _faaStepsHelper.ViewAndChangeEqualityQuestionAnswersInSettings();
}
