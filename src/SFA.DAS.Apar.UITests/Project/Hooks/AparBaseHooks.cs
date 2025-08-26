using SFA.DAS.Apar.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.Apar.UITests.Project.Helpers.UkprnDataHelpers;

namespace SFA.DAS.Apar.UITests.Project.Hooks;

public abstract class AparBaseHooks : FrameworkBaseHooks
{
    protected readonly ObjectContext _objectContext;
    private readonly AparApplyAndQnASqlDbHelper _roatpApplyAndQnASqlDbHelper;
    private readonly RoatpQnASqlDbHelper _roatpQnASqlDbHelper;
    private readonly AparAdminSqlDbHelper _adminClearDownDataHelpers;
    protected readonly DbConfig _dbConfig;

    private readonly AparApplyUkprnDataHelpers _roatpApplyUkprnDataHelpers;
    private readonly AparApplyTestDataPrepDataHelpers _roatpApplyTestDataPrepDataHelpers;
    private readonly AparApplyChangeUkprnDataHelpers _roatpApplyChangeUkprnDataHelpers;
    private readonly NewAparAdminUkprnDataHelpers _roatpAdminUkprnDataHelpers;
    private readonly OldAparAdminUkprnDataHelpers _roatpOldAdminUkprnDataHelpers;
    private readonly AparFullUkprnDataHelpers _roatpFullUkprnDataHelpers;

    public AparBaseHooks(ScenarioContext context) : base(context)
    {
        _objectContext = context.Get<ObjectContext>();
        _dbConfig = context.Get<DbConfig>();
        _roatpApplyAndQnASqlDbHelper = new AparApplyAndQnASqlDbHelper(_objectContext, _dbConfig);
        _roatpQnASqlDbHelper = new RoatpQnASqlDbHelper(_objectContext, _dbConfig);
        _adminClearDownDataHelpers = new AparAdminSqlDbHelper(_objectContext, _dbConfig);
        _roatpApplyUkprnDataHelpers = new AparApplyUkprnDataHelpers();
        _roatpApplyTestDataPrepDataHelpers = new AparApplyTestDataPrepDataHelpers();
        _roatpApplyChangeUkprnDataHelpers = new AparApplyChangeUkprnDataHelpers();
        _roatpAdminUkprnDataHelpers = new NewAparAdminUkprnDataHelpers();
        _roatpOldAdminUkprnDataHelpers = new OldAparAdminUkprnDataHelpers();
        _roatpFullUkprnDataHelpers = new AparFullUkprnDataHelpers();
    }

    protected async Task GoToUrl(string url) => await Navigate(url);

    protected void SetUpApplyDataHelpers() => context.Set(new AparApplyDataHelpers());

    protected void SetUpCreateAccountApplyDataHelpers() => context.Set(new AparApplyCreateUserDataHelper());

    protected async Task ClearDownApplyDataAndTrainingProvider()
    {
        await ClearDownApplyData();

        await DeleteTrainingProvider();
    }

    protected async Task ClearDownApplyData() => await _roatpQnASqlDbHelper.ClearDownDataFromQna(await _roatpApplyAndQnASqlDbHelper.ClearDownDataFromApply());

    protected async Task ClearDownApplyAndQnAData_ReappliedApplication(string ukprn)
    {
        var applicationId = await _roatpApplyAndQnASqlDbHelper.GetApplicationId(ukprn);

        await _roatpQnASqlDbHelper.OversightReviewClearDownFromQnA_ReApplyRecord(applicationId);

        await _roatpApplyAndQnASqlDbHelper.OversightReviewClearDownFromApply_ReapplyRecord(applicationId);
    }

    protected async Task ClearDownDataUkprnFromApply(string ukprn) => await _roatpQnASqlDbHelper.ClearDownDataFromQna(await _roatpApplyAndQnASqlDbHelper.ClearDownDataUkprnFromApply(ukprn));

    protected async Task AllowListProviders(string ukprn = null) => await _roatpApplyAndQnASqlDbHelper.AllowListProviders(ukprn);

    protected async Task DeleteTrainingProvider() => await _adminClearDownDataHelpers.DeleteTrainingProvider(GetUkprn());

    protected async Task GetRoatpAppplyData() => await SetDetails(_roatpApplyUkprnDataHelpers.GetRoatpAppplyData(GetTag("rp")));

    protected async Task GetRoatpApplyTestDataPrepData() => await SetDetails(_roatpApplyTestDataPrepDataHelpers.GetRoatpAppplyData(GetTag("rptestdata")));

    protected async Task GetRoatpChangeUkprnAppplyData()
    {
        // every scenario (apply) should only have one tag which starts with rp, which is mapped to the test data.
        var (email, ukprn, newukprn) = _roatpApplyChangeUkprnDataHelpers.GetRoatpChangeUkprnAppplyData(GetTag("rpchangeukprn"));

        await SetEmail(email);
        SetUkprn(ukprn);
        SetNewUkprn(newukprn);
    }

    protected void GetOldRoatpAdminData()
    {
        // every scenario (admin) should only have one tag which starts with rpad, which is mapped to the test data.
        var (providername, ukprn) = _roatpOldAdminUkprnDataHelpers.GetOldRoatpAdminData(GetTag("rpad"));

        SetProviderName(providername);
        SetUkprn(ukprn);
    }

    protected async Task GetNewRoatpAdminData() => await SetDetails(_roatpAdminUkprnDataHelpers.GetNewRoatpAdminData(GetTag("rpad")));

    protected async Task GetRoatpFullData() => await SetDetails(_roatpFullUkprnDataHelpers.GetRoatpE2EData(GetTag("rp")));

    protected string GetUkprn() => _objectContext.GetUkprn();

    private async Task SetEmail(string email)
    {
        if (context.ScenarioInfo.Tags.Contains("perftestroatpapplye2e")) return;

        _objectContext.SetEmail(email);

        var signinId = await new AparApplyContactSqlDbHelper(_objectContext, _dbConfig).GetSignInId(email);

        _objectContext.SetSigninId(signinId);
    }

    private async Task SetDetails((string email, string providername, string ukprn) p)
    {
        await SetEmail(p.email);
        SetProviderName(p.providername);
        SetUkprn(p.ukprn);
    }

    private async Task SetDetails((string email, string ukprn) p)
    {
        await SetEmail(p.email);
        SetUkprn(p.ukprn);
    }

    private void SetProviderName(string providername) => _objectContext.SetProviderName(providername);
    private void SetUkprn(string ukprn) => _objectContext.SetUkprn(ukprn);
    private void SetNewUkprn(string ukprn) => _objectContext.SetNewUkprn(ukprn);

    private string GetTag(string tag) => context.ScenarioInfo.Tags.ToList().Single(x => x.StartsWith(tag));
}
