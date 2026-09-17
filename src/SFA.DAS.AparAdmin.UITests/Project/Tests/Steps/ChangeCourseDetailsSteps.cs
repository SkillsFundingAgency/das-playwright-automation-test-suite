using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;

[Binding, Scope(Tag = "apar")]

public class ChangeCourseDetailsSteps
{
    private readonly ScenarioContext _context;
    private readonly AcademicProfessionalPage _academicProfessionalPage;
    private readonly StopThisProviderPage _stopThisProviderPage;

    public ChangeCourseDetailsSteps(ScenarioContext context)
    {
        _context = context;
        _academicProfessionalPage = new AcademicProfessionalPage(context);
        _stopThisProviderPage = new StopThisProviderPage(context);
    }

    private async Task<StopThisProviderPage>OpenStopThisProviderPage(string firstTime,string UKPRN)
    {
        var home = new AcademicProfessionalPage(_context);
        await home.ClickChangeDate(firstTime, UKPRN);
        return await new StopThisProviderPage(_context)
            .VerifyPageAsync( () => new StopThisProviderPage(_context));
    }

    [Given(@"the user clicks to change the details of provider with UKPRN ""(.*)""")]

        public async Task GivenTheUserClicksToChangeTheDetailsOfProviderWithUKPRN(string UKPRN)
        {
            await OpenStopThisProviderPage("yes",UKPRN);
            await _stopThisProviderPage.VerifyPage();
        }

    [When(@"the user clicks to change the details of provider with UKPRN ""(.*)""")]

        public async Task WhenTheUserClicksToChangeTheDetailsOfProviderWithUKPRN(string UKPRN)
        {
            await OpenStopThisProviderPage("no",UKPRN);
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