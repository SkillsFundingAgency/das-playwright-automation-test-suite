using SFA.DAS.AparAdmin.UITests.Project.Helpers;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;

[Binding, Scope(Tag = "apar")]
public class ApprenticeshipListSteps
{
    private readonly ScenarioContext _context;
    private readonly ProviderRestrictedCoursesPage _providerRestrictedCoursesPage;
    private readonly RestrictACoursesPage _restrictACoursePage;
    private readonly ChangeRestrictionPage _changeRestrictionPage;
    private readonly SetLastStartDatePage _setLastStartPage;
    private readonly RemoveRestrictionPage _removeRestrictionPage;
    private readonly ApprenticeshipListHelper _apprenticeshipListHelper;
    private readonly RestrictProviderPage _restrictProviderPage;


public ApprenticeshipListSteps(ScenarioContext context)
{
    _context = context;
    _providerRestrictedCoursesPage = new ProviderRestrictedCoursesPage(context);
    _restrictACoursePage = new RestrictACoursesPage(context);
    _changeRestrictionPage = new ChangeRestrictionPage(context);
    _setLastStartPage = new SetLastStartDatePage(context);
    _removeRestrictionPage = new RemoveRestrictionPage(context);
    _apprenticeshipListHelper = new ApprenticeshipListHelper(context);
    _restrictProviderPage = new RestrictProviderPage(context);
}    

    [When(@"the user clicks on manage restricted courses")]

    public async Task WhenTheUserClicksOnManageRestrictedCourses()
    {
        await _apprenticeshipListHelper.OpenManageProviderCoursePage();
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
        await _setLastStartPage.EnterDate("20-08-2027");
        await _setLastStartPage.ConfirmLastStartDate();
        await _setLastStartPage.ErrorMessageForWrongInput();
        await _setLastStartPage.EnterDate("20-08-2020");
        await _setLastStartPage.ConfirmLastStartDate();
        await _providerRestrictedCoursesPage.SearchFunctionality(courseName);
        await _providerRestrictedCoursesPage.VerifyResults(courseName, "yes");
    }

    [Then(@"the user ""(.*)"" a date for restricted course with larsCode ""(.*)"" to a date in the ""(.*)"" ""(.*)""")]
    [Then(@"the user ""(.*)"" a date for restricted course with larsCode ""(.*)"" to a date in the ""(.*)"" ""(.*)""")]


    public async Task ThenTheUserEditsADateForARestrictedCourseToADateInTheFuture(string editType, string larsCode, string timeReference, string date)
    {
        await _apprenticeshipListHelper.ClickChangeCourseDateDetails(larsCode);
        await _changeRestrictionPage.SelectOption(editType);
        await _apprenticeshipListHelper.ConfirmEdit();
        await _setLastStartPage.EnterDate(date);
        await _setLastStartPage.ConfirmLastStartDate();
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
        await _changeRestrictionPage.SelectOption(editType);
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
}