namespace SFA.DAS.RequestApprenticeshipTraining.UITests.Project.Tests.StepDefinitions;

[Binding]
public class RatProviderSteps(ScenarioContext context)
{
    private readonly ProviderHomePageStepsHelper _providerHomePageStepsHelper = new(context);

    [Then(@"a provider responds to the employer request")]
    public async Task AProviderRespondsToTheEmployerRequest()
    {
        var providerConfig = context.GetProviderConfig<ProviderConfig>();

        await _providerHomePageStepsHelper.GoToProviderHomePage(providerConfig, true);

        var dataHelper = context.Get<RatDataHelper>();

        dataHelper.ProviderEmail = providerConfig.Username;

        dataHelper.ProviderName = providerConfig.Name;

        var page = await new RatProviderHomePage(context).NavigateToEmployerRequestPage();

        var page1 = await page.SelectStandard();

        var page2 = await page1.SelectRequest();

        var page4 = await page2.SelectEmail();

        var page5 = await page4.SelectPhoneNumber();

        await page5.SubmitAnswers();
    }
}
