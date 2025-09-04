namespace SFA.DAS.FAA.UITests.Project.Tests.Steps;

[Binding]
public class FAALoginSteps(ScenarioContext context)
{
    [Given(@"the candidate can login in to faa")]
    [Then(@"the candidate can login in to faa")]
    public async Task TheCandidateCanLoginInToFaav()
    {
        await new FAAStepsHelper(context).GoToFAAHomePage();
    }
}