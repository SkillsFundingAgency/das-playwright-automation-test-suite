namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps;

public abstract class AanBaseSteps(ScenarioContext context) : FrameworkBaseHooks(context)
{
    protected readonly ObjectContext objectContext = context.Get<ObjectContext>();

    protected readonly AANSqlHelper _aanSqlHelper = context.Get<AANSqlHelper>();

    protected static void AssertListContains(List<string> actual, List<string> expectedresults)
    {
        Assert.Multiple(() =>
        {
            foreach (var expected in expectedresults)
            {
                CollectionAssert.Contains(actual, expected);
            }
        });
    }
}