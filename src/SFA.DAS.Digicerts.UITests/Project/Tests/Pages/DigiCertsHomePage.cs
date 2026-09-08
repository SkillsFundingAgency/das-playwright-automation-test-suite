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

            var stubJsonFilesPath = Path.Combine(AppContext.BaseDirectory,"StubJsonfiles");

            string jsonFileName;

            if (user is DigiCertStandardUser)
            {
                jsonFileName = "OliverTurnerVerify.json";
            }
            else if (user is DigiCertFrameworkUser)
            {
                jsonFileName = "AmeliaParkerVerify.json";
            }
            else if (user is DigiCertMultiStandardUser)
            {
                jsonFileName = "EmilyCarterVerify.json";
            }
            else if (user is DigiCertMultiFrameworkUser)
            {
                jsonFileName = "JamesBennettVerify.json";
            }
            else
            {
                throw new ArgumentException($"Unsupported user type: {user.GetType().Name}");
            }

            var jsonFilePath = Path.Combine(stubJsonFilesPath,jsonFileName);

            Console.WriteLine($"DEBUG stubJsonFilesPath: {stubJsonFilesPath}");
            Console.WriteLine($"DEBUG jsonFileName: {jsonFileName}");
            Console.WriteLine($"DEBUG jsonFilePath: {jsonFilePath}");
            Console.WriteLine($"DEBUG file exists: {File.Exists(jsonFilePath)}");

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException(
                    $"Stub JSON file could not be found. " +
                    $"Expected path: {jsonFilePath}",
                    jsonFilePath);
            }

            await page.GetByRole(AriaRole.Button,  new() { Name = "Upload a JSON file that" }).SetInputFilesAsync(jsonFilePath);

            await page.GetByRole(AriaRole.Button,  new() { Name = "Authenticate" }).ClickAsync();

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
