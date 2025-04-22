using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign;
using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.Moderation;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Steps;

[Binding]
public class MS_Moderation_Steps(ScenarioContext context)
{
    private Moderation_ProviderDetailsPage _moderation_ProviderDetailsPage;
    private Moderation_UpdateProviderPage _moderation_UpdateProviderPage;

    [Given(@"the tribal user searches for provider with UKPRN")]
    public async Task GivenTheTribalUserSearchesForProviderWithUKPRN()
    {
        await new DfeAdminLoginStepsHelper(context).NavigateAndLoginToASAdmin();

        var page = new Moderation_HomePage(context);

        var page1 = await page.SearchForTrainingProvider();

        _moderation_ProviderDetailsPage = await page1.SearchTrainingProviderByUkprn(MS_DataHelper.UKPRN);
    }

    [When(@"the tribal user chooses to change the provider details")]
    public async Task WhenTheTribalUserChoosesToChangeTheProviderDetails()
    {
        _moderation_UpdateProviderPage = await _moderation_ProviderDetailsPage.ChangeProviderDetail();
    }

    [Then(@"the tribal user is allowed to make the change")]
    public async Task ThenTheTribalUserIsAllowedToMakeTheChange()
    {
        var page = await _moderation_UpdateProviderPage.EnterUpdateDescriptionAndContinue();

        var page1 = await page.ContinueOnCheckProviderUpdatePage();

        var page2 = await page1.ChangeProviderDetail();
        
        await page2.VerifyUpdateDescriptionText();
    }
}
