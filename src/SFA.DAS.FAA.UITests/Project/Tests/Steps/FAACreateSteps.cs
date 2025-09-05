using SFA.DAS.FAA.UITests.Project.Tests.Pages;

namespace SFA.DAS.FAA.UITests.Project.Tests.Steps;

[Binding]
public class FAACreateSteps(ScenarioContext context)
{
    private readonly FAAStepsHelper _faaStepsHelper = new(context);

    [Given(@"appretince creates an account")]
    public async Task GivenAppretinceCreatesAnAccount()
    {
        await _faaStepsHelper.SubmitNewUserDetails();

        var page = await new FAA_CreateAccountPage(context).CreateAnAccount();

        var page1 = await page.SubmitApprenticeName();

        var page2 = await page1.SubmitApprenticeDateOfBirth();

        var page3 = await page2.SubmitApprenticePostCode();

        var page4 = await page3.SubmitApprenticeTelephoneNumber();

        var page5 = await page4.SelectRemindersNotification();

        await page5.ClickCreateYourAccountConfirmation();
    }

    [Then(@"apprentice is able to delete account")]
    public async Task ThenApprenticeIsAbleToDeleteAccount()
    {
        var page = new SettingPage(context);

        await page.VerifyPage();

        var page1 = await page.DeleteMyAccount();

        var page2 = await page1.ContinueDeleteMyAccount();

        var page3 = await page2.ConfirmDeleteMyAccount();

        await page3.VerifyNotification();
    }

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

    [When("the apprentice has submitted their first application")]
    public async Task GivenTheApprenticeHasSubmittedTheirFirstApplication()
    {
        var page = await _faaStepsHelper.ApplyForAVacancyWithNewAccount(true, true, true, true, true, true);

        var page1 = await page.PreviewApplication();

        await page1.SubmitApplication();
    }

}
