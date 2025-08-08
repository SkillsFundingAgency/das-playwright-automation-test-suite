using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Admin;

[Binding, Scope(Tag = "@aanadmin")]
public class Admin_Notifications_Steps(ScenarioContext context) : AanBaseSteps(context)
{

    [Given(@"user select notification settings on dashboard")]
    public async Task GivenUserSelectNotificationSettingsOnDashboard()
    {
        await new AdminAdministratorHubPage(context)
            .ManageNotifications();
    }

    [Then(@"the user select Yes for emails and confirm notification saved")]
    public async Task WhenTheUserSelectYesForEmails()
    {
        await new NotificationsSettingsPage(context)
            .SelectYesAndSave();
    }

    [Then(@"the user select No for emails and confirm notification saved")]
    public async Task ThenTheUserSelectNoForEmailsAndConfirmNotificationSaved()
    {
        await new NotificationsSettingsPage(context)
            .SelectNoAndSave();
    }
}
