using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.ProviderPortal.UITests.Project.Helpers;

namespace SFA.DAS.ProviderPortal.UITests.Project.Tests.Steps;

[Binding]
public class EmployerRelationSteps(ScenarioContext context) : ProviderPortalBaseSteps(context)
{
    [Given(@"Levy employer grants all permission to a provider")]
    public async Task LevyEmployerGrantsAllPermissionToAProvider()
    {
        permissions = (AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprentices);

        await EPRLevyUserLogin();

        await _employerPermissionsStepsHelper.SetAllProviderPermissions(providerConfig);
    }

    [When(@"the employer changes recruit apprentice permission")]
    public async Task TheEmployerChangesRecruitApprenticePermission()
    {
        await EmployerUpdatePermission((AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprenticesButEmployerWillReview));
    }

    [When(@"the employer revokes all provider permissions")]
    public async Task TheEmployerRevokesAllProviderPermissions()
    {
        await EmployerUpdatePermission((AddApprenticePermissions.NoToAddApprenticeRecords, RecruitApprenticePermissions.NoToRecruitApprentices));
    }

    [Then(@"an employer has to select at least one permission")]
    public async Task ThenAnEmployerHasToSelectAtLeastOnePermission()
    {
        await EPRLevyUserLogin();

        var page = await new ManageTrainingProvidersLinkHomePage(context).OpenRelationshipPermissions();

        var page1 = await page.SelectAddATrainingProvider();

        var page2 = await page1.SearchForATrainingProvider(providerConfig);

        await page2.VerifyDoNotAllowPermissions();
    }

    [Then(@"the correct permissions should be displayed in the employer portal")]
    public async Task TheCorrectPermissionsShouldBeDisplayedInTheEmployerPortal()
    {
        var page = await _employerPermissionsStepsHelper.OpenProviderPermissions();

        var actual = await page.GetPermissions(providerConfig);

        Assert.Multiple(() =>
        {
            StringAssert.Contains(EnumToString.GetStringValue(permissions.AddApprentice), actual, "Incorrect add apprentice permission trainning provider page");

            StringAssert.Contains(EnumToString.GetStringValue(permissions.RecruitApprentice), actual, "Incorrect add apprentice permission trainning provider page");
        });

        await page.GoToHomePage();
    }

    [Then(@"the employer is unable to add an existing provider")]
    public async Task ThenTheEmployerIsUnableToAddAnExistingProvider()
    {
        var page = await new ManageTrainingProvidersLinkHomePage(context).OpenRelationshipPermissions();

        var page1 = await page.SelectAddATrainingProvider();

        var page2 = await page1.SearchForAnExistingTrainingProvider(providerConfig);

        await page2.CannotAddExistingTrainingProvider();
    }

    [Then(@"the employer accepts the add account request")]
    public async Task TheEmployerAcceptsTheAddAccountRequest()
    {
        await EPRReLoginAcceptOrDeclineProviderPermissionsRequest(RequestType.AddAccount, true);
    }

    [Then(@"the employer declines the add account request")]
    public async Task TheEmployerDeclinesTheAddAccountRequest()
    {
        eprDataHelper.EmployerEmail = context.GetUser<EPRDeclineRequestUser>().Username;

        await EPRReLoginAcceptOrDeclineProviderPermissionsRequest(RequestType.AddAccount, false);
    }

    [Then(@"the employer declines the update permission request")]
    public async Task TheEmployerDeclinesTheUpdatePermissionRequest()
    {
        await EPRReLoginAcceptOrDeclineProviderPermissionsRequest(RequestType.Permission, false);
    }

    [Then(@"the employer accepts the update permission request")]
    public async Task TheEmployerAcceptsTheUpdatePermissionRequest()
    {
        await EPRReLoginAcceptOrDeclineProviderPermissionsRequest(RequestType.Permission, true);
    }

    private async Task EPRReLoginAcceptOrDeclineProviderPermissionsRequest(RequestType requestType, bool doesAllow)
    {
        await EPRReLogin(requestType);

        var homePage = await _employerPermissionsStepsHelper.AcceptOrDeclineProviderRequest(requestType, providerConfig, eprDataHelper.LatestRequestId, doesAllow);

        var page = await homePage.SignOut();
    }

}