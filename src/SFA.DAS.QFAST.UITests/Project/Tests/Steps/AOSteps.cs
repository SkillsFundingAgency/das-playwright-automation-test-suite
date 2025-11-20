using SFA.DAS.QFAST.UITests.Project.Helpers;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application;
namespace SFA.DAS.QFAST.UITests.Project.Tests.Steps;

[Binding]
public class AOSteps(ScenarioContext context)
{
    private readonly QfastHelpers qfastHelpers = new(context);
    private readonly AO_Page qfastAOPage = new(context);
    protected readonly QfastDataHelpers qfastDataHelpers = context.Get<QfastDataHelpers>();

    [Given("I create a new funding application and submit the application")]
    public async Task GivenICreateANewFundingApplicationAndSubmitTheApplication()
    {
        await qfastAOPage.SubmitApplication();        
    }    
}
