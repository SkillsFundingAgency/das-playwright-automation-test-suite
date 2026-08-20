using Microsoft.Playwright;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Authorisation;
using SFA.DAS.Framework;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SFA.DAS.ConfigurationBuilder;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages
{
    public class DigiCertsClearMatchesPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page).ToHaveTitleAsync(new Regex("We need more information to match results"));

        public static string DigiCerts_ClearCacheUrl => $"https://{EnvironmentConfig.EnvironmentName}-certificates.apprenticeships.education.gov.uk/admin/clear-matches";
        public static string DigiCerts_HomePage => $"https://{EnvironmentConfig.EnvironmentName}-certificates.apprenticeships.education.gov.uk";

        public async Task<DigiCertsAuthorisationStartPage> NavigatetoClearCache()
        {
            await page.GotoAsync(DigiCerts_ClearCacheUrl);

            return await VerifyPageAsync(() => new DigiCertsAuthorisationStartPage(context));
        }

        public async Task<DigiCertsAuthorisationStartPage> NavigatetoHomePage()
        {
            await page.GotoAsync(DigiCerts_HomePage);

            return await VerifyPageAsync(() => new DigiCertsAuthorisationStartPage(context));
        }

    }
}