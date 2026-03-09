using Polly;
using SFA.DAS.Framework;
using SFA.DAS.RAA.DataGenerator.Project.Config;
using SFA.DAS.RAA.DataGenerator.Project.Helpers;
using System;

namespace SFA.DAS.RAA.APITests.Project;

[Binding]
public class BeforeScenarioHooks
{
    private readonly ScenarioContext _context;

    public BeforeScenarioHooks(ScenarioContext context) => _context = context;

    [BeforeScenario(Order = 32)]
    public void SetUpHelpers()
    {
        _context.Set(new AdvertDataHelper());

        var objectContext = _context.Get<ObjectContext>();

        _context.SetRestClient(new Outer_RecruitApiClient(objectContext, _context.GetOuter_ApiAuthTokenConfig()));

        _context.Set(new EmployerLegalEntitiesSqlDbHelper(objectContext, _context.Get<DbConfig>()));

        var faaConfig = _context.GetFAAConfig<FAAUserConfig>();
        if (faaConfig == null)
        {
            faaConfig = new FAAUserConfig
            {
                FAAFirstName = "Test",
                FAALastName = "User",
                FAAUserName = "test.user@local",
                FAAPassword = "Password1"
            };
            _context.SetFAAConfig(faaConfig);
        }

        var vacancyTitleHelper = new VacancyTitleDatahelper(isCloneVacancy: false);
        var raaDataHelper = new RAADataHelper(faaConfig, vacancyTitleHelper);

        _context.Set(vacancyTitleHelper);
        _context.Set(raaDataHelper);
    }
}
