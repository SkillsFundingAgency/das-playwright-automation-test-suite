

namespace SFA.DAS.RAAEmployer.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    [BeforeScenario(Order = 34)]
    public async Task SetUpHelpers()
    {
        //var apprenticeCourseDataHelper = new ApprenticeCourseDataHelper(new RandomCourseDataHelper(), ApprenticeStatus.WaitingToStart, []);

        //context.Set(apprenticeCourseDataHelper);

        var dfeframeworkList = context.Get<FrameworkList<DfeProviderUsers>>();

        var dfeProviderDetailsList = context.Get<List<ProviderDetails>>();

        var providerUsedByRaaEmployer = new ProviderUsedByRaaEmployer { Ukprn = RAADataHelper.Provider };

        providerUsedByRaaEmployer = SetProviderCredsHelper.SetProviderCreds(dfeframeworkList, dfeProviderDetailsList, providerUsedByRaaEmployer);

        RAADataHelper.ProviderEmail = providerUsedByRaaEmployer.Username;

        await Task.CompletedTask;
    }
}
