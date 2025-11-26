using SFA.DAS.ProviderLogin.Service.Project.Pages;
using SFA.DAS.ProviderPortal.UITests.Project.Pages;

namespace SFA.DAS.ProviderPortal.UITests.Project.Tests.Steps;

[Binding]
public class ProviderRolesSteps(ScenarioContext context)
{
    private ProviderHomePage providerHomePage;

    [Then(@"user can access Notification Settings page")]
    public async Task UserCanAccessNotificationSettingsPage()
    {
        providerHomePage = new ProviderHomePage(context);

        await providerHomePage.GotoAddNewEmployerStartPage();

        var page1 = await providerHomePage.GoToProviderNotificationSettingsPage();

        providerHomePage = await page1.ClickCancel();
    }

    [Then("user can access Notification Settings page as viewer")]
    public async Task ThenUserCanAccessNotificationSettingsPageAsViewer()
    {
        providerHomePage = new ProviderHomePage(context);

        await providerHomePage.ClickAddAnEmployerLink();

        var page1 = await providerHomePage.GoToProviderNotificationSettingsPage();

        providerHomePage = await page1.ClickCancel();
    }

    [Then(@"user can access Orgs And Agreements page")]
    public async Task UserCanAccessProviderOrganisationsAndAgreementsPage()
    {
        var page = await providerHomePage.GoToProviderEmployersAndPermissionsPagePage();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can access Help page")]
    public async Task UserCanAccessHelpPage()
    {
        var page = await providerHomePage.GoToManageApprenticeshipsServiceHelpPage();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can access Feedback page")]
    public async Task UserCanAccessFeedbackPage()
    {
        await providerHomePage.VerifyProviderFooterFeedbackPage();
    }

    [Then(@"user can access Privacy Statement page")]
    public async Task UserCanAccessPrivacyStatementPage()
    {
        var page = await providerHomePage.GoToProviderFooterPrivacyPage();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can access Cookies page")]
    public async Task UserCanAccessCookiesPage()
    {
        var page = await providerHomePage.GoToProviderFooterCookiesPage();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can access Terms Of Use page")]
    public async Task UserCanAccessTermsOfUsePage()
    {

        var page = await providerHomePage.GoToProviderFooterTermsOfUsePage();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can signout from their account")]
    public async Task UserCanSignoutFromTheirAccount()
    {
        await providerHomePage.SignsOut();
    }

    [Then(@"user can view Add New Apprentices page as defined in the table below (.*)")]
    public async Task UserCanOrCannotViewAddNewApprenticesPageAsDefinedInTable(bool canAccess)
    {
        var page = new ProviderHomePage(context);

        if (canAccess)
        {
            var page1 = await page.GotoSelectJourneyPage();

            providerHomePage = await page1.GoToProviderHomePage();
        }
        else
        {
            var page1 = await page.GotoSelectJourneyPageGoesToAccessDenied();

            providerHomePage = await page1.GoBackToTheServiceHomePage();
        }
    }

    [Then(@"user can view Add An Employer page as defined in the table below (.*)")]
    public async Task UserCanOrCannotViewAddAnEmployerPageAsDefinedInTable(bool canAccess)
    {
        if (canAccess)
        {
            var page1 = await providerHomePage.GotoAddNewEmployerStartPage();

            providerHomePage = await page1.GoToProviderHomePage();
        }
        else
        {
            var page1 = await providerHomePage.GotoAddNewEmployerStartPageGoesToAccessDenied();

            await page1.NavigateBrowserBack();

            providerHomePage = new ProviderHomePage(context);

            await providerHomePage.VerifyPage();
        }
    }

    [Then(@"user can view Get Funding For NonLevy Employers page as defined in the table below (.*)")]
    public async Task UserCanOrCannotViewGetFundingNonLevyEmployersPageAsDefinedInTable(bool canAccess)
    {
        if (canAccess)
        {
            var page1 = await providerHomePage.GoToProviderGetFunding();

            providerHomePage = await page1.GoToProviderHomePage();
        }
        else
        {
            var page1 = await providerHomePage.GoToProviderGetFundingGoesToAccessDenied();

            await page1.NavigateBrowserBack();

            providerHomePage = new ProviderHomePage(context);

            await providerHomePage.VerifyPage();
        }
    }

    [Then(@"user can view View Employers And Manage Permissions page")]
    public async Task UserCanViewEmployersAndManagePermissionsPage()
    {
        await providerHomePage.ClickViewEmployersAndManagePermissionsLink();

        var page = new ViewEmpAndManagePermissionsPage(context);

        await page.VerifyPage();

        providerHomePage = new ProviderHomePage(context);

        await providerHomePage.GoToProviderHomePage();
    }

    [Then(@"user can view Apprentice Requests page")]
    public async Task UserCanViewApprenticeRequestsPage()
    {
        var page = await providerHomePage.GoToApprenticeRequestsPage();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can view Manage Your Funding Reserved For NonLevy Employers page")]
    public async Task UserCanViewManageYourFundingReservedForNonLevyEmployersPage()
    {
        var page = await providerHomePage.GoToManageYourFunding();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can view Manage Your Apprentices page")]
    public async Task UserCanViewManageYourApprenticesPage()
    {
        var page = await providerHomePage.GoToProviderManageYourApprenticePage();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can view Recruit Apprentices page")]
    public async Task UserCanViewRecruitApprenticesPage()
    {
        var page = await providerHomePage.GoToProviderRecruitApprenticesHomePage();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can view Your Standards And Training Venues page")]
    public async Task UserCanViewYourStandardsAndTrainingVenuesPage()
    {
        var page = await providerHomePage.NavigateToYourStandardsAndTrainingVenuesPage();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user cannot view Your Standards And Training Venues page")]
    public async Task UserCannotViewYourStandardsAndTrainingVenuesPage()
    {
        var page = await providerHomePage.NavigateToShutterPage_EmployerTypeProviderPage();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can view Developer APIs page as defined in the table below (.*)")]
    public async Task UserCanViewDeveloperAPIsPage(bool canAccess)
    {

        if (canAccess)
        {
            var page1 = await providerHomePage.NavigateToDeveloperAPIsPage();

            providerHomePage = await page1.GoToProviderHomePage();
        }
        else
        {
            var page1 = await providerHomePage.NavigateToDeveloperAPIsPageGoesToApimAccessDenied();

            providerHomePage = await page1.GoBackToTheServiceHomePage();
        }
    }

    [Then(@"user can view Your Feedback page")]
    public async Task UserCanViewYourFeedbackPage()
    {
        var page = await providerHomePage.NavigateToYourFeedback();

        providerHomePage = await page.GoToProviderHomePage();
    }

    [Then(@"user can view View Employer Requests For Training page")]
    public async Task UserCanViewEmployerRequestsForTrainingPage()
    {
        var page = await providerHomePage.NavigateToViewEmployerRequestsForTrainingPage();

        providerHomePage = await page.GoToProviderHomePage();
    }
}
