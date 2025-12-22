namespace SFA.DAS.RAA.Service.Project.Steps;

[Binding]
public class ReviewerSteps(ScenarioContext context)
{
    private readonly ReviewerStepsHelper _reviewerStepsHelper = new(context);


    [Given(@"the Reviewer Approves the vacancy")]
    [When(@"the Reviewer Approves the vacancy")]
    [Then(@"the Reviewer Approves the vacancy")]
    public async Task TheReviewerApprovesTheVacancy() => await _reviewerStepsHelper.VerifyEmployerNameAndApprove();

    [Given(@"the Reviewer sign out")]
    [When(@"the Reviewer sign out")]
    [Then(@"the Reviewer sign out")]
    public async Task TheReviewerSignOut() => await _reviewerStepsHelper.RAAQASignOut();

    [Given(@"the Reviewer Refer the vacancy")]
    public async Task GivenTheReviewerReferTheVacancy() => await _reviewerStepsHelper.Refer();

    [Then(@"the Reviewer verifies disability confident and approves the vacancy")]
    public async Task ThenTheReviewerVerifiesDisabilityConfidentAndApprovesTheVacancy() => await _reviewerStepsHelper.VerifyDisabilityConfidenceAndApprove();
}
