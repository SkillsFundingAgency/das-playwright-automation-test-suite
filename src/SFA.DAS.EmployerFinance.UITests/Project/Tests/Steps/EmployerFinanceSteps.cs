﻿
using Azure;
using SFA.DAS.EmployerFinance.UITests.Project.Tests.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using System.Threading.Tasks;

namespace SFA.DAS.EmployerFinance.UITests.Project.Tests.Steps;

[Binding]
public class EmployerFinanceSteps(ScenarioContext context)
{
    private FinancePage _financePage;

    [Then(@"'Set up an apprenticeship' section is displayed")]
    public async Task ThenSetUpAnApprnticeshipSectionIsDisplayed() => await new HomePage(context).VerifySetupAnApprenticeshipSection();

    [Then(@"'Your funding reservations' and 'Your finances' links are displayed in the Finances section")]
    public async Task ThenAndLinksAreDisplayedInTheFinancesSection() => await new HomePageFinancesSection_YourFinance(context).VerifyYourFinancesSectionLinksForANonLevyUser();

    [Then(@"'Your finances' link is displayed in the Finances section")]
    public async Task ThenLinkIsDisplayedInTheFinancesSection() => await new HomePageFinancesSection_YourFinance(context).VerifyYourFinancesSectionLinksForALevyUser();

    [When(@"the Employer navigates to 'Finance' Page")]
    public async Task WhenTheEmployerNavigatesFinancePage() => _financePage = await new HomePageFinancesSection_YourFinance(context).NavigateToFinancePage();

    [Then(@"the employer can navigate to recruitment page")]
    public async Task ThenTheEmployerCanNavigateToRecruitment()
    {
        await new InterimFinanceHomePage(context, true).VerifyPage();

        await new InterimYourApprenticeshipAdvertsHomePage(context, true).VerifyPage();
    }

    [Then(@"the employer can navigate to apprentice page")]
    public async Task ThenTheEmployerCanNavigateToApprentice()
    {
        await new InterimFinanceHomePage(context, true).VerifyPage();

        await new InterimApprenticesHomePage(context, false).VerifyPage();
    }

    [Then(@"the employer can navigate to your team page")]
    public async Task ThenTheEmployerCanNavigateToYourTeamPage()
    {
        var page = new InterimFinanceHomePage(context, true, true);

        await page.VerifyPage();

        await page.GotoYourTeamPage();
    }

    [Then(@"the employer can navigate to account settings page")]
    public async Task ThenTheEmployerCanNavigateToAccountSettingsPage()
    {
        var page = new InterimFinanceHomePage(context, true, true);

        await page.VerifyPage();

        var page1 = await page.GoToYourAccountsPage();

        await page1.OpenAccount();
    }

    [Then(@"the employer can navigate to rename account settings page")]
    public async Task ThenTheEmployerCanNavigateToRenameAccountSettingsPage()
    {
        var page = new InterimFinanceHomePage(context, true, true);

        await page.VerifyPage();

        await page.GoToRenameAccountPage();
    }

    [Then(@"the employer can navigate to notification settings page")]
    public async Task ThenTheEmployerCanNavigateToNotificationSettingsPage()
    {
        var page = new InterimFinanceHomePage(context, true, true);

        await page.VerifyPage();

        var page1 = await page.GoToNotificationSettingsPage();

        await page1.GoBackToHomePage();
    }

    [Then(@"the employer can navigate to help settings page")]
    public async Task ThenTheEmployerCanNavigateToHelpSettingsPage()
    {
        var page = new InterimFinanceHomePage(context, true, true);

        await page.VerifyPage();

        await page.GoToHelpPage();
    }

    [Then(@"'View transactions', 'Download transactions' and 'Transfers' links are displayed")]
    public async Task ThenAndLinksAreDisplayed()
    {
        await _financePage.IsViewTransactionsLinkPresent();

        await _financePage.IsDownloadTransactionsLinkPresent();
        
        await _financePage.IsTransfersLinkPresent();
    }

    [Then(@"Employer is able to navigate to 'View transactions', 'Download transactions' and 'Transfers' pages")]
    public async Task ThenEmployerIsAbleToNavigateToAndPages()
    {
        var page = await _financePage.GoToViewTransactionsPage();

        _financePage = await page.GoToFinancePage();

        var page1 = await _financePage.GoToDownloadTransactionsPage();

        _financePage = await page1.GoToFinancePage();

        var page3 = await _financePage.GoToTransfersPage();

        _financePage = await page3.GoToFinancePage();

    }

    [Then(@"Funds data information is diplayed")]
    public async Task ThenFundsDataInformationIsDiplayed()
    {
        await _financePage.GetCurrentFundsLabel();

        await _financePage.GetFundsSpentLabel();

        await _financePage.GetEstimatedPlannedSpendingText();
    }

}