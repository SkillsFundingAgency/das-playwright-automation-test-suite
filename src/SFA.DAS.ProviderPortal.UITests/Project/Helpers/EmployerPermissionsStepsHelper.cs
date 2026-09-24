using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.ProviderPortal.UITests.Project.Helpers;

public class EmployerPermissionsStepsHelper(ScenarioContext context)
{
    public async Task<HomePage> SetAllProviderPermissions(ProviderConfig providerConfig) => await SetProviderPermissions(providerConfig, (AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprentices));

    public async Task<HomePage> SetCreateCohortProviderPermissions(ProviderConfig providerConfig) => await SetProviderPermissions(providerConfig, (AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.NoToRecruitApprentices));

    public async Task<HomePage> RemoveAllProviderPermission(ProviderConfig providerConfig) => await UpdateProviderPermission(providerConfig, (AddApprenticePermissions.NoToAddApprenticeRecords, RecruitApprenticePermissions.NoToRecruitApprentices));

    private async Task<HomePage> SetProviderPermissions(ProviderConfig providerConfig, (AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permissions)
    {
        var page = await OpenProviderPermissions();

        var page1 = await page.SelectAddATrainingProvider();

        var page2 = await page1.SearchForATrainingProvider(providerConfig);

        var page3 = await page2.AddOrSetPermissions(permissions);

        await page3.VerifyYouHaveAddedNotification(providerConfig.Name);

        return await page3.GoToHomePage();
    }

    public async Task<HomePage> AcceptOrDeclineProviderRequest(RequestType requestType, ProviderConfig providerConfig, string requestId, bool accept)
    {
        var page = await OpenProviderPermissions();

        AddOrReviewRequestFromProvider page1 = requestType == RequestType.Permission ? await page.ReviewProviderRequests(providerConfig, requestId) : await page.ViewProviderRequests(providerConfig, requestId);

        if (requestType == RequestType.Permission)
        {
            if (accept)
            {
                var page2 = await page1.AcceptProviderRequest();

                await page2.VerifyYouHaveSetPermissionNotification(providerConfig.Name);

                return await page2.GoToHomePage();
            }
            else
            {
                var page2 = await page1.DeclinePermissionRequest();

                await page2.VerifyYouHaveDeclinedNotification(providerConfig.Name);

                return await page2.GoToHomePage();
            }
        }

        else
        {
            if (accept)
            {
                var page2 = await page1.AcceptProviderRequest();

                await page2.VerifyYouHaveAddedNotification(providerConfig.Name);

                return await page2.GoToHomePage();
            }
            else
            {
                var page2 = await page1.DeclineAddRequest();

                var page3 = await page2.ConfirmDeclineRequest();

                return await page3.GoToHomePage();
            }
        }
    }

    public async Task<HomePage> UpdateProviderPermission(ProviderConfig providerConfig, (AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permissions)
    {
        var page = await OpenProviderPermissions();

        var page1 = await page.SelectChangePermissions(providerConfig.Ukprn);

        var page2 = await page1.AddOrSetPermissions(permissions);

        await page2.VerifyYouHaveSetPermissionNotification();

        return await page2.GoToHomePage();
    }

    public async Task<HomePage> UpdateProviderRecruitPermission(ProviderConfig providerConfig, (AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permissions)
    {
        var page = await OpenProviderPermissions();

        var shouldUpdatePermissions = (permissions.recruitpermission == RecruitApprenticePermissions.YesRecruitApprentices && !await page.IsRecruitPermissionsSetToYes()) ||
            (permissions.recruitpermission == RecruitApprenticePermissions.YesRecruitApprenticesButEmployerWillReview && !await page.IsRecruitPermissionsSetToReview()) ||
            (permissions.recruitpermission == RecruitApprenticePermissions.NoToRecruitApprentices && !await page.IsRecruitPermissionsSetToNo());

        if (shouldUpdatePermissions)
        {
            var permissionsPage = await page.SelectChangePermissions(providerConfig.Ukprn);
            var confirmationPage = await permissionsPage.AddOrSetPermissions(permissions);

            await confirmationPage.VerifyYouHaveSetPermissionNotification();

            return await confirmationPage.GoToHomePage();
        }

        return await page.GoToHomePage();
    }

    internal async Task<ManageTrainingProvidersPage> OpenProviderPermissions() => await new ManageTrainingProvidersLinkHomePage(context).OpenRelationshipPermissions();

}
