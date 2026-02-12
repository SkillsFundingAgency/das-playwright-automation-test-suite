using SFA.DAS.QFAST.UITests.Project.Helpers;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application;
namespace SFA.DAS.QFAST.UITests.Project.Tests.Steps;

[Binding]
public class AOSteps(ScenarioContext context)
{
    private readonly QfastHelpers qfastHelpers = new(context);
    private readonly AO_Page qfastAOPage = new(context);
    protected readonly QfastDataHelpers qfastDataHelpers = context.Get<QfastDataHelpers>();
    private readonly StartApplication_Page startApplication_Page = new(context);

    [Given("I create a new funding application and submit the application")]
    public async Task GivenICreateANewFundingApplicationAndSubmitTheApplication()
    {
        await qfastAOPage.SubmitApplication();        
    }

    [Given("I createa new funding application on behalf of different organisation")]
    public async Task GivenICreateaNewFundingApplicationOnBehalfOfDiffOrganisation()
    {
        await qfastAOPage.SubmitApplicationForDiffOrganisation();
    }

    [Given("I validate status is (.+) for (.+) application")]
    public async Task GivenIValidateOptionIsAvailableWhenTheApplicationStatusIs(string status, string applicationname)
    {
        await startApplication_Page.SelectApplicationAsAOUser(applicationname);
        await startApplication_Page.ValidateStatus(status);
    }

    [When("I withdraw the funding application")]
    public async Task WhenIWithdrawTheFundingApplication()
    {
        await startApplication_Page.WithdrawTheApplication();
        await startApplication_Page.ClickOnManageFundingApplications();
    }
}
