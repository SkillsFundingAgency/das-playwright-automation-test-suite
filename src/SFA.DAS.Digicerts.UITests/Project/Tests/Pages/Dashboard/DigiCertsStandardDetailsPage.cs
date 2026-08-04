using Azure;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework;
using SFA.DAS.Framework;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using static System.Net.Mime.MediaTypeNames;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Dashboard
{
    public class DigiCertsStandardDetailsPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page).ToHaveTitleAsync("Certificate Standard");

        public async Task<DigiCertsStandardDetailsPage> verifyMultiStandardCertificateDetails()
        {

            await Assertions.Expect(page.GetByText("You have passed your")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Emily", new() { Exact = true })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Carter", new() { Exact = true })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("3283991481")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("00014801")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Definition).Filter(new() { HasText = "Business administrator" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("3", new() { Exact = true })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("January 2024")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("CITY SKILLS LIMITED")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("If these details are wrong,")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "contact us to correct your" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Create a sharing link" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("You can create a secure link")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Create link" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Download your certificate" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Download certificate (PDF)" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Certificate print status" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Your certificate is")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("A certificate was printed on")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("If your certificate has been")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "contact us for a replacement" })).ToBeVisibleAsync();

            return await VerifyPageAsync(() => new DigiCertsStandardDetailsPage(context));
        }

        public async Task<DigiCertsStandardDetailsPage> verifyStandardCertificateDetails()
        {
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Success" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "We've matched your" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Accountancy or taxation" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("You have passed your")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Oliver", new() { Exact = true })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Turner", new() { Exact = true })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("2697749755")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("00014802")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Definition).Filter(new() { HasText = "Accountancy or taxation professional" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("7", new() { Exact = true })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("January 2020")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("KAPLAN FINANCIAL LIMITED")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("If these details are wrong,")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "contact us to correct your" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Create a sharing link" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("You can create a secure link")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Create link" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Download your certificate" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Download certificate (PDF)" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Certificate print status" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Your certificate is")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("A certificate was printed on")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("If your certificate has been")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "contact us for a replacement" })).ToBeVisibleAsync();

            return await VerifyPageAsync(() => new DigiCertsStandardDetailsPage(context));
        }

               

        public async Task<DigiCertsStandardDetailsPage> verifyStandardCertificatePDF()
        {
            var download = await page.RunAndWaitForDownloadAsync(async () =>
            {
                await page
                    .GetByRole(AriaRole.Link, new() { Name = "Download certificate (PDF)" })
                    .ClickAsync();
            });

            // Create download folder
            var downloadDirectory = Path.Combine(TestContext.CurrentContext.WorkDirectory, "test-results");
            Directory.CreateDirectory(downloadDirectory);

            // Save PDF
            var downloadPath = Path.Combine(downloadDirectory, download.SuggestedFilename);
            await download.SaveAsAsync(downloadPath);

            // Verify PDF exists
            Assert.That(File.Exists(downloadPath), Is.True,
                $"PDF download failed. File not found: {downloadPath}");

            string pdfText;

            using (var pdfDocument = UglyToad.PdfPig.PdfDocument.Open(downloadPath))
            {
                pdfText = string.Join(
                    Environment.NewLine,
                    pdfDocument.GetPages()
                        .Select(pdfPage => pdfPage.Text)
                );
            }

            Console.WriteLine("PDF Content:");
            Console.WriteLine(pdfText);

            // Verify PDF content
            Assert.Multiple(() =>
            {
                Assert.That(pdfText, Does.Contain("CERTIFICATE OF ACHIEVEMENT"), "Missing certificate title");
                Assert.That(pdfText, Does.Contain("Certificate number"), "Missing certificate number label");
                Assert.That(pdfText, Does.Contain("00014802"), "Incorrect certificate number");
                Assert.That(pdfText, Does.Contain("Oliver Turner"), "Candidate name missing");
                Assert.That(pdfText, Does.Contain("ACCOUNTANCY OR TAXATION PROFESSIONAL"), "Qualification name missing");
                Assert.That(pdfText, Does.Contain("Level 7"), "Level missing");
                Assert.That(pdfText, Does.Contain("PASS"), "Grade missing");
                Assert.That(pdfText, Does.Contain("01 JANUARY 2020"), "Award date missing");
            });
            
            return await VerifyPageAsync(() => new DigiCertsStandardDetailsPage(context));
        }

    }
}