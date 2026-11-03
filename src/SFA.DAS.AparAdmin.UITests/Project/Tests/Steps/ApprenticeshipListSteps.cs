using Azure;
using SFA.DAS.AparAdmin.UITests.Project.Helpers;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;

[Binding, Scope(Tag = "apar")]
public class ApprenticeshipListSteps
{
    private readonly ScenarioContext _context;
    private readonly ProviderRestrictedCoursesPage _providerRestrictedCoursesPage;
    private readonly RestrictACoursesPage _restrictACoursePage;
    private readonly ChangeRestrictionPage _changeRestrictionPage;
    private readonly SetLastStartDatePage _setLastStartDatePage;
    private readonly RemoveRestrictionPage _removeRestrictionPage;
    private readonly ApprenticeshipListHelper _apprenticeshipListHelper;
    private readonly RestrictProviderPage _restrictProviderPage;
    private readonly AddApprenticeshipToAllowedCoursePage _addApprenticeshipToAllowedCoursePage;
    private readonly AllowedCoursesPage _allowedCoursesPage;
    private readonly AddApprenticeshipUnitToAllowedPage _addApprenticeshipUnitToAllowedPage;
    private readonly AllowedCourseEditPage _allowedCourseEditPage;


public ApprenticeshipListSteps(ScenarioContext context)
{
    _context = context;
    _providerRestrictedCoursesPage = new ProviderRestrictedCoursesPage(context);
    _restrictACoursePage = new RestrictACoursesPage(context);
    _changeRestrictionPage = new ChangeRestrictionPage(context);
    _setLastStartDatePage = new SetLastStartDatePage(context);
    _removeRestrictionPage = new RemoveRestrictionPage(context);
    _apprenticeshipListHelper = new ApprenticeshipListHelper(context);
    _restrictProviderPage = new RestrictProviderPage(context);
    _allowedCoursesPage = new AllowedCoursesPage(context);
    _addApprenticeshipToAllowedCoursePage = new AddApprenticeshipToAllowedCoursePage(context);
    _addApprenticeshipUnitToAllowedPage = new AddApprenticeshipUnitToAllowedPage(context);
    _allowedCourseEditPage = new AllowedCourseEditPage(context);
}    

    [When(@"the user clicks on manage restricted courses")]

    public async Task WhenTheUserClicksOnManageRestrictedCourses()
    {
        await _apprenticeshipListHelper.OpenManageProviderCoursePage();
    }
    
    [When(@"the user navigates to manage approved courses")]

    public async Task WhenTheUserClicksOnManageApprovedCourses()
    {
        await _apprenticeshipListHelper.OpenManageAllowedCoursePage();
    }

    [When(@"the user navigates to manage approved units")]

    public async Task WhenTheUserClicksOnManageApprovedUnits()
    {
        await _apprenticeshipListHelper.GoBackToProviderDetailsPage();
        await _apprenticeshipListHelper.OpenManageApprenticeshipUnitsPage();
    }

    [Then(@"the user uses the search and filter functionality and results are displayed as expected")]

    public async Task ThenTheUserUsesTheSearchAndFilterFunctionalityAndResultsAreDisplayedAsExpected()
    {
        await _providerRestrictedCoursesPage.SearchFunctionality("professional");
        await _providerRestrictedCoursesPage.VerifyResults("professional", "yes");
        await _providerRestrictedCoursesPage.SelectFilter("Last start date added");
        await _providerRestrictedCoursesPage.ApplyFilter();
        await _providerRestrictedCoursesPage.VerifySelectedFilter("Last start date added");
        await _providerRestrictedCoursesPage.ClearAllFilters();
        await _providerRestrictedCoursesPage.VerifyNoFiltersSelected();
    }

    [Then(@"the user restricts a course that is not available in the drop down")]

    public async Task ThenTheUserRestrictsACourseThatIsNotAvailableInTheDropDown()
    {
        await _apprenticeshipListHelper.ClickRestrictACourse();
        await _restrictACoursePage.SelectCourse("Singing");
        await _restrictACoursePage.ErrorMessageForWrongInput();
    }

    [Then(@"^the user restricts for the provider course ""(.*)"" which they currently do not provide$")]

    public async Task ThenTheUserRestrictsForTheProviderCourseWhichTheyCurrentlyDoNotProvide(string courseName)
    {
        await _restrictACoursePage.SelectCourse(courseName);
        await _restrictACoursePage.ConfirmRestriction();
        await _providerRestrictedCoursesPage.SearchFunctionality(courseName);
        await _providerRestrictedCoursesPage.VerifyResults(courseName, "yes");
    }

    [Then(@"the user restricts for the provider course ""(.*)"" which they currently provide")]

    public async Task ThenTheUserRestrictsForTheProviderCourseWhichTheyCurrentlyProvide(string courseName)
    {
        await _apprenticeshipListHelper.ClickRestrictACourse();
        await _restrictACoursePage.SelectCourse(courseName);
        await _setLastStartDatePage.EnterDate("20-08-2027");
        await _setLastStartDatePage.ConfirmLastStartDate();
        await _setLastStartDatePage.ErrorMessageForWrongInput();
        await _setLastStartDatePage.EnterDate("20-08-2020");
        await _setLastStartDatePage.ConfirmLastStartDate();
        await _providerRestrictedCoursesPage.SearchFunctionality(courseName);
        await _providerRestrictedCoursesPage.VerifyResults(courseName, "yes");
    }

