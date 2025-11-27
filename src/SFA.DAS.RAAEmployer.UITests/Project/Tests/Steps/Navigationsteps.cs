using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using SFA.DAS.RAAEmployer.UITests.Project.Helpers;
using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Steps;

[Binding]
public class Navigationsteps
{
    private readonly ScenarioContext _context;
    private readonly RAAEmployerLoginStepsHelper _rAAEmployerLoginHelper;

    public Navigationsteps(ScenarioContext context)
    {
        _context = context;
        _rAAEmployerLoginHelper = new RAAEmployerLoginStepsHelper(_context);
    }

    [Given(@"the Employer navigates to 'Recruit' Page")]
    [When(@"the Employer navigates to 'Recruit' Page")]
    public async Task WhenTheEmployerNavigatesToPage() => await _rAAEmployerLoginHelper.GoToRecruitmentHomePage();

    [Then(@"the employer can navigate to finance page")]
    public async Task ThenTheEmployerCanNavigateToFinancePage()
    {
        await new InterimYourApprenticeshipAdvertsHomePage(_context, true).VerifyPage();

        await new InterimFinanceHomePage(_context, true).VerifyPage();
    }

    [Then(@"the employer can navigate to apprentice page")]
    public async Task ThenTheEmployerCanNavigateToApprenticePage()
    {
        await new InterimYourApprenticeshipAdvertsHomePage(_context, true).VerifyPage();

        await new InterimApprenticesHomePage(_context, false).VerifyPage();
    }

    [Then(@"the employer can navigate to your team page")]
    public async Task ThenTheEmployerCanNavigateToYourTeamPage() => await new InterimYourApprenticeshipAdvertsHomePage(_context, true, true).GotoYourTeamPage();

    [Then(@"the employer can navigate to account settings page")]
    public async Task ThenTheEmployerCanNavigateToAccountSettingsPage() => await new InterimYourApprenticeshipAdvertsHomePage(_context, true, true).GoToYourAccountsPage();

    [Then(@"the employer can navigate to rename account settings page")]
    public async Task ThenTheEmployerCanNavigateToRenameAccountSettingsPage() => await new InterimYourApprenticeshipAdvertsHomePage(_context, true, true).GoToRenameAccountPage();

    [Then(@"the employer can navigate to notification settings page")]
    public async Task ThenTheEmployerCanNavigateToNotificationSettingsPage() => await new InterimYourApprenticeshipAdvertsHomePage(_context, true, true).GoToNotificationSettingsPage();

    [Then(@"the employer can navigate to advert notifications page via settings dropdwon")]
    public async Task ThenTheEmployerCanNavigateToAdvertNotificationsPageViaSettingsDropdwon() => await new YourApprenticeshipAdvertsHomePage(_context, true, true).GoToAdvertNotificationsPage();

    [Then(@"the employer can navigate to help settings page")]
    public async Task ThenTheEmployerCanNavigateToHelpSettingsPage() => await new InterimYourApprenticeshipAdvertsHomePage(_context, true, true).GoToHelpPage();

}
