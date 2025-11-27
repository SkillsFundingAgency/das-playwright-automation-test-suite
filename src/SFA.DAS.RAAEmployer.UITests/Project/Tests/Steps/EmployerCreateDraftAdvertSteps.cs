using SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;
using SFA.DAS.RAAEmployer.UITests.Project.Helpers;
using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Steps;

[Binding]
public class EmployerCreateDraftAdvertSteps(ScenarioContext context)
{
    private readonly EmployerCreateDraftAdvertStepsHelper _stepsHelper = new(context);

    private YourApprenticeshipAdvertsHomePage _yourApprenticeshipAdvertsHomePage;

    [When(@"the Employer creates first Draft advert")]
    public async Task TheEmployerCreatesFirstDraftAdvert() => await ReturnToApplications(await _stepsHelper.CreateFirstDraftAdvert(new CreateAnApprenticeshipAdvertOrVacancyPage(context)));

    [Then(@"the Employer is able to delete the draft vacancy")]
    public async Task ThenTheEmployerIsAbleToDeleteTheDraftVacancy() => await _stepsHelper.CompleteDeleteOfDraftVacancy();

    [When(@"the Employer completes the Draft advert to cancel deleting the draft")]
    public async Task TheEmployerCreatesCompleteDraftAdvert() => await _stepsHelper.CompleteDraftAdvert(await GoToYourAdvertFromDraftAdverts());

    [Then(@"the Employer can open the draft and submits the advert")]
    public async Task TheEmployerCanOpenTheDraftAndSubmitsTheAdvert() => await _stepsHelper.SubmitDraftAdvert(await GoToYourAdvertFromDraftAdverts());

    [Given(@"the Employer creates Draft advert")]
    public async Task TheEmployerCreatesDraftAdvert() => await ReturnToApplications(await _stepsHelper.CreateDraftAdvert());

    [Then(@"the advert is saved as a draft")]
    public async Task ThenTheVacancyIsSavedAsADraft() => await GoToYourAdvertFromDraftAdverts();

    [When(@"Employer cancels after saving the title of the advert")]
    public async Task EmployerCancelsAfterSavingTheTitleOfTheAdvert() => _yourApprenticeshipAdvertsHomePage = await _stepsHelper.CancelAdvert();

    private async Task ReturnToApplications(CreateAnApprenticeshipAdvertOrVacancyPage page) { await page.ReturnToApplications(); _yourApprenticeshipAdvertsHomePage = new YourApprenticeshipAdvertsHomePage(context); }

    private async Task<CreateAnApprenticeshipAdvertOrVacancyPage> GoToYourAdvertFromDraftAdverts()
    {
        var page = await _yourApprenticeshipAdvertsHomePage.GoToYourAdvertFromDraftAdverts();

        return await page.CreateAnApprenticeshipAdvertPage();
    }
}
