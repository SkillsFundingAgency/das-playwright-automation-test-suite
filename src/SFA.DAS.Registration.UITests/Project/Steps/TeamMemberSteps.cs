using SFA.DAS.Registration.UITests.Project.Helpers;
using SFA.DAS.Registration.UITests.Project.Pages;
using SFA.DAS.Registration.UITests.Project.Pages.InterimPages;

namespace SFA.DAS.Registration.UITests.Project.Tests.StepDefinitions;

[Binding]
public class TeamMemberSteps
{
    private readonly ScenarioContext _context;
    private readonly ObjectContext _objectContext;
    private readonly RegistrationDataHelper _registrationDataHelper;
    private YourTeamPage _yourTeamPage;
    private readonly AccountSignOutHelper _accountSignOutHelper;
    private readonly AccountCreationStepsHelper _accountCreationStepsHelper;
    private string _invitedMemberEmailId;

    public TeamMemberSteps(ScenarioContext context)
    {
        _context = context;
        _objectContext = _context.Get<ObjectContext>();
        _registrationDataHelper = context.Get<RegistrationDataHelper>();
        _accountSignOutHelper = new AccountSignOutHelper(context);
        _accountCreationStepsHelper = new AccountCreationStepsHelper(context);
    }

    [Then(@"Employer is able to invite a team member with Viewer access")]
    public async Task ThenEmployerIsAbleToInviteATeamMemberWithViewerAccess()
    {
        _invitedMemberEmailId = _registrationDataHelper.AnotherRandomEmail;

        var page = await new HomePage(_context, true).GoToHomePage();

        var page1 = await page.GotoYourTeamPage();

        var page2 = await page1.ClickInviteANewMemberButton();

        await page2.EnterEmailAndFullName(_invitedMemberEmailId);

        var page3 = await page2.SelectViewerAccessRadioButtonAndSendInvitation();

        _yourTeamPage = await page3.ViewAllTeamMembers();
    }

    [Then(@"Employer is able to resend an invite")]
    public async Task ThenEmployerIsAbleToResendAnInvite()
    {
        var page = await _yourTeamPage.ClickViewMemberLink(_invitedMemberEmailId);

        _yourTeamPage = await page.ClickResendInvitationButton();

        await _yourTeamPage.VerifyInvitationResentHeaderInfoMessage();
    }

    [Then(@"Employer is able abort cancelling during cancelling an invite")]
    public async Task ThenEmployerIsAbleAbortCancellingDuringCancellingAnInvite()
    {
        var page = await _yourTeamPage.ClickViewMemberLink(_invitedMemberEmailId);

        var page1 = await page.ClickCancelInvitationLink();

        _yourTeamPage = await page1.ClickNoDontCancelInvitationLink();
    }

    [Then(@"Employer is able to cancel an invite")]
    public async Task ThenEmployerIsAbleToCancelAnInvite()
    {
        var page = await _yourTeamPage.ClickViewMemberLink(_invitedMemberEmailId);

        var page1 = await page.ClickCancelInvitationLink();

        var page2 = await page1.ClickYesCancelInvitationButton();

        await _yourTeamPage.VerifyInvitationCancelledHeaderInfoMessage();
    }

    [Then(@"the invited team member is able to accept the invite and login to the Employer account")]
    public async Task ThenTheInvitedTeamMemberIsAbleToAcceptTheInviteAndLoginToTheEmployerAccount()
    {
        await ThenEmployerIsAbleToInviteATeamMemberWithViewerAccess();

        await _accountCreationStepsHelper.AcceptUserInvite(await SignOut(), _invitedMemberEmailId);
    }

    [Then(@"Employer is able to Remove the team member from the account")]
    public async Task ThenEmployerIsAbleToRemoveTheTeamMemberFromTheAccount()
    {
        var page = await SignOut();

        var page1 = await page.GoToStubSignInPage();

        var page2 = await page1.Login(_objectContext.GetLoginCredentials());

        var page3 = await page2.ContinueToHomePage();

        var page4 = await page3.GotoYourTeamPage();

        var page5 = await page4.ClickViewMemberLink(_invitedMemberEmailId);

        var page6 = await page5.ClickRemoveTeamMemberButton();

        var page7 = await page6.ClickYesRemoveNowButton();

        await _yourTeamPage.VerifyTeamMemberRemovedHeaderInfoMessage();
    }

    private async Task<CreateAnAccountToManageApprenticeshipsPage> SignOut() => await _accountSignOutHelper.SignOut();
}
