using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework;
using SFA.DAS.Digicerts.UITests.Project.Tests.Features;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Authorisation;
using SFA.DAS.Framework;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages
{
    public class DigiCertsCreateNewSharingLinkPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page).ToHaveTitleAsync(new Regex("Sharing link"));
        private IPlaywright playwright;
        private IBrowser newBrowser;
        private IBrowserContext newBrowserContext;
        private IPage newPage;

        public async Task<DigiCertsCreateNewSharingLinkPage> verifyCreateNewSharingLinkPage()
        {
            await Assertions.Expect(page.GetByText("This sharing link will")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Delete link" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Secure web link")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Copy link" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Share by email")).ToBeVisibleAsync();
            await Assertions.Expect(page.Locator("#EmailAddress")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Send email" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Link history" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("When your link has been")).ToBeVisibleAsync();

            return await VerifyPageAsync(() => new DigiCertsCreateNewSharingLinkPage(context));
        }

        public async Task<DigiCertsCreateNewSharingLinkPage> viewSharingLinkPrivateBrowserandVerify(string certificate)
        {
            var secureLink = page.GetByRole(AriaRole.Textbox, new() { Name = "Secure web link" });

            // Get the URL from the textbox
            var sharingUrl = await secureLink.InputValueAsync();

            playwright = await Playwright.CreateAsync();

            newBrowser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            newBrowserContext = await newBrowser.NewContextAsync();

            newPage = await newBrowserContext.NewPageAsync();

            // Navigate to the copied URL
            await newPage.GotoAsync(sharingUrl);

            await newPage.GetByRole(AriaRole.Button, new() { Name = "Start" }).ClickAsync();

            if(certificate == "Framework") {
                await Assertions.Expect(newPage.GetByText("Amelia Parker")).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByRole(AriaRole.Heading, new() { Name = "Hospitality" })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("This verifies successful completion of the apprenticeship.")).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("Amelia", new() { Exact = true })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("Parker", new() { Exact = true })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByRole(AriaRole.Definition).Filter(new() { HasTextRegex = new Regex("^Hospitality$") })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("Hospitality Retail Outlet")).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("Advanced")).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("Lancaster and Morecambe")).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("January 2019")).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByRole(AriaRole.Heading, new() { Name = "Download certificate" })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByRole(AriaRole.Link, new() { Name = "Download certificate (PDF)" })).ToBeVisibleAsync();
            } else if(certificate == "Standard")
            {
                await Assertions.Expect(newPage.GetByText("Oliver Turner")).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByRole(AriaRole.Heading, new() { Name = "Accountancy or taxation professional" })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("This verifies successful completion of the apprenticeship.")).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("Oliver", new() { Exact = true })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("Turner", new() { Exact = true })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByRole(AriaRole.Definition).Filter(new() { HasTextRegex = new Regex("^Accountancy or taxation professional$") })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("7", new() { Exact = true })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("KAPLAN FINANCIAL LIMITED")).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByText("January 2020")).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByRole(AriaRole.Heading, new() { Name = "Download certificate" })).ToBeVisibleAsync();
                await Assertions.Expect(newPage.GetByRole(AriaRole.Link, new() { Name = "Download certificate (PDF)" })).ToBeVisibleAsync();
            }
            
           
            await newPage.CloseAsync();
            await newBrowserContext.CloseAsync();
            await newBrowser.CloseAsync();
            

            return await VerifyPageAsync(() => new DigiCertsCreateNewSharingLinkPage(context));
        }

        public async Task<DigiCertsEmailSentPage> sentEmail()
        {
            await page.Locator("#EmailAddress").ClickAsync();
            await page.Locator("#EmailAddress").FillAsync("arunkumar.selvasundaram@education.gov.uk");

            await page.GetByText("Share by email Enter an email address Send email").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Send email" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and send" }).ClickAsync();

            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Email sent" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "What happens next" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Your certificate has been")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("We’ve sent an email with a")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Return to Sharing link" })).ToBeVisibleAsync();
            
            return await VerifyPageAsync(() => new DigiCertsEmailSentPage(context));
        }

    }
}