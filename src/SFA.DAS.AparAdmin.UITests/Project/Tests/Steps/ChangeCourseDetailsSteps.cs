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
    private readonly AddATrainingProviderPage _addATrainingProviderPage;

    public ChangeCourseDetailsSteps(ScenarioContext context)
    {
        _context = context;
        _academicProfessionalPage = new AcademicProfessionalPage(context);
        _stopThisProviderPage = new StopThisProviderPage(context);
        _addATrainingProviderPage = new AddATrainingProviderPage(context);
    }

    private async Task<StopThisProviderPage>OpenStopThisProviderPage(string firstTime,string UKPRN)
    {
        var home = new AcademicProfessionalPage(_context);
        await home.ClickChangeDate(firstTime, UKPRN);
        return await new StopThisProviderPage(_context)
            .VerifyPageAsync( () => new StopThisProviderPage(_context));
    }

    private async Task<AddATrainingProviderPage>ConfirmAddAProviderPage(string UKPRN)
    {
        var home = new AcademicProfessionalPage(_context);
        await home.ProviderToRestrict(UKPRN);
        return await new AddATrainingProviderPage(_context)
            .VerifyPageAsync( () => new AddATrainingProviderPage(_context));
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

    [Given(@"the user clicks to add a training provider")]
    [When(@"the user clicks to add a training provider")]
    
    public async Task GivenTheUserClicksToAddATrainingProvider()
    {
        await _academicProfessionalPage.AddTrainingProvider();
    }

    [When(@"the user submits the UKPRN ""(.*)"" to add")]
    [Then(@"the user submits the UKPRN ""(.*)"" to add")]

    public async Task WhenTheUserSubmitsTheUKPRNToAdd(string UKPRN)
    {
        await ConfirmAddAProviderPage(UKPRN);
    }

    [Then(@"the user is asked to confirm that they want to allow this provider to offer this course and they click confirm")]

    public async Task ThenTheUserIsAskedToConfirmThatTheyWantToAllowThisProviderToOfferThisCourse()
    {
        await _addATrainingProviderPage.VerifyPage();
        await _addATrainingProviderPage.ConfirmAddATrainingProvider();
    }

    [Then(@"the user is asked to confirm that they want to allow this provider to offer this course and they click cancel")]

    public async Task ThenTheUserIsAskedToConfirmThatTheyWantToAllowThisProviderToOfferThisCourseAndTheyClickCancel()
    {
                await _addATrainingProviderPage.VerifyPage();
                await _addATrainingProviderPage.CancelAddATrainingProvider();
        
    }

    [Then(@"it is confirmed that the provider with UKPRN ""(.*)"" has not been added to the list")]

    public async Task ThenItIsConfirmedThatTheProviderWithUKPRNHasNotBeenAddedToTheList(string UKPRN)
    {
        await _academicProfessionalPage.SearchFunctionality(UKPRN);
        await _academicProfessionalPage.VerifyResults(UKPRN,"no");
    }

    [Then(@"it is confirmed that the provider with UKPRN ""(.*)"" has been added to the list")]

    public async Task ThenItIsConfirmedThatTheProviderWithUKPRNHasBeenAddedToTheList(string UKPRN)
    {
        await _academicProfessionalPage.SearchFunctionality(UKPRN);
        await _academicProfessionalPage.VerifyResults(UKPRN,"yes");
    }

    [When(@"the user adds a UKPRN that does not exist ""(.*)")]

    public async Task WhenTheUserAddsAUKPRNThatDoesNotExist( string UKPRN)
    {
        var home = new AddATrainingProviderPage(_context);
        await home.ProviderToRestrict(UKPRN);
    }

    [Then(@"the user gets an errror message")]

    public async Task ThenTheUserGetsAnErrorMessage()
    {
        await _addATrainingProviderPage.ErrorMessage();
    }
}