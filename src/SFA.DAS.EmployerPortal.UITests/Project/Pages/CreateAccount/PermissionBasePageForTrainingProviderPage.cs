namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;


public enum AddApprenticePermissions
{
    //[ToString("Yes, employer will review records")]
    YesAddApprenticeRecords,
    //[ToString("No")]
    NoToAddApprenticeRecords
}

public enum RecruitApprenticePermissions
{
    //[ToString("Yes")]
    YesRecruitApprentices,
    //[ToString("Yes, employer will review adverts")]
    YesRecruitApprenticesButEmployerWillReview,
    //[ToString("No")]
    NoToRecruitApprentices
}


public abstract class PermissionBasePageForTrainingProviderPage(ScenarioContext context) : EmployerPortalBasePage(context)
{

    //public async Task<ManageTrainingProvidersPage> AddOrSetPermissions((AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permisssion)
    //{
    //    await SetAddApprentice(permisssion.cohortpermission);

    //    await SetRecruitApprentice(permisssion.recruitpermission);

    //    return await VerifyPageAsync(() => new ManageTrainingProvidersPage(context));
    //}

    public async Task<EmployerAccountCreatedPage> AddOrSetPermissionsAndCreateAccount((AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permisssion)
    {
        await SetAddApprentice(permisssion.cohortpermission);

        await SetRecruitApprentice(permisssion.recruitpermission);

        return await VerifyPageAsync(() => new EmployerAccountCreatedPage(context));
    }

    protected async Task SetAddApprentice(AddApprenticePermissions permission)
    {
        if (permission == AddApprenticePermissions.YesAddApprenticeRecords)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, but I want final review" }).CheckAsync();
        }

        if (permission == AddApprenticePermissions.NoToAddApprenticeRecords)
        {
            await page.GetByRole(AriaRole.Group, new() { Name = "Add apprentice records" }).GetByLabel("No").CheckAsync();
        }
    }

    protected async Task SetRecruitApprentice(RecruitApprenticePermissions permission)
    {
        if (permission == RecruitApprenticePermissions.YesRecruitApprentices)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes", Exact = true }).CheckAsync();
        }

        if (permission == RecruitApprenticePermissions.YesRecruitApprenticesButEmployerWillReview)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, but I want to review" }).CheckAsync();
        }

        if (permission == RecruitApprenticePermissions.NoToRecruitApprentices)
        {
            await page.GetByRole(AriaRole.Group, new() { Name = "Recruit apprentices" }).GetByLabel("No").CheckAsync();
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
    }
}

public class ManageTrainingProvidersLinkHomePage(ScenarioContext context) : HomePage(context)
{
    public async Task<ManageTrainingProvidersPage> OpenRelationshipPermissions()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Manage training providers" }).ClickAsync();

        return new ManageTrainingProvidersPage(context);
    }
}
