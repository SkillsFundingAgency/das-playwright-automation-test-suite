using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Authorisation;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Dashboard;
using SFA.DAS.Framework;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages
{
    public class DigiCertsCheckIdentityPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check you've proved your identity");

        public async Task<DigiCertsClearMatchesPage> verifyAuthorisationJourney()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsClearMatchesPage(context));
        }

        public async Task<DigiCertsDashboardPage> verifyDashBoardPage()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsDashboardPage(context));
        }

        public async Task<DigiCertsStandardDetailsPage> verifyStandardDetailsPage()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsStandardDetailsPage(context));
        }

        public async Task<DigiCertsFrameworkDetailsPage> verifyFrameworkDetailsPage()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsFrameworkDetailsPage(context));
        }
    }
}
