using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Employer;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Employer;

public abstract class Employer_BaseSteps(ScenarioContext context) : AppEmp_BaseSteps(context)
{
    protected Employer_NetworkHubPage networkHubPage;

    protected async Task EmployerSign(EasAccountUser user) => await EmployerSign(async () => await new EmployerPortalLoginHelper(context).Login(user, true));

    protected async Task EmployerSign(MultipleEasAccountUser user) => await EmployerSign(async () => await new MultipleAccountsLoginHelper(context, user).Login(user, true));

    private static async Task EmployerSign(Func<Task<HomePage>> func)
    {
        var x = await func(); await x.GoToAanHomePage();
    }
}