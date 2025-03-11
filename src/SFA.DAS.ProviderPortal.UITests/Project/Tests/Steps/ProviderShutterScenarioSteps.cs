namespace SFA.DAS.ProviderPortal.UITests.Project.Tests.Steps;

[Binding]
public class ProviderShutterScenarioSteps(ScenarioContext context) : ProviderPortalBaseSteps(context)
{
    [Then(@"the provider can not send a request to the same email")]
    public async Task TheProviderCanNotSendARequestToTheSameEmail() => await InviteSent();

    [Then(@"the provider can not send a request to a different email from the same account")]
    public async Task TheProviderCanNotSendARequestToADifferentEmailFromTheSameAccount()
    {
        eprDataHelper.EmployerEmail = context.GetUser<EPRDeclineRequestUser>().AnotherEmail;

        await InviteSent();
    }

    [Then(@"the provider can not re send the invite to the same email")]
    public async Task TheProviderCanNotReSendTheInviteToTheSameEmail() => await InviteSent();

    [Then(@"the provider can not send an invite to a different email using same aorn and paye")]
    public async Task TheProviderCanNotSendAnInviteToADifferentEmailUsingSameAornAndPaye()
    {
        eprDataHelper.EmployerEmail = $"A_{eprDataHelper.EmployerEmail}";

        var page = await new ViewEmpAndManagePermissionsPage(context).ClickAddAnEmployer();

        var page1 = await page.StartNowToAddAnEmployer();

        var page2 = await page1.EnterNewEmployerEmail();

        var page3 = await page2.SubmitPayeAndContinueToInviteSent();

        var page4 = await page3.GoToEmpAccountDetails();

        await page4.ViewEmployersAndManagePermissionsPage();
    }

    [Then(@"the provider should be shown a shutter page where relationship already exists")]
    public async Task TheProviderShouldBeShownAShutterPageWhereRelationshipAlreadyExists()
    {
        await GoToProviderViewEmployersAndManagePermissions();

        var page = await GoToEmailAccountFoundPage();

        await page.VerifyAlreadyLinkedToThisEmployer();
    }

    [Then(@"the provider should be shown a shutter page where an employer has multiple accounts")]
    public async Task TheProviderShouldBeShownAShutterPageWhereAnEmployerHasMultipleAccounts()
    {
        var user = context.GetUser<EPRMultiAccountUser>();

        await EnterEmployerEmailAndGoToShutterPage(user.Username);
    }

    [Then(@"the provider should be shown a shutter page where an employer has multiple organisations")]
    public async Task ThenTheProviderShouldBeShownAShutterPageWhereAnEmployerHasMultipleOrganisations()
    {
        var user = context.GetUser<EPRMultiOrgUser>();

        await EnterEmployerEmailAndGoToShutterPage(user.Username);
    }

    private async Task EnterEmployerEmailAndGoToShutterPage(string username)
    {
        eprDataHelper.EmployerEmail = username;

        await GoToProviderViewEmployersAndManagePermissions();

        var page = await GoToSearchEmployerEmailPage();

        return await page.EnterEmployerEmailAndGoToContactEmployer();
    }

    private async Task InviteSent()
    {
        var page = await new ViewEmpAndManagePermissionsPage(context).ClickAddAnEmployer();

        var page1 = await page.StartNowToAddAnEmployer();

        var page2 = await page1.EnterEmployerEmailAndGoToInviteSent();

        var page3 = await page2.GoToEmpAccountDetails();

        await page3.ViewEmployersAndManagePermissionsPage();
    }
}
