using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;

[Binding, Scope(Tag = "apar")]
public class MangeRestrictedCoursesSteps
{
    private readonly ScenarioContext _context;
    private readonly ViewMangeRestrictedCoursesPage _viewMangeRestrictedCoursesPage;
    private readonly AcademicProfessionalPage _academicProfessionalPage;
    private readonly StopThisProviderPage _stopThisProviderPage;

    public MangeRestrictedCoursesSteps(ScenarioContext context)
    {
        _context = context;
        _viewMangeRestrictedCoursesPage =
            new ViewMangeRestrictedCoursesPage(context);
        _academicProfessionalPage = new AcademicProfessionalPage(context);
        _stopThisProviderPage = new StopThisProviderPage(context);
    }

    [Given(@"the user navigates to restricted courses")]
    [When(@"the user navigates to restricted courses")]
    public async Task WhenTheUserNavigatesToRestrictedCourses()
    {
        var manageTrainingProviderPage = await OpenManageTrainingProviderPage();
        await manageTrainingProviderPage.NavigateToRestrictedCourses();
        await _viewMangeRestrictedCoursesPage.VerifyPage();
    }

    [When(@"the user searches for ""(.*)""")]
    public async Task WhenTheUserSearchesFor(string courseName)
    {
        await _viewMangeRestrictedCoursesPage.SearchFunctionality(courseName);
    }

    [Then(@"the user is able to verify the (?:provider|restricted course) results contain ""(.*)""")]
    public async Task ThenTheUserIsAbleToVerifyTheRestrictedCourseResults(string searchWord)
    {
        await _viewMangeRestrictedCoursesPage.VerifyResults(searchWord);
    }

    [When(@"the user selects the ""(.*)"" training type filter")]
    public async Task WhenTheUserSelectsTheTrainingTypeFilter(string trainingType)
    {
        await _viewMangeRestrictedCoursesPage.SelectFilter(trainingType);
    }

    [When(@"the user selects the following training type filters:")]
    public async Task WhenTheUserSelectsTheFollowingTrainingTypeFilters(Table table)
    {
        foreach (var row in table.Rows)
        {
            await _viewMangeRestrictedCoursesPage.SelectFilter(row["Training Type"]);
        }
    }

    [When(@"the user applies the filter")]
    public async Task WhenTheUserAppliesTheFilter()
    {
        await _viewMangeRestrictedCoursesPage.ApplyFilter();
    }

    [Then(@"the user is able to verify the ""(.*)"" filter is selected")]
    public async Task ThenTheUserIsAbleToVerifyTheFilterIsSelected(string trainingType)
    {
        await _viewMangeRestrictedCoursesPage.VerifySelectedFilter(trainingType);
    }

    [Then(@"the user is able to verify the following filters are selected:")]
    public async Task ThenTheUserIsAbleToVerifyTheFollowingFiltersAreSelected(Table table)
    {
        foreach (var row in table.Rows)
        {
            await _viewMangeRestrictedCoursesPage.VerifySelectedFilter(row["Training Type"]);
        }
    }

    [When(@"the user clears the selected filter")]
    public async Task WhenTheUserClearsTheSelectedFilter()
    {
        await _viewMangeRestrictedCoursesPage.ClearAllFilters();
    }

    [When(@"the user clears all selected filters")]
    public async Task WhenTheUserClearsAllSelectedFilters()
    {
        await _viewMangeRestrictedCoursesPage.ClearAllFilters();
    }

    [Then(@"the user is able to verify that no filters are selected")]
    public async Task ThenTheUserIsAbleToVerifyThatNoFiltersAreSelected()
    {
        await _viewMangeRestrictedCoursesPage.VerifyNoFiltersSelected();
    }

    private async Task<ManageTrainingProviderInformationPage>OpenManageTrainingProviderPage()
    {
        var home = new AparAdminHomePage(_context);
        await home.ClickManageTrainingProvidersAndRestrictedCourses();
        return await new ManageTrainingProviderInformationPage(_context)
            .VerifyPageAsync( () => new ManageTrainingProviderInformationPage(_context));
    }

    private async Task<AcademicProfessionalPage>OpenAcademicProfessionalPage(string larsCode)
    {
        var home = new ViewMangeRestrictedCoursesPage(_context);
        await home.SelectRestrictedManageProviders(larsCode);
        return await new AcademicProfessionalPage(_context)
            .VerifyPageAsync( () => new AcademicProfessionalPage(_context));
    }

    private async Task<StopThisProviderPage>OpenStopThisProviderPage(string UKPRN)
    {
        var home = new AcademicProfessionalPage(_context);
        await home.ClickChange(UKPRN);
        return await new StopThisProviderPage(_context)
            .VerifyPageAsync( () => new StopThisProviderPage(_context));
    }

    [Then(@"the user verifies pagination links are working as expected")]
        public async Task ThenTheUserVerifiesPaginationLinksAreWorkingAsExpected()
        {
            await _viewMangeRestrictedCoursesPage.VerifyPaginationLinks(new List<int> { 2 });
        }

    [When(@"the user selects Manage providers on course with LARS Code ""(.*)""")]
    [Given(@"the user selects Manage providers on course with LARS Code ""(.*)""")]

        public async Task WhenTheUserSelectsManageProvidersOnCourseWithLARSCode(string larsCode)
        {
            await OpenAcademicProfessionalPage(larsCode);
            await _academicProfessionalPage.VerifyPage();
        }

    [When(@"the user seaches for provider with UKPRN ""(.*)""")]

        public async Task WhenTheUserSearchesForProviderWithUKPRN(string UKPRN)
        {
            await _academicProfessionalPage.SearchFunctionality(UKPRN);
            
        }

    [Then(@"the user is able to verify the provider results contains ""(.*)""")]

        public async Task ThenTheUserIsAbleToVerifyTheProviderResults(string searchWord)
    {
            await _academicProfessionalPage.VerifyResults(searchWord);
        }
    [Then(@"the user selects the filter ""(.*)""")]

        public async Task ThenTheUserSelectsTheFilters(string filterName)
        {
            await _viewMangeRestrictedCoursesPage.SelectFilter(filterName);
        }
    [Then(@"the user applies the filter")]
    public async Task ThenTheUserAppliesTheFilter()
    {
        await _viewMangeRestrictedCoursesPage.ApplyFilter();
    }

    [Given(@"the user clicks to change the details of provider with UKPRN ""(.*)""")]

        public async Task GivenTheUserClicksToChangeTheDetailsOfProviderWithUKPRN(string UKPRN)
        {
            await OpenStopThisProviderPage(UKPRN);
            await _stopThisProviderPage.VerifyPage();
        }

    [When(@"the user enters a date in the future ""(.*)""")]

        public async Task WhenTheUserEntersADateInTheFuture(string date)
        {
            await _stopThisProviderPage.EnterDate(date);
        }

    [When(@"the user enters a date before Sept 2014 ""(.*)""")]
    public async Task WhenTheUserEntersADateBeforeSept2014(string date)
    {
        await WhenTheUserEntersADateInTheFuture(date);
    }

    [When(@"the user enters a date after Sept 2014 ""(.*)""")]

    public async Task WhenTheUserEntersADateAfterSept2014(string date)
    {
        await WhenTheUserEntersADateInTheFuture(date);
    }

    [Then(@"the last date for new starts is updated for ""(.*)"" to ""(.*)"" and success banner is displayed")]

    public async Task ThenTheLastDateForNewStartsIsUpdatedForToAndSuccessBannerISDisplayed(string UKPRN, string date)
    {
        await _academicProfessionalPage.VerifyDateUpdate(UKPRN,date);
    }

    [When(@"the user confirms they want to ""(.*)""")]

    public async Task WhenTheUserConfirmsTheyWantTo(string actionToTake)
    {
        await _stopThisProviderPage.ChangeRestriction(actionToTake);
    }

    [Then(@"the user sees the message ""(.*)""")]

    public async Task ThenTheUserSeesTheMessage(string message)
    {
        await _stopThisProviderPage.LastStartDateErrorMessage(message);
    }

    [Then(@"the banner for UKPRN ""(.*)"" displays ""(.*)""")]

    public async Task ThenTheBannerForUKPRNDisplays(string UKPRN, string bannerMessage)
    {
        await _academicProfessionalPage.AssertBanner(UKPRN, bannerMessage);
    }
}