using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Dashboard;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using SFA.DAS.ProvideFeedback.UITests.Project.Helpers;
using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Authorisation
{
    public class DigiCertsAuthorisationAnswersPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {

        public override async Task VerifyPage() => await Assertions.Expect(page).ToHaveTitleAsync("Check your answers");


        public async Task<DigiCertsDashboardPage> clickSubmit()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsDashboardPage(context));
        }

        public async Task<DigiCertsStandardDetailsPage> clickSubmitandViewStandard()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsStandardDetailsPage(context));
        }

        public async Task<DigiCertsFrameworkDetailsPage> clickSubmitandViewFramework()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsFrameworkDetailsPage(context));
        }

    }
}
