using SFA.DAS.Registration.UITests.Project;
using SFA.DAS.Registration.UITests.Project.Helpers.SqlDbHelpers;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public abstract class EmpAccountCreationBase : RegistrationBasePage
{
    protected EmpAccountCreationBase(ScenarioContext context) : base(context)
    {
        VerifyPage();

        var _registrationSqlDataHelper = context.Get<RegistrationSqlDataHelper>();

        var loggedInAccountUser = objectContext.GetLoginCredentials();

        //objectContext.SetOrUpdateUserCreds(loggedInAccountUser.Username, loggedInAccountUser.IdOrUserRef, _registrationSqlDataHelper.CollectAccountDetails(loggedInAccountUser.Username));
    }
}
