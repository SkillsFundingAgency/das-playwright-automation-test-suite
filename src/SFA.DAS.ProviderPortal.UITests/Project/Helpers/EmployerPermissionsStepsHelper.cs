using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.ProviderPortal.UITests.Project.Helpers;

public class EmployerPermissionsStepsHelper(ScenarioContext context)
{
    public async Task<HomePage> SetAllProviderPermissions(ProviderConfig providerConfig) => await SetProviderPermissions(providerConfig, (AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprentices));

    public async Task<HomePage> SetCreateCohortProviderPermissions(ProviderConfig providerConfig) => await SetProviderPermissions(providerConfig, (AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.NoToRecruitApprentices));

    public async Task<HomePage> RemoveAllProviderPermission(ProviderConfig providerConfig) => await UpdateProviderPermission(providerConfig, (AddApprenticePermissions.NoToAddApprenticeRecords, RecruitApprenticePermissions.NoToRecruitApprentices));

    private async Task<HomePage> SetProviderPermissions(ProviderConfig providerConfig, (AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permissions)
    {
        var page = OpenProviderPermissions();

        var page1 = await page.SelectAddATrainingProvider();

        var page2 = await page1.SearchForATrainingProvider(providerConfig);

        var page3 = await page2.AddOrSetPermissions(permissions);

        var page4 = await page3.VerifyYouHaveAddedNotification(providerConfig.Name);

        return await page4.GoToHomePage();
    }

    public async Task<HomePage> AcceptOrDeclineProviderRequest(RequestType requestType, ProviderConfig providerConfig, string requestId, bool accept)
    {
        var page = OpenProviderPermissions();

        AddOrReviewRequestFromProvider page1 = requestType == RequestType.Permission ? await page.ReviewProviderRequests(providerConfig, requestId) : await page.ViewProviderRequests(providerConfig, requestId);

        EmployerPortalBasePage registrationBasePage;

        if (requestType == RequestType.Permission)
        {
            if (accept)
            {
                var page2 = await page1.AcceptProviderRequest();

                registrationBasePage = await page2.VerifyYouHaveSetPermissionNotification(providerConfig.Name);
            }
            else
            {
                var page2 = await page1.DeclinePermissionRequest();

                registrationBasePage = await page2.VerifyYouHaveDeclinedNotification(providerConfig.Name);
            }
        }

        else
        {
            if (accept)
            {
                var page2 = await page1.AcceptProviderRequest();

                registrationBasePage = await page2.VerifyYouHaveAddedNotification(providerConfig.Name);
            }
            else
            {
                var page2 = await page1.DeclineAddRequest();

                registrationBasePage = await page2.ConfirmDeclineRequest();
            }
        }

        return await registrationBasePage.GoToHomePage();
    }

    internal async Task<HomePage> UpdateProviderPermission(ProviderConfig providerConfig, (AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permissions)
    {
        var page = await OpenProviderPermissions();

        var page1 = await page.SelectChangePermissions(providerConfig.Ukprn);

        var page2 = await page1.AddOrSetPermissions(permissions);

        var page3 = await page2.VerifyYouHaveSetPermissionNotification();

        return await page3.GoToHomePage();
    }

    internal async Task<ManageTrainingProvidersPage> OpenProviderPermissions() => await new ManageTrainingProvidersLinkHomePage(context).OpenRelationshipPermissions();

}
