using System;

namespace SFA.DAS.RAAProvider.UITests.Project;

[Binding]
public class RAAProviderHooks(ScenarioContext context)
{
    [BeforeScenario(Order = 32)]
    public void SetUpDataHelpers()
    {
        var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered RAAProvider SetUpDataHelpers Order = 32 hook");

        var vacancyTitleDatahelper = new VacancyTitleDatahelper(isCloneVacancy: false);
        context.Set(vacancyTitleDatahelper);
    }

    [BeforeScenario(Order = 33)]
    public void SetUpDatabaseHelpers()
    {
        try
        {
            var objectContext = context.Get<ObjectContext>();

            objectContext.SetConsoleAndDebugInformation("Entered RAAProvider SetUpDatabaseHelpers Order = 33 hook");

            var dbConfig = context.Get<DbConfig>();

            objectContext.SetConsoleAndDebugInformation("Setting ProviderCreateVacancySqlDbHelper");
            context.Set(new ProviderCreateVacancySqlDbHelper(objectContext, dbConfig));

            objectContext.SetConsoleAndDebugInformation("Setting RAAProviderPermissionsSqlDbHelper");
            context.Set(new RAAProviderPermissionsSqlDbHelper(objectContext, dbConfig));

            objectContext.SetConsoleAndDebugInformation("Completed RAAProvider SetUpDatabaseHelpers Order = 33 hook");
        }
        catch (Exception ex)
        {
            var objectContext = context.Get<ObjectContext>();
            objectContext.SetConsoleAndDebugInformation($"ERROR in RAAProvider SetUpDatabaseHelpers Order = 33 hook: {ex.Message}");
            throw;
        }
    }

    [BeforeScenario(Order = 34)]
    public void SetUpEmployerCredentialsForSharedScenarios()
    {
        try
        {
            var objectContext = context.Get<ObjectContext>();

            // Set up LoginCredentialsHelper if not already present (for shared E2E journeys)
            if (!context.ContainsKey(typeof(LoginCredentialsHelper).FullName))
            {
                var loginCredentialsHelper = new LoginCredentialsHelper(objectContext);
                context.Set(loginCredentialsHelper);

                // Set up employer user credentials if not already set (for non-API employer scenarios)
                var isApiEmployer = context.ScenarioInfo.Tags.Contains("raaapiemployer");
                if (!isApiEmployer)
                {
                    try
                    {
                        var employerUser = context.GetUser<RAAEmployerUser>();
                        if (employerUser != null)
                        {
                            loginCredentialsHelper.SetLoginCredentials(employerUser.Username, employerUser.IdOrUserRef, employerUser.OrganisationName);
                            objectContext.SetConsoleAndDebugInformation("Set employer credentials for shared scenario");
                        }
                    }
                    catch (KeyNotFoundException)
                    {
                        // RAAEmployerUser not set for this scenario - skip credential setup
                    }
                }
            }
        }
        catch (Exception ex)
        {
            var objectContext = context.Get<ObjectContext>();
            objectContext.SetConsoleAndDebugInformation($"ERROR in RAAProvider SetUpEmployerCredentialsForSharedScenarios Order = 34 hook: {ex.Message}");
            throw;
        }
    }
}
