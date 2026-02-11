using System.IO;

namespace SFA.DAS.Framework.Hooks;

[Binding]
public class DirectorySetupHooks(ScenarioContext context, FeatureContext featureContext)
{
    [BeforeScenario(Order = 3)]
    public void SetUpDirectory()
    {
        var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered SetUpDirectory Order = 3 hook");

        string directory = GetDirectoryPath();

        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        objectContext.SetDirectory(directory);
    }

    private string GetDirectoryPath()
    {
        string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestAttachments", $"{DateTime.Now:dd-MM-yyyy}", $"{EscapePatternHelper.DirectoryEscapePattern(featureContext.FeatureInfo.Title)}");

        return Path.GetFullPath(directory);
    }
}