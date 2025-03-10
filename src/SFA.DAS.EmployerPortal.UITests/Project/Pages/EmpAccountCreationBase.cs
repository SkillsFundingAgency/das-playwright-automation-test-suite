namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public abstract class EmpAccountCreationBase : RegistrationBasePage
{
    private readonly LoggedInAccountUser loggedInAccountUser;

    protected EmpAccountCreationBase(ScenarioContext context) : base(context) => loggedInAccountUser = objectContext.GetLoginCredentials();

    protected async Task SetEasNewUser()
    {
        await context.SetEasLoginUser([new NewUser { Username = loggedInAccountUser.Username, IdOrUserRef = loggedInAccountUser.IdOrUserRef }]);
    }
}
