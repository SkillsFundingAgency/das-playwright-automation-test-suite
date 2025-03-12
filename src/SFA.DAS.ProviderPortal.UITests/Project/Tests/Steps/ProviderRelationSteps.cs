namespace SFA.DAS.ProviderPortal.UITests.Project.Tests.Steps;

[Binding]
public class ProviderRelationSteps(ScenarioContext context) : ProviderPortalBaseSteps(context)
{
    [Given(@"a provider requests all permission from an employer")]
    public async Task AProviderRequestsAllPermissionFromAnEmployer()
    {
        EPRBaseUser employerUser = tags.Contains("acceptrequest") ? context.GetUser<EPRAcceptRequestUser>() : context.GetUser<EPRDeclineRequestUser>();

        context.Set(employerUser);

        await EPRLogin(employerUser);

        permissions = (AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprentices);

        await GoToProviderViewEmployersAndManagePermissions();

        eprDataHelper.EmployerEmail = employerUser.Username;

        eprDataHelper.EmployerOrganisationName = employerUser.OrganisationName;

        var page = await GoToEmailAccountFoundPage();

        var page1 = await page.ContinueToInvite();

        var page2 = await page1.ProviderRequestPermissions(permissions);

        var page3 = await page2.GoToViewEmployersPage();

        await page3.VerifyPendingRequest();
    }

    [When("the provider updates the permission to NoToAddApprenticeRecords YesRecruitApprenticesButEmployerWillReview")]
    public async Task WhenTheProviderUpdatesThePermissionToNoToAddApprenticeRecordsYesRecruitApprenticesButEmployerWillReview()
    {
        await ProviderUpdatePermission((AddApprenticePermissions.NoToAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprenticesButEmployerWillReview));
    }

    [When("the provider updates the permission to NoToAddApprenticeRecords YesRecruitApprentices")]
    public async Task WhenTheProviderUpdatesThePermissionToNoToAddApprenticeRecordsYesRecruitApprentices()
    {
        await ProviderUpdatePermission((AddApprenticePermissions.NoToAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprentices));
    }

    [When("the provider updates the permission to YesAddApprenticeRecords YesRecruitApprentices")]
    public async Task WhenTheProviderUpdatesThePermissionToYesAddApprenticeRecordsYesRecruitApprentices()
    {
        await ProviderUpdatePermission((AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprentices));
    }

    [When("the provider updates the permission to YesAddApprenticeRecords YesRecruitApprenticesButEmployerWillReview")]
    public async Task WhenTheProviderUpdatesThePermissionToYesAddApprenticeRecordsYesRecruitApprenticesButEmployerWillReview()
    {
        await ProviderUpdatePermission((AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprenticesButEmployerWillReview));
    }

    [When("the provider updates the permission to YesAddApprenticeRecords NoToRecruitApprentices")]
    public async Task WhenTheProviderUpdatesThePermissionToYesAddApprenticeRecordsNoToRecruitApprentices()
    {
        await ProviderUpdatePermission((AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.NoToRecruitApprentices));
    }
}