global using NUnit.Framework;
global using SFA.DAS.ConfigurationBuilder;
global using SFA.DAS.EPAO.UITests.Project.Helpers.DataHelpers;
global using SFA.DAS.EPAO.UITests.Project.Helpers.SqlHelpers;
global using SFA.DAS.FrameworkHelpers;
global using SFA.DAS.Login.Service.Project.Helpers;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text.RegularExpressions;
global using TechTalk.SpecFlow;
using SFA.DAS.Login.Service.Project;
using System.Threading.Tasks;

namespace SFA.DAS.EPAO.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    private readonly DbConfig _config = context.Get<DbConfig>();
    private readonly TryCatchExceptionHelper _tryCatch = context.Get<TryCatchExceptionHelper>();
    private EPAOAdminDataHelper _ePAOAdminDataHelper;
    private EPAOAdminSqlDataHelper _ePAOAdminSqlDataHelper;
    private EPAOApplySqlDataHelper _ePAOApplySqlDataHelper;

    [BeforeScenario(Order = 32)]
    public void SetUpHelpers()
    {
        var objectContext = context.Get<ObjectContext>();

        _ePAOApplySqlDataHelper = new EPAOApplySqlDataHelper(objectContext, _config);

        context.Set(_ePAOApplySqlDataHelper);

        _ePAOAdminSqlDataHelper = new EPAOAdminSqlDataHelper(objectContext, _config);

        context.Set(_ePAOAdminSqlDataHelper);

        context.Set(new EPAOAssesorCreateUserDataHelper());

        context.Set(new EPAOAssesmentServiceDataHelper());

        context.Set(new EPAOApplyDataHelper());

        context.Set(new EPAOApplyStandardDataHelper());

        _ePAOAdminDataHelper = new EPAOAdminDataHelper();

        context.Set(_ePAOAdminDataHelper);

        context.Set(new EPAOAdminCASqlDataHelper(objectContext, _config));
    }

    [BeforeScenario(Order = 33)]
    [Scope(Tag = "deleteorganisationstandards")]
    public async Task ClearStandards() => await _ePAOAdminSqlDataHelper.DeleteOrganisationStandard(_ePAOAdminDataHelper.StandardCode, EPAOAdminDataHelper.OrganisationEpaoId);

    [BeforeScenario(Order = 34)]
    [Scope(Tag = "resetapplyuserorganisationid")]
    public async Task ResetApplyUserOrganisationId() => await _ePAOApplySqlDataHelper.ResetApplyUserOrganisationId(context.GetUser<EPAOApplyUser>().Username);

    [BeforeScenario(Order = 35)]
    [Scope(Tag = "resetstandardwithdrawal")]
    public async Task ResetStandardWithdrawalApplication() => await _ePAOApplySqlDataHelper.ResetStandardWithdrawals(context.GetUser<EPAOWithdrawalUser>().Username);

    [BeforeScenario(Order = 36)]
    [Scope(Tag = "resetregisterwithdrawal")]
    public async Task ResetRegisterWithdrawalApplication() => await _ePAOApplySqlDataHelper.ResetRegisterWithdrawals(context.GetUser<EPAOWithdrawalUser>().Username);

    [BeforeScenario(Order = 37)]
    [Scope(Tag = "deleteorganisationstandardversion")]
    public async Task ClearOrgganisationStandardVersion() => await _ePAOApplySqlDataHelper.DeleteOrganisationStandardVersion();

    [AfterScenario(Order = 32)]
    [Scope(Tag = "deleteorganisationcontact")]
    public async Task ClearContact() => await _tryCatch.AfterScenarioException(async () => await _ePAOAdminSqlDataHelper.DeleteContact(_ePAOAdminDataHelper.Email));

    [AfterScenario(Order = 33)]
    [Scope(Tag = "deleteorganisation")]
    public async Task ClearOrganisation() => await _tryCatch.AfterScenarioException(async () => await _ePAOAdminSqlDataHelper.DeleteOrganisation(_ePAOAdminDataHelper.NewOrganisationUkprn));

    [AfterScenario(Order = 34)]
    [Scope(Tag = "makeorganisationlive")]
    public async Task MakeOrganisationLive() => await _tryCatch.AfterScenarioException(async () => await _ePAOAdminSqlDataHelper.UpdateOrgStatusToLive(EPAOAdminDataHelper.MakeLiveOrganisationEpaoId));
}
