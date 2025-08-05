using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Steps;

[Binding]

public class ApprovalsSteps(ScenarioContext context)
{
    private readonly AccountCreationStepsHelper _stepsHelper = new(context);

    [Given("The User creates NonLevyEmployer account and sign an agreement")]
    [Given("The User creates LevyEmployer account and sign an agreement")]


    public async Task GivenTheUsercreatesNonLevyEmployeraccountandsignanagreement()

    {
        await _stepsHelper.CreateUserAccount();
    }
}

