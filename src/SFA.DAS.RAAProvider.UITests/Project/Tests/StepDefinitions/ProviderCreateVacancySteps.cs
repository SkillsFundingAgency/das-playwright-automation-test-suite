using SFA.DAS.RAAProvider.UITests.Project.Helpers;
using SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.StepDefinitions;

[Binding]
public class ProviderCreateVacancySteps(ScenarioContext context)
{
    private readonly ProviderCreateVacancyStepsHelper _providerStepsHelper = new(context);
    //private readonly ProviderCreateDraftAdvertStepsHelper _stepsHelper = new(context);
    private RecruitmentHomePage _recruitmentHomePage;

    [Then(@"the Provider creates anonymous vacancy through View all your vacancies page")]
    public async Task ThenTheProviderCreatesAnonymousVacancyThroughViewAllYourVacanciesPage() => await _providerStepsHelper.CreateAnonymousVacancy();
    
}
