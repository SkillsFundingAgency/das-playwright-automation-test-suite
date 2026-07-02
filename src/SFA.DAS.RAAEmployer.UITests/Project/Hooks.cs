namespace SFA.DAS.RAAEmployer.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    [BeforeScenario(Order = 34)]
    public async Task SetUpHelpers()
    {
        //var apprenticeCourseDataHelper = new ApprenticeCourseDataHelper(new RandomCourseDataHelper(), ApprenticeStatus.WaitingToStart, []);

        //context.Set(apprenticeCourseDataHelper);

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
                    }
                }
                catch (KeyNotFoundException)
                {
                    // RAAEmployerUser not set for this scenario - skip credential setup
                }
            }
        }

        var dfeframeworkList = context.Get<FrameworkList<DfeProviderUsers>>();

        var dfeProviderDetailsList = context.Get<List<ProviderDetails>>();

        var providerUsedByRaaEmployer = new ProviderUsedByRaaEmployer { Ukprn = RAADataHelper.Provider };

        providerUsedByRaaEmployer = SetProviderCredsHelper.SetProviderCreds(dfeframeworkList, dfeProviderDetailsList, providerUsedByRaaEmployer);

        RAADataHelper.ProviderEmail = providerUsedByRaaEmployer.Username;

        await Task.CompletedTask;
    }
}
