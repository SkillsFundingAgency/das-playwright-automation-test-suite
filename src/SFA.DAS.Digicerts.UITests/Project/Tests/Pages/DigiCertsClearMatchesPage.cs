using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Authorisation;
using SFA.DAS.Framework;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages
{
    public class DigiCertsClearMatchesPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page).ToHaveTitleAsync(new Regex("We need more information to match results"));


        public async Task<DigiCertsAuthorisationStartPage> NavigatetoClearCache()
        {
            await page.GotoAsync("https://test-certificates.apprenticeships.education.gov.uk/admin/clear-matches");

            return await VerifyPageAsync(() => new DigiCertsAuthorisationStartPage(context));
        }

        public async Task<DigiCertsAuthorisationStartPage> NavigatetoHomePage()
        {
            await page.GotoAsync("https://test-certificates.apprenticeships.education.gov.uk");

            return await VerifyPageAsync(() => new DigiCertsAuthorisationStartPage(context));
        }

    }
}