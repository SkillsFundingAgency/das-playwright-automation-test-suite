using System;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.StepDefinitions;

[Binding]
public class ProviderCreateVacancySteps(ScenarioContext context)
{
    private readonly ProviderCreateVacancyStepsHelper _providerStepsHelper = new(context);
    private readonly ProviderCreateDraftAdvertStepsHelper _stepsHelper = new(context);
    private RecruitmentHomePage _recruitmentHomePage;

    
}
