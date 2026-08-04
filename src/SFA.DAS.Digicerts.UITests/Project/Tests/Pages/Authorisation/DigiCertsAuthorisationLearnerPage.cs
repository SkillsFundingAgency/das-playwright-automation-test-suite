using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework;
using SFA.DAS.ConfigurationBuilder;
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
    public class DigiCertsAuthorisationLearnerPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {

        public override async Task VerifyPage() => await Assertions.Expect(page).ToHaveTitleAsync("Do you know your unique learner number?");



        public async Task<DigiCertsAuthorisationCoursePage> enterLearner(string uln)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

            await page.GetByRole(AriaRole.Spinbutton, new() { Name = "Unique learner number" }).FillAsync(uln);

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsAuthorisationCoursePage(context));
        }

        public async Task<DigiCertsAuthorisationCoursePage> SelectNoForLearner()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsAuthorisationCoursePage(context));
        }
    }
}
