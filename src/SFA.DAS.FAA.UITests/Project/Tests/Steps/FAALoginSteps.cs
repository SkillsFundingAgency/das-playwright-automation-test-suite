namespace SFA.DAS.FAA.UITests.Project.Tests.Steps;

[Binding]
public class FAALoginSteps(ScenarioContext context)
{
    [Then(@"the candidate can login in to faa")]
    public async Task TheCandidateCanLoginInToFaav()
    {
        await new FAAStepsHelper(context).GoToFAAHomePage();
    }
}