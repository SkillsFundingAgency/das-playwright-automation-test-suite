using SFA.DAS.ProviderLogin.Service.Project.Pages;
namespace SFA.DAS.ProviderPortal.UITests.Project.Tests.Steps;

[Binding]
public class ProviderRolesSteps(ScenarioContext context)
{
    private ProviderHomePage providerHomePage;

    [Then(@"user can access Notification Settings page")]
    public async Task UserCanAccessNotificationSettingsPage()
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
        //if (canAccess)
        //    _providerStepsHelper.NavigateToProviderHomePage().GotoSelectJourneyPage();
        //else
        //    _providerStepsHelper.NavigateToProviderHomePage()
        //        .GotoSelectJourneyPageGoesToAccessDenied()
        //        .GoBackToTheServiceHomePage();
    }

    [Then(@"user can view Add An Employer page as defined in the table below (.*)")]
    public async Task UserCanOrCannotViewAddAnEmployerPageAsDefinedInTable(bool canAccess)
    {
        //if (canAccess)
        //    _providerStepsHelper.NavigateToProviderHomePage().GotoAddNewEmployerStartPage();
        //else
        //    _providerStepsHelper.NavigateToProviderHomePage()
        //        .GotoAddNewEmployerStartPageGoesToAccessDenied()
        //        .NavigateBrowserBack();
    }

    [Then(@"user can view Get Funding For NonLevy Employers page as defined in the table below (.*)")]
    public async Task UserCanOrCannotViewGetFundingNonLevyEmployersPageAsDefinedInTable(bool canAccess)
    {
        //if (canAccess)
        //    _providerStepsHelper.NavigateToProviderHomePage().GoToProviderGetFunding();
        //else
        //    _providerStepsHelper.NavigateToProviderHomePage()
        //        .GoToProviderGetFundingGoesToAccessDenied()
        //        .NavigateBrowserBack();
    }

    [Then(@"user can view View Employers And Manage Permissions page")]
    public async Task UserCanViewEmployersAndManagePermissionsPage()
    {
        //_providerStepsHelper.NavigateToProviderHomePage().GotoViewEmpAndManagePermissionsPage();
    }

    [Then(@"user can view Apprentice Requests page")]
    public async Task UserCanViewApprenticeRequestsPage()
    {
        //_providerStepsHelper.NavigateToProviderHomePage().GoToApprenticeRequestsPage();
    }

    [Then(@"user can view Manage Your Funding Reserved For NonLevy Employers page")]
    public async Task UserCanViewManageYourFundingReservedForNonLevyEmployersPage()
    {
        //_providerStepsHelper.NavigateToProviderHomePage().GoToManageYourFunding();
    }

    [Then(@"user can view Manage Your Apprentices page")]
    public async Task UserCanViewManageYourApprenticesPage()
    {
        //_providerStepsHelper.NavigateToProviderHomePage().GoToProviderManageYourApprenticePage();
    }

    [Then(@"user can view Recruit Apprentices page")]
    public async Task UserCanViewRecruitApprenticesPage()
    {
        //_providerStepsHelper.NavigateToProviderHomePage().GoToProviderRecruitApprenticesHomePage();
    }

    [Then(@"user can view Your Standards And Training Venues page")]
    public async Task UserCanViewYourStandardsAndTrainingVenuesPage()
    {
        //_providerStepsHelper.NavigateToProviderHomePage().NavigateToYourStandardsAndTrainingVenuesPage();
    }

    [Then(@"user can view Developer APIs page as defined in the table below (.*)")]
    public async Task UserCanViewDeveloperAPIsPage(bool canAccess)
    {
        //if (canAccess) 
        //_providerStepsHelper.NavigateToProviderHomePage().NavigateToDeveloperAPIsPage();
        //else
        //    _providerStepsHelper.NavigateToProviderHomePage()
        //        .NavigateToDeveloperAPIsPageGoesToApimAccessDenied()
        //        .GoBackToTheServiceHomePage();
    }

    [Then(@"user can view Your Feedback page")]
    public async Task UserCanViewYourFeedbackPage()
    {
        //_providerStepsHelper.NavigateToProviderHomePage().NavigateToYourFeedback();
    }

    [Then(@"user can view View Employer Requests For Training page")]
    public async Task UserCanViewEmployerRequestsForTrainingPage()
    {
        //_providerStepsHelper.NavigateToProviderHomePage().NavigateToViewEmployerRequestsForTrainingPage();
    }
}
