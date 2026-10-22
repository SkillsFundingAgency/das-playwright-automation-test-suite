using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using static SFA.DAS.EmployerPortal.UITests.Project.Helpers.EnumHelper;

namespace SFA.DAS.EmployerPortal.UITests.Project.Steps;

[Binding]
public class MultipleAccountsSteps
{
    private readonly ScenarioContext _context;
    private readonly EmployerPortalLoginHelper _employerPortalLoginHelper;
    private readonly CreateAccountEmployerPortalLoginHelper _createAccountEmployerPortalLoginHelper;
    private readonly AccountCreationStepsHelper _accountCreationStepsHelper;
    private readonly ObjectContext _objectContext;
    private readonly EmployerHomePageStepsHelper _employerStepHelper;
    private readonly EmployerWithMultipleAccountsUser _employerWithMultipleAccountsUser;

    protected readonly EmployerHomePageStepsHelper employerHomePageHelper;

    private HomePage _homePage;

    public MultipleAccountsSteps(ScenarioContext context)
    {
        _context = context;
        _objectContext = _context.Get<ObjectContext>();
        _employerStepHelper = new EmployerHomePageStepsHelper(context);
        _accountCreationStepsHelper = new AccountCreationStepsHelper(context);
        _employerPortalLoginHelper = new EmployerPortalLoginHelper(context);
        _createAccountEmployerPortalLoginHelper = new CreateAccountEmployerPortalLoginHelper(_context);
        _employerWithMultipleAccountsUser = _context.GetUser<EmployerWithMultipleAccountsUser>();
    }


    [Given(@"the Employer1 logins using existing Levy Account")]
    public async Task ThenEmployer1LogIns() {
        // await _employerStepHelper.GotoEmployerHomePage(true);
        await _employerPortalLoginHelper.Login(_context.GetUser<EmployerWithMultipleAccountsUser>());
        await _employerStepHelper.GotoEmployerHomePage(false);
       
        var organisationName = _objectContext.GetOrganisationName();
        _context.Set(organisationName, "CurrentOrganisationName");
    }

    [Given(@"the Employer2 logins")]
    public async Task ThenEmployer2LogIns()
    {
        _objectContext.UpdateOrganisationName(_employerWithMultipleAccountsUser.SecondOrganisationName);
        await _employerStepHelper.GotoEmployerHomePage(false);

        var organisationName = _objectContext.GetOrganisationName();
        _context.Set(organisationName, "CurrentOrganisationName");
    }

    [Given(@"the Employer3 logins")]
    [Then(@"the Employer3 logins")]
    public async Task ThenEmployer3LogIns()
    {

        //  await new YourAccountsPage(_context).SwitchAccount(_employerWithMultipleAccountsUser.ThirdOrganisationName);
        _objectContext.UpdateOrganisationName(_employerWithMultipleAccountsUser.ThirdOrganisationName);
        await _employerStepHelper.GotoEmployerHomePage(false);

        var organisationName = _objectContext.GetOrganisationName();
        _context.Set(organisationName, "CurrentOrganisationName");
    }
}