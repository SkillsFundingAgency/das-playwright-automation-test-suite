using Reqnroll;
using SFA.DAS.Framework;
using SFA.DAS.Framework.Hooks;
using System.Threading.Tasks;



namespace SFA.DAS.DigiCerts.UITests.Project;

[Binding]
public class Hooks(Reqnroll.ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario]
    public async Task Navigate() => await Navigate(UrlConfig.DigiCerts_BaseUrl);
   


}