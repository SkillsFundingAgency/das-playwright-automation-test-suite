using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework;
using Reqnroll;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using SFA.DAS.ProvideFeedback.UITests.Project.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static System.Net.Mime.MediaTypeNames;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages
{
    public class DigiCertsHomePage(Reqnroll.ScenarioContext context) : BasePage(context)
    {

        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Sign in stub");

        public async Task<DigiCertsHomePage> clickStart()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Start" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsHomePage(context));
        }

        public async Task<DigiCertsSignedInPage> enterLogin(DigitalCertUser user)
        {
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Id" }).ClickAsync();
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Id" }).FillAsync(user.Id);

            await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).ClickAsync();
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync(user.Email);

            await page.GetByRole(AriaRole.Textbox, new() { Name = "Phone" }).ClickAsync();
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Phone" }).FillAsync(user.Phone);

            if(user is DigiCertStandardUser)
            {
                await page.GetByRole(AriaRole.Button, new() { Name = "Upload a JSON file that" }).SetInputFilesAsync("Oliver_Turner_Verify.json");
            }
            else if (user is DigiCertFrameworkUser)
            {
                await page.GetByRole(AriaRole.Button, new() { Name = "Upload a JSON file that" }).SetInputFilesAsync("Amelia_Parker_Verify.json");
            }
            else if (user is DigiCertMultiStandardUser)
            {
                await page.GetByRole(AriaRole.Button, new() { Name = "Upload a JSON file that" }).SetInputFilesAsync("Emily_Carter_Verify.json");
            }
            else if (user is DigiCertMultiFrameworkUser)
            {
                await page.GetByRole(AriaRole.Button, new() { Name = "Upload a JSON file that" }).SetInputFilesAsync("James_Bennett_Verify.json");
            }

            await page.GetByRole(AriaRole.Button, new() { Name = "Authenticate" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsSignedInPage(context));
        }


        public async Task RemoveAuthenticationAsync(DigitalCertUser user)
        {
            var objectContext = context.Get<ObjectContext>();
            var dbConfig = context.Get<DbConfig>();

            var sqlHelper = new DigiCertsSqlHelper(objectContext, dbConfig);

            await sqlHelper.RemoveAuthentication(user.Id);
        }


    }
}
