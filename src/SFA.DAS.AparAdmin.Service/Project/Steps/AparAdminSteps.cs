using SFA.DAS.AparAdmin.Service.Project.Helpers;

namespace SFA.DAS.AparAdmin.Service.Project.Steps;

[Binding]
public class AparAdminSteps(ScenarioContext context)
{
    private readonly AparAdminStepsHelper _roatpAdminStepsHelper = new(context);

    [Given("the provider logs into old apar admin portal")]
    public async Task GivenTheProviderLogsIntoOldAparAdminPortal()
    {
        await _roatpAdminStepsHelper.GoToRoatpAdminHomePage();
    }
}
