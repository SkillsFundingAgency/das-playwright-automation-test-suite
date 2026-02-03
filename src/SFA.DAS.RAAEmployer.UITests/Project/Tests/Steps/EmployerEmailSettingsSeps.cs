using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Steps;

[Binding]
public class EmployerEmailSettingsSeps(ScenarioContext context)
{
    [Then(@"the employer sets the email preferences")]
    public async Task ThenTheEmployerSetsTheEmailPreferences()
    {
        var page = await new ManageYourEmailsEmployerPage(context).SelectAndSaveEmailPreferences();
        await page.VerifyEmailSettingsConfirmationBanner();
    }
}
