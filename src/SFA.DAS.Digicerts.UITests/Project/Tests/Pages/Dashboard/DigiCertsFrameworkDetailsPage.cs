using Azure;
using Mailosaur.Models;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using static System.Net.Mime.MediaTypeNames;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Dashboard
{
    public class DigiCertsFrameworkDetailsPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page).ToHaveTitleAsync("Certificate Framework");

        public async Task<DigiCertsFrameworkDetailsPage> verifyFrameworkCertificateDetails()
        {
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Success" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "We've matched your" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Hospitality" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("You have passed your")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Amelia", new() { Exact = true })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Parker", new() { Exact = true })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("2637151197")).ToBeVisibleAsync();
             await Assertions.Expect(page.GetByRole(AriaRole.Definition).Filter(new() { HasTextRegex = new Regex("^Hospitality$") })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Hospitality Retail Outlet")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Advanced")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Lancaster and Morecambe")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("January 2019")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("If these details are wrong,")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "contact us to correct your" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Create a sharing link" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("You can create a secure link")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Create link" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Download your certificate" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Download certificate (PDF)" })).ToBeVisibleAsync();
            
            return await VerifyPageAsync(() => new DigiCertsFrameworkDetailsPage(context));
        }

        public async Task<DigiCertsFrameworkDetailsPage> verifyMultiFrameworkCertificateDetails()
        {
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Hospitality" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("You have passed your")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("James", new() { Exact = true })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Bennett", new() { Exact = true })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("4713798115")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Definition).Filter(new() { HasTextRegex = new Regex("^Hospitality$") })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Hospitality Retail Outlet")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Advanced")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Lancaster and Morecambe")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("January 2019")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("If these details are wrong,")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "contact us to correct your" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Create a sharing link" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("You can create a secure link")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Create link" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Download your certificate" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Download certificate (PDF)" })).ToBeVisibleAsync();
            
            return await VerifyPageAsync(() => new DigiCertsFrameworkDetailsPage(context));
        }


        public async Task<DigiCertsFrameworkDetailsPage> verifyFrameworkCertificatePDF()
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
            int totalImages = 0;


            using (var pdfDocument = UglyToad.PdfPig.PdfDocument.Open(downloadPath))
            {
                pdfText = string.Join(Environment.NewLine, pdfDocument.GetPages().Select(pdfPage => pdfPage.Text));

                // Count and validate images
                foreach (var pdfPage in pdfDocument.GetPages())
                {
                    var images = pdfPage.GetImages().ToList();

                    totalImages += images.Count;

                    foreach (var image in images)
                    {
                        Assert.That(image.Bounds.Width, Is.GreaterThan(0), "Image width should be greater than 0.");

                        Assert.That(image.Bounds.Height, Is.GreaterThan(0), "Image height should be greater than 0.");
                    }
                }
            }

            Console.WriteLine("PDF Content:");
            Console.WriteLine(pdfText);

            // Verify PDF content and Images
            Assert.Multiple(() =>
            {
                Assert.That(pdfText, Does.Contain("CERTIFICATE OF ACHIEVEMENT AND RECOGNITION"), "Missing certificate title");
                Assert.That(pdfText, Does.Contain("Certificate number"), "Missing certificate number label");
                Assert.That(pdfText, Does.Contain("00067857"), "Incorrect certificate number");
                Assert.That(pdfText, Does.Contain("Amelia Parker"), "Candidate name missing");
                Assert.That(pdfText, Does.Contain("HOSPITALITY"), "Sector name missing");
                Assert.That(pdfText, Does.Contain("HOSPITALITY RETAIL OUTLET SUPERVISION"), "Standard name missing");
                Assert.That(pdfText, Does.Contain("ADVANCED  Level"), "Level missing");
                Assert.That(pdfText, Does.Contain("DISTINCTION"), "Grade missing");
                Assert.That(pdfText, Does.Contain("01 JANUARY 2019"), "Award date missing");
                Assert.That(totalImages, Is.GreaterThan(0), "No images were found in the PDF.");
            });

            return await VerifyPageAsync(() => new DigiCertsFrameworkDetailsPage(context));
        }


        public async Task<DigiCertsCreateSharingLinkPage> clickCreateLink()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Create link" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsCreateSharingLinkPage(context));
        }
    }
 }