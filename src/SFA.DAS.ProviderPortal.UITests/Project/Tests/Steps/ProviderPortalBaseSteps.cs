
using SFA.DAS.EmployerPortal.UITests.Project;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderPortal.UITests.Project.Helpers;
using SFA.DAS.ProviderPortal.UITests.Project.Pages;

namespace SFA.DAS.ProviderPortal.UITests.Project.Tests.Steps;

public abstract class ProviderPortalBaseSteps(ScenarioContext context)
{
    protected readonly ScenarioContext context = context;

    protected readonly EmployerPortalLoginHelper _employerLoginHelper = new(context);

    protected readonly EmployerHomePageStepsHelper _employerHomePageHelper = new(context);

    protected readonly EmployerPermissionsStepsHelper _employerPermissionsStepsHelper = new(context);

    private readonly ProviderHomePageStepsHelper _providerHomePageStepsHelper = new(context);

    protected readonly ProviderConfig providerConfig = context.GetProviderConfig<ProviderConfig>();

    protected readonly ObjectContext objectContext = context.Get<ObjectContext>();

    protected readonly EprDataHelper eprDataHelper = context.Get<EprDataHelper>();

    private readonly RetryHelper _assertHelper = context.Get<RetryHelper>();

    protected readonly string[] tags = context.ScenarioInfo.Tags;

    protected (AddApprenticePermissions AddApprentice, RecruitApprenticePermissions RecruitApprentice) permissions;

    protected async Task<SearchEmployerEmailPage> GoToSearchEmployerEmailPage()
    {
        var page = await new ViewEmpAndManagePermissionsPage(context).ClickAddAnEmployer();

        return await page.StartNowToAddAnEmployer();
    }

    protected async Task<EmailAccountFoundPage> GoToEmailAccountFoundPage()
    {
        var page = await GoToSearchEmployerEmailPage();

        return await page.EnterEmployerEmail();
    }

    protected async Task GoToProviderAddAnEmployer() => await GoToProviderRelationsHomePage(true);

    protected async Task GoToProviderViewEmployersAndManagePermissions() => await GoToProviderRelationsHomePage(false);

    protected async Task OpenEmpInviteFromProvider()
    {
        _assertHelper.RetryOnNUnitException(async () =>
        {
            await SetRequestId(RequestType.CreateAccount);

            var expected = "Sent";

            var actual = eprDataHelper.RequestStatus;

            Assert.AreEqual(expected, actual, $"Waiting for Invite status to be '{expected}' for requestid - '{eprDataHelper.LatestRequestId}', email - {eprDataHelper.EmployerEmail}");
        }, RetryTimeOut.GetTimeSpan([60, 60, 60, 45, 45, 45, 45, 45, 45]));

        await context.Get<Driver>().Page.GotoAsync(UrlConfig.Relations_Employer_Invite(eprDataHelper.LatestRequestId));
    }

    protected async Task ProviderUpdatePermission((AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permisssion)
    {
        await GoToProviderViewEmployersAndManagePermissions();

        var page = await new ViewEmpAndManagePermissionsPage(context).ViewEmployer();

        var page1 = await page.ChangePermissions();

        var page2 = await page1.ProviderRequestPermissions(permisssion);

        var page3 = await page2.GoToViewCurrentEmployersPage();

        await page3.VerifyPendingRequest();

    }

    protected async Task EmployerUpdatePermission((AddApprenticePermissions AddApprentice, RecruitApprenticePermissions RecruitApprentice) permissions)
    {
        this.permissions = permissions;

        await _employerPermissionsStepsHelper.UpdateProviderPermission(providerConfig, permissions);
    }

    protected async Task EPRLevyUserLogin() => await EPRLogin(context.GetUser<EPRLevyUser>());

    protected async Task EPRReLogin(RequestType requestType)
    {
        await _employerHomePageHelper.GotoEmployerHomePage();

        await SetRequestId(requestType);

        eprDataHelper.AgreementId = objectContext.GetAleAgreementId();
    }

    protected async Task EPRLogin(EPRBaseUser user)
    {
        await _employerLoginHelper.Login(user, true);

        await new DeleteProviderRelationinDbHelper(context).DeleteProviderRelation();
    }

    protected async Task SetRequestId(RequestType requestType)
    {
        string ukprn = providerConfig.Ukprn;

        string empemail = eprDataHelper.EmployerEmail;

        var query = $"and EmployerContactEmail = '{empemail}'";

        if (requestType == RequestType.Permission)
        {
            var user = context.Get<EPRBaseUser>();

            var accountLegalEntityId = user.UserCreds.AccountDetails.SingleOrDefault().Aleid;

            query = $"and AccountLegalEntityId = '{accountLegalEntityId}'";
        }

        query = $"select id, [Status] from Requests where ukprn = {ukprn} and RequestType = '{EnumToString.GetStringValue(requestType)}' {query} order by RequestedDate desc";

        var (requestId, requestStatus) = await context.Get<RelationshipsSqlDataHelper>().GetRequestId(query);

        objectContext.SetDebugInformation($"fetched request id from db - '{requestId}' with status '{requestStatus}'");

        eprDataHelper.LatestRequestId = requestId;

        eprDataHelper.RequestIds.Add(requestId);

        eprDataHelper.RequestStatus = requestStatus;
    }

    private async Task GoToProviderRelationsHomePage(bool addAnEmployer)
    {
        var providerHomepage = await _providerHomePageStepsHelper.GoToProviderHomePage(providerConfig, true);

        if (addAnEmployer) await providerHomepage.ClickAddAnEmployerLink();

        else await providerHomepage.ClickViewEmployersAndManagePermissionsLink();
    }
}