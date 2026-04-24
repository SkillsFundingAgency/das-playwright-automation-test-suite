using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using SFA.DAS.Framework.Hooks;
using SFA.DAS.ManagingStandards.UITests.Project.Helpers;

namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions;

[Binding, Scope(Tag = "fate")]
public class FateAddAndDeleteE2ESteps(ScenarioContext context) : FrameworkBaseHooks(context)
{
    private readonly FATeHomePage _fATeHomePage = new(context);
    private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage = new(context);
    private readonly ApprenticeshipTrainingCoursesPage _apprenticeshipTrainingCoursesPage = new(context);
    private readonly TrainingProvidersPage _trainingProvidersPage = new(context);
    private readonly ApprenticeshipTrainingCourseDetailsPage _apprenticeshipTrainingCourseDetailsPage = new(context);

    private readonly string _appName = context.Get<ManagingStandardsDataHelpers>().Apprenticeshipunit_ElectricalFitting;

    [When("the provider is listed on the FAT apprenticeship unit providers page")]
    public async Task WhenTheProviderIsListedOnTheFATApprenticeshipUnitProvidersPage()
    {
        await SelectAppUnitCourseByName();
        await _apprenticeshipTrainingCourseDetailsPage.ViewProvidersForThisCourse();
        string providerName = "CENTRAL TRAINING ACADEMY LIMITED";
        await _trainingProvidersPage.VerifyProviderListed(providerName, true);
        await _trainingProvidersPage.VerifyProviderTrainingLocation([providerName, "Online"]);          
        await Navigate(UrlConfig.Provider_BaseUrl);
        await _fATeHomePage.StartNow();
    }

    [When("the provider is listed at learners and provider location on the FAT apprenticeship unit providers page")]
    public async Task TheProviderIsListedOnlearnersAndProviderLocationOnTheFATApprenticeshipUnitProvidersPage()
    {
        await SelectAppUnitCourseByName();
        await _apprenticeshipTrainingCourseDetailsPage.ViewProvidersForThisCourse();
        string providerName = "CENTRAL TRAINING ACADEMY LIMITED";
        await _trainingProvidersPage.VerifyProviderListed(providerName, true);
        await _trainingProvidersPage.VerifyProviderTrainingLocation([providerName, "At learner's workplace", "At training provider's location"]);
        await _trainingProvidersPage.VerifyWithProviderLocationFilter([providerName, "At learner's workplace", "At training provider's location", "miles"]);
        await Navigate(UrlConfig.Provider_BaseUrl);
        await _fATeHomePage.StartNow();
    }

    [Then("the provider is not listed or no provider found on the FAT apprenticeship unit providers page")]
    public async Task TheProviderIsNotListedOrNotFound()
    {
        await SelectAppUnitCourseByName();

        if (await _apprenticeshipTrainingCourseDetailsPage.ProviderAvailableForThisCourse())
        {
            await _apprenticeshipTrainingCourseDetailsPage.ViewProvidersForThisCourse();
            await _trainingProvidersPage.VerifyProviderListed("CENTRAL TRAINING ACADEMY LIMITED", false);
        }       
    }

    private async Task SelectAppUnitCourseByName()
    {
        await Navigate(UrlConfig.FAT_BaseUrl);
        await _fATeHomePage.ClickStartNow();
        await _search_TrainingCourses_ApprenticeworkLocationPage.SelectTrainingTypes([TrainingType.ApprenticeshipUnits]);
        await _fATeHomePage.EnterCourseJobOrStandard(_appName);
        await _fATeHomePage.ApplyFilters();
        await _apprenticeshipTrainingCoursesPage.SelectCourseByName("Electrical fitting and assembly – Apprenticeship unit (level 2)");
    }


    [When("the provider is listed on the FAT training providers page")]
    public async Task WhenTheProviderIsListedOnTheFATTrainingProvidersPage()
    {
        await Navigate(UrlConfig.FAT_BaseUrl);
        await _fATeHomePage.ClickStartNow();
        await _search_TrainingCourses_ApprenticeworkLocationPage.BrowseAllCourses();
        await _fATeHomePage.EnterCourseJobOrStandard("Craft plasterer");
        await _fATeHomePage.ApplyFilters();
        await _apprenticeshipTrainingCoursesPage.SelectCourseByName("Craft plasterer (level 3)");
        await _apprenticeshipTrainingCourseDetailsPage.ViewProvidersForThisCourse();
        await _trainingProvidersPage.VerifyProviderListed("REMIT GROUP LIMITED", true);

        await Navigate(UrlConfig.Provider_BaseUrl);
        await _fATeHomePage.StartNow();
    }

    [When("the provider is not listed on the FAT training providers page")]
    public async Task WhenTheProviderIsNotListedOnTheFATTrainingProvidersPage()
    {
        await Navigate(UrlConfig.FAT_BaseUrl);
        await _fATeHomePage.ClickStartNow();
        await _search_TrainingCourses_ApprenticeworkLocationPage.BrowseAllCourses();
        await _fATeHomePage.EnterCourseJobOrStandard("Craft plasterer");
        await _fATeHomePage.ApplyFilters();
        await _apprenticeshipTrainingCoursesPage.SelectCourseByName("Craft plasterer (level 3)");
        await _apprenticeshipTrainingCourseDetailsPage.ViewProvidersForThisCourse();
        await _trainingProvidersPage.VerifyProviderListed("REMIT GROUP LIMITED", false);
    }
}