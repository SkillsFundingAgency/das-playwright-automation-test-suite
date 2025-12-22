namespace SFA.DAS.FAA.UITests.Project.Tests.Steps;

[Binding]
public class FAASteps(ScenarioContext context)
{
    private readonly FAAStepsHelper _faaStepsHelper = new(context);

    [Then(@"the status of the Application is shown as '(successful|unsuccessful)' in FAA")]
    public async Task ThenTheStatusOfTheApplicationIsShownAsInFAA(string expectedStatus)
    {
        await _faaStepsHelper.VerifyApplicationStatus(expectedStatus == "successful");
    }

    [Then(@"the applicant can save on vacancy details page before applying for the vacancy")]
    public async Task ThenTheApplicantCanSaveBeforeApplyingForTheVacancy() => await _faaStepsHelper.GoToVacancyDetailsPageThenSaveBeforeApplying();

    [Then(@"the applicant can save vacancy on search results page before applying for the vacancy")]
    public async Task ThenTheApplicantCanSaveVacancyOnSearchResultsPageBeforeApplyingForTheVacancy() => await _faaStepsHelper.GoToSearchResultsPagePageAndSaveBeforeApplying();

    [Then("the Applicant can withdraw the application")]
    public async Task ThenTheApplicantCanWithdrawTheApplication() => await _faaStepsHelper.GoToYourApplicationsPageAndWithdrawAnApplication();

    [Then("the Applicant can withdraw a random application")]
    public async Task ThenTheApplicantCanWithdrawARandomApplication() => await _faaStepsHelper.GoToYourApplicationsPageAndWithdrawARandomApplication();

    [Then("the Applicant can view submitted applications page")]
    public async Task ThenTheApplicantCanViewSubmittedApplicationsPage() => await _faaStepsHelper.GoToYourApplicationsPageAndOpenSubmittedApplicationsPage();

    [Then("the apprentice attempts to delete their account they are notified of application withdrawal")]
    public async Task WhenTheApprenticeAttemptsToDeleteTheirAccountTheyAreNotifiedOfApplicationWithdrawal()
    {
        var page = new SettingPage(context);

        await page.VerifyPage();

        var page1 = await page.DeleteMyAccount();

        var page2 = await page1.ContinueToDeleteMyAccounWithApplication();

        var page3 = await page2.WithdrawBeforeDeletingMyAccount();

        var page4 = await page3.ConfirmDeleteMyAccount();
        
        await page4.VerifyNotification();
    }
}