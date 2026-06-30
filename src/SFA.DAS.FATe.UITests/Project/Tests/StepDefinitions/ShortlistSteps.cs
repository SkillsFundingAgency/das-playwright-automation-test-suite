using SFA.DAS.FATe.UITests.Project.Tests.Pages;

namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinition;

[Binding, Scope(Tag = "fate")]
public class ShortlistSteps(ScenarioContext context)
{
    private readonly TrainingProvidersPage _trainingProvidersPage = new(context);
    private readonly ShortlistPage _shortlistPage = new(context);

    [Then("^the shortlisted provider should display location (.*)$")]
    public void TheShortlistedProviderShouldDisplayLocation(string location)
    {
        
    }

    [When("^the user navigates to the shortlist page$")]
    public void TheUserNavigatesToTheShortlistPage()
    {
        
    }

    [When("^the user adds a provider to the shortlist$")]
    public void TheUserAddsAProviderToTheShortlist()
    {
        
    }

    [Then("^verify add remove count shortlist functionality$")]

    public async Task ThenVerifyAddRemoveCountShortlistFunctionality()
    {
        {
            await _trainingProvidersPage.AddProviderToShortlist("10002599");
            await _trainingProvidersPage.ClickViewShortlistAsync();
            await _shortlistPage.VerifyCourseNameShortlisted("Adult care worker (level 2)");
            await _shortlistPage.GoToTrainingProvidersPage();
            await _trainingProvidersPage.ClickRemoveFromShortlistAsync();
            await _trainingProvidersPage.ClickViewShortlistAsync();
        }
    }
}