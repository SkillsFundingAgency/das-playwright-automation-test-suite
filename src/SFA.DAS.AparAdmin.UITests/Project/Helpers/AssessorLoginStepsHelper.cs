using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

namespace SFA.DAS.AparAdmin.UITests.Project.Helpers;

public class AssessorLoginStepsHelper(ScenarioContext context)
{
    private readonly DfeAdminLoginStepsHelper _dfeAdminLoginStepsHelper = new(context);

    public async Task<AparAssessorApplicationsHomePage> Assessor1Login()
    {
        await _dfeAdminLoginStepsHelper.LoginToAsAssessor1();

        return await VerifyPageHelper.VerifyPageAsync(context, () => new AparAssessorApplicationsHomePage(context));
    }

    public async Task<AparAssessorApplicationsHomePage> Assessor2Login()
    {
        await _dfeAdminLoginStepsHelper.LoginToAsAssessor2();

        return await VerifyPageHelper.VerifyPageAsync(context, () => new AparAssessorApplicationsHomePage(context));
    }
}
