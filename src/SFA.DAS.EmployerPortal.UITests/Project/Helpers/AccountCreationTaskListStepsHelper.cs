using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.StubPages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers;

public class AccountCreationTaskListStepsHelper(ScenarioContext context)
{
    private readonly EmployerPortalDataHelper _employerPortalDataHelper = context.Get<EmployerPortalDataHelper>();

    internal async Task<ConfirmYourUserDetailsPage> UserEntersNameAndContinue(StubAddYourUserDetailsPage stubUserDetailsPage) => await stubUserDetailsPage.EnterNameAndContinue(_employerPortalDataHelper);

    internal async Task<ConfirmYourUserDetailsPage> UserChangesUserDetails(ConfirmYourUserDetailsPage confirmDetailsPage)
    {
        var page = await confirmDetailsPage.ClickChange();

        return await page.EnterNameAndContinue(_employerPortalDataHelper);
    }

    internal static async Task<CreateYourEmployerAccountPage> UserClicksContinueButtonToAcknowledge(ConfirmYourUserDetailsPage confirmDetailsPage)
    {
        var page = await confirmDetailsPage.ConfirmNameAndContinue();

        return await page.ClickContinueButtonToAcknowledge();
    }


    internal static async Task<CreateYourEmployerAccountPage> UserChangesDetailsFromTaskList(CreateYourEmployerAccountPage confirmDetailsPage)
    {
        var page = await confirmDetailsPage.GoToAddYouUserDetailsLink();

        var page1 = await page.EnterName();

        var page2 = await page1.ConfirmNameAndContinue(true);

        return await page2.ClickContinueButtonToAcknowledge();
    }

    internal static async Task<CreateYourEmployerAccountPage> UserCanClickAddAPAYEScheme(CreateYourEmployerAccountPage createEmployerAccountPage)
    {
        var page = await createEmployerAccountPage.GoToAddPayeLink();

        return await page.GoBackToCreateYourEmployerAccountPage();
    }

    internal static async Task<CreateYourEmployerAccountPage> UserCannotAmendPAYEScheme(CreateYourEmployerAccountPage createEmployerAccountPage)
    {
        var page = await createEmployerAccountPage.GoToAddPayeLinkWhenAlreadyAdded();

        return await page.GoBackToCreateYourEmployerAccountPage();
    }

    internal static async Task<CreateYourEmployerAccountPage> UserCanClickAddAccountName(CreateYourEmployerAccountPage createEmployerAccountPage)
    {
        var page = await createEmployerAccountPage.GoToSetYourAccountNameLink();

        return await page.GoBackToCreateYourEmployerAccountPage();
    }

    internal static async Task<CreateYourEmployerAccountPage> UserCanClickAcceptEmployerAgreement(CreateYourEmployerAccountPage createEmployerAccountPage)
    {
        var page = await createEmployerAccountPage.GoToYourEmployerAgreementLink();

        return await page.GoBackToCreateYourEmployerAccountPage();
    }

    internal static async Task<CreateYourEmployerAccountPage> UserAcknowledgesEmployerAgreement(CreateYourEmployerAccountPage createEmployerAccountPage)
    {
        var page = await createEmployerAccountPage.GoToYourEmployerAgreementLink();

        var page1 = await page.ClickContinueToYourAgreementButtonToDoYouAcceptTheEmployerAgreementPage();

        return await page1.DoNotSignAgreement();
    }

    internal static async Task<CreateYourEmployerAccountPage> UserCanClickTrainingProvider(CreateYourEmployerAccountPage createEmployerAccountPage)
    {
        var page = await createEmployerAccountPage.GoToTrainingProviderLink();

        return await page.GoBackToCreateYourEmployerAccountPage();
    }
    internal static async Task<CreateYourEmployerAccountPage> AddPAYEFromTaskListForCloseTo3Million(CreateYourEmployerAccountPage createEmployerAccountPage)
    {
        var page = await createEmployerAccountPage.GoToAddPayeLink();

        var page1 = await page.SelectOptionCloseTo3Million();

        var page2 = await page1.AgreeAndContinue();

        var page3 = await page2.SignInTo(0);

        var page4 = await page3.SearchForAnOrganisation();

        var page5 = await page4.SelectYourOrganisation();

        var page6 = await page5.ClickYesThisIsMyOrg();

        return await page6.ContinueToConfirmationPage();
    }

    internal static async Task<CreateYourEmployerAccountPage> ConfirmEmployerAccountName(CreateYourEmployerAccountPage createEmployerAccountPage)
    {
        var page = await createEmployerAccountPage.GoToSetYourAccountNameLink();

        var page1 = await page.SelectoptionToSkipNameChange();

        return await page1.ContinueToAcknowledge();
    }

    internal async Task<CreateYourEmployerAccountPage> UpdateEmployerAccountName(CreateYourEmployerAccountPage createEmployerAccountPage)
    {
        var page = await createEmployerAccountPage.GoToSetYourAccountNameLink();

        var page1 = await page.SelectoptionToChangeAccountName(_employerPortalDataHelper.CompanyTypeOrg2);

        var page2 = await page1.ContinueToAcknowledge(_employerPortalDataHelper.CompanyTypeOrg2);

        return await page2.ContinueToCreateYourEmployerAccountPage();
    }

    internal static async Task<CreateYourEmployerAccountPage> AcceptEmployerAgreement(CreateYourEmployerAccountPage createEmployerAccountPage)
    {
        var page = await createEmployerAccountPage.GoToYourEmployerAgreementLink();

        var page1 = await page.ClickContinueToYourAgreementButtonToDoYouAcceptTheEmployerAgreementPage();

        var page2 = await page1.SignAgreementFromCreateAccountTasks();

        return await page2.SelectContinueToCreateYourEmployerAccount();
    }

    internal static async Task<HomePage> AddTrainingProviderAndGrantPermission(CreateYourEmployerAccountPage createEmployerAccountPage, ProviderConfig providerConfig)
    {
        var page = await createEmployerAccountPage.GoToTrainingProviderLink();

        var page1 = await page.AddTrainingProviderNow();

        var page2 = await page1.SearchForATrainingProvider(providerConfig);

        var page3 = await page2.AddOrSetPermissionsAndCreateAccount((AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprentices));

        return await page3.SelectGoToYourEmployerAccountHomepage();
    }

}
