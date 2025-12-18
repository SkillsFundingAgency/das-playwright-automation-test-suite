using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using SFA.DAS.RAAEmployer.UITests.Project.Helpers;
using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Steps;

[Binding]
public class Navigationsteps(ScenarioContext context)
{

    [Given(@"the Employer navigates to 'Recruit' Page")]
    [When(@"the Employer navigates to 'Recruit' Page")]
    public async Task WhenTheEmployerNavigatesToPage() => await new RAAEmployerLoginStepsHelper(context).GoToRecruitmentHomePage();

    [Then(@"the employer can navigate to finance page")]
    public async Task ThenTheEmployerCanNavigateToFinancePage()
    {
        await VerifyPageHelper.VerifyPageAsync(context, () => new InterimYourApprenticeshipAdvertsHomePage(context, true));

        await VerifyPageHelper.VerifyPageAsync(context, () => new InterimFinanceHomePage(context, true));
    }

    [Then(@"the employer can navigate to apprentice page")]
    public async Task ThenTheEmployerCanNavigateToApprenticePage()
    {
        await VerifyPageHelper.VerifyPageAsync(context, () => new InterimYourApprenticeshipAdvertsHomePage(context, true));

        await VerifyPageHelper.VerifyPageAsync(context, () => new InterimApprenticesHomePage(context, true));
    }

    [Then(@"the employer can navigate to your team page")]
    public async Task ThenTheEmployerCanNavigateToYourTeamPage()
    {
        var page = await VerifyPageHelper.VerifyPageAsync(context, () => new InterimYourApprenticeshipAdvertsHomePage(context, true, true));

        await page.GotoYourTeamPage();
    }

    [Then(@"the employer can navigate to account settings page")]
    public async Task ThenTheEmployerCanNavigateToAccountSettingsPage()
    {
        var page = await VerifyPageHelper.VerifyPageAsync(context, () => new InterimYourApprenticeshipAdvertsHomePage(context, true, true));

        await page.GoToYourAccountsPage();
    }

    [Then(@"the employer can navigate to rename account settings page")]
    public async Task ThenTheEmployerCanNavigateToRenameAccountSettingsPage()
    {
        var page = await VerifyPageHelper.VerifyPageAsync(context, () => new InterimYourApprenticeshipAdvertsHomePage(context, true, true));

        await page.GoToRenameAccountPage();
    }

    [Then(@"the employer can navigate to notification settings page")]
    public async Task ThenTheEmployerCanNavigateToNotificationSettingsPage()
    {
        var page = await VerifyPageHelper.VerifyPageAsync(context, () => new InterimYourApprenticeshipAdvertsHomePage(context, true, true));

        await page.GoToNotificationSettingsPage();
    }

    [Then(@"the employer can navigate to advert notifications page via settings dropdwon")]
    public async Task ThenTheEmployerCanNavigateToAdvertNotificationsPageViaSettingsDropdwon()
    {
        var page = await VerifyPageHelper.VerifyPageAsync(context, () => new YourApprenticeshipAdvertsHomePage(context, true, true));

        await page.GoToAdvertNotificationsPage();
    }

    [Then(@"the employer can navigate to help settings page")]
    public async Task ThenTheEmployerCanNavigateToHelpSettingsPage()
    {
        var page = await VerifyPageHelper.VerifyPageAsync(context, () => new InterimYourApprenticeshipAdvertsHomePage(context, true, true));

        await page.GoToHelpPage();
    }
}