    [Then(@"the user ""(.*)"" a date for restricted course with larsCode ""(.*)"" to a date in the ""(.*)"" ""(.*)""")]
    [Then(@"the user ""(.*)"" a date for restricted course with larsCode ""(.*)"" to a date in the ""(.*)"" ""(.*)""")]


    public async Task ThenTheUserEditsADateForARestrictedCourseToADateInTheFuture(string editType, string larsCode, string timeReference, string date)
    {
        await _apprenticeshipListHelper.ClickChangeCourseDateDetails(larsCode);
        await _changeRestrictionPage.SelectOptionAddEditRemove(editType);
        await _apprenticeshipListHelper.ConfirmEdit();
        await _setLastStartDatePage.EnterDate(date);
        await _setLastStartDatePage.ConfirmLastStartDate();
        string bannerMessage = timeReference switch
        {
            "future" => "Last start date added",
            "past"  => "Closed to new starts",
             _        => throw new System.ArgumentException($"Unknown message type: {timeReference}")
        };
        await _providerRestrictedCoursesPage.AssertBanner(larsCode, bannerMessage);
    }

    [Then (@"the user ""(.*)"" restrictions for the provider on the course with lars code ""(.*)""")]

    public async Task ThenTheUserRemovedRestrictionsForTheProviderOnTheCourse(string editType, string larsCode)
    {
        await _apprenticeshipListHelper.ClickChangeCourseDateDetails(larsCode);
        await _changeRestrictionPage.SelectOptionEditRemove(editType);
        await _apprenticeshipListHelper.ConfirmRemoveCourseRestriction();
        await _removeRestrictionPage.RemoveRestriction();
        await _providerRestrictedCoursesPage.SearchFunctionality(larsCode);
        await _providerRestrictedCoursesPage.VerifyResults(larsCode, "no");
    }

    [Then(@"the user changes the provider from unrestricted to restricted")]

    public async Task ThenTheUserNavigatesToAnUnrestrictedProviderAndChangesThemToARestrictedProvider()
    {
        await _apprenticeshipListHelper.ChangeHowWeManageProvider();
        await _apprenticeshipListHelper.ControlProviderDelivery();
        await _apprenticeshipListHelper.ConfirmControlProviderDelivery();
    }

    [Then(@"the user can add apprenticeships that the provider can deliver")]

    public async Task ThenTheUserCanAddApprenticeshipsThatTheProviderCanDeliver()
    {
        await _apprenticeshipListHelper.AddAnApprenticeship();
        await _addApprenticeshipToAllowedCoursePage.SelectCourse("Associate continuing healthcare practitioner (Level 5)");
        await _allowedCoursesPage.SuccessBannerVisible();
    }   

    [Then(@"the user can add apprenticeship units that the provider can deliver")]

    public async Task ThenTheUserCanAddApprenticeshipsUnitsThatTheProviderCanDeliver()
    {
        await _apprenticeshipListHelper.AddAnApprenticeshipUnit();
        await _addApprenticeshipUnitToAllowedPage.SelectCourse("Permanent modular building assembly – Apprenticeship unit (Level 2)");
        await _allowedCoursesPage.SuccessBannerVisible();
    }   

    [Then(@"the user can edit dates for allowlisted apprenticeships that the provider can deliver")]

    public async Task ThenTheUserCanEditDatesForAllowlistedApprenticeshipsThatTheProviderCanDeliver()
    {
        await _apprenticeshipListHelper.AllowedCourseAddLastDate("128"); // Course Open to new starts
        await _setLastStartDatePage.EnterDate("20-08-2054");
        await _setLastStartDatePage.ConfirmLastStartDate();
        await _allowedCoursesPage.SuccessBannerVisible();
        await _allowedCoursesPage.AssertBanner("128", "Last start date added");
        await _apprenticeshipListHelper.EditAllowedCourseDetails("128");
        await _allowedCourseEditPage.SelectOptionEditRemove("Edits");
        await _allowedCourseEditPage.ContinueToEditDate();
        await _setLastStartDatePage.EnterDate("20-08-2014");
        await _setLastStartDatePage.ConfirmLastStartDate();
        await _setLastStartDatePage.ErrorMessageForWrongInput();
        await _setLastStartDatePage.EnterDate("20-08-2016");
        await _setLastStartDatePage.ConfirmLastStartDate();
        await _allowedCoursesPage.AssertBanner("128", "Closed to new starts");
    }

    [Then(@"the user can edit dates for allowlisted apprenticeship units that the provider can deliver")]

    public async Task ThenTheUserCanEditDatesForAllowlistedApprenticeshipUnitsThatTheProviderCanDeliver()
    {
        await _apprenticeshipListHelper.EditApprenticeshipUnit("ZSC00007");
        await _setLastStartDatePage.EnterDate("20-08-2054");
        await _setLastStartDatePage.ConfirmLastStartDate();
        await _allowedCoursesPage.SuccessBannerVisible();
        await _allowedCoursesPage.AssertBanner("ZSC00007", "Last start date added");
        await _apprenticeshipListHelper.EditAllowedCourseDetails("ZSC00007"); 
        await _allowedCourseEditPage.SelectOptionEditRemove("Edits");
        await _allowedCourseEditPage.ContinueToEditDate();
        await _setLastStartDatePage.EnterDate("20-08-2014");
        await _setLastStartDatePage.ConfirmLastStartDate();
        await _setLastStartDatePage.ErrorMessageForWrongInput();
        await _setLastStartDatePage.EnterDate("20-08-2016");
        await _setLastStartDatePage.ConfirmLastStartDate();
        await _allowedCoursesPage.AssertBanner("ZSC00007", "Closed to new starts");
    }
 
}