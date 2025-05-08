using System.Configuration.Provider;
using System.Diagnostics;
using Azure;
using SFA.DAS.FATe.UITests.Project.Tests.Pages;

[Binding, Scope(Tag = "fate")]
public class ShortlistSteps
{
    private readonly TrainingProvidersPage _trainingProvidersPage;
    private readonly ShortlistPage _shortlistPage;

    public ShortlistSteps(ScenarioContext context)
    {
        _trainingProvidersPage = new TrainingProvidersPage(context);
        _shortlistPage = new ShortlistPage(context);
    }
    [Then("verify add remove count shortlist functionality")]

    public async Task ThenVerifyAddRemoveCountShortlistFunctionality()
    {
        {
            await _trainingProvidersPage.AddProviderToShortlist("10005077");
            await _trainingProvidersPage.ClickViewShortlistAsync();
            await _shortlistPage.VerifyCourseNameShortlisted("Adult care worker (level 2)");
            await _shortlistPage.GoToTrainingProvidersPage();
            await _trainingProvidersPage.ClickRemoveFromShortlistAsync();
            await _trainingProvidersPage.ClickViewShortlistAsync();
        }
    }
}