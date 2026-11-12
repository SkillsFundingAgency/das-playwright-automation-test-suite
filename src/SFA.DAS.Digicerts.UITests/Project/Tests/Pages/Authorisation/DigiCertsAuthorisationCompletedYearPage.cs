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
    public class DigiCertsAuthorisationCompletedYearPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {

        public override async Task VerifyPage() => await Assertions.Expect(page).ToHaveTitleAsync("Do you know the year you completed your apprenticeship?");



        public async Task<DigiCertsAuthorisationProviderPage> selectYear(string year)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

            await page.GetByRole(AriaRole.Spinbutton, new() { Name = "Year you completed your" }).FillAsync(year);

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsAuthorisationProviderPage(context));
        }

    }
}
