using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SFA.DAS.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static System.Net.Mime.MediaTypeNames;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages
{
    public class CertificatePage(ScenarioContext context) : BasePage(context)
    {
      

        private ILocator StartButton => page.GetByRole(AriaRole.Button, new() { Name = "Start" });
        private ILocator IdTextbox => page.GetByRole(AriaRole.Textbox, new() { Name = "Id" });
        private ILocator EmailTextbox => page.GetByRole(AriaRole.Textbox, new() { Name = "Email" });
        private ILocator PhoneTextbox => page.GetByRole(AriaRole.Textbox, new() { Name = "Phone" });
        private ILocator FileUpload => page.Locator("input[type='file']");
        private ILocator AuthenticateButton => page.GetByRole(AriaRole.Button, new() { Name = "Authenticate" });
        private ILocator ContinueLink => page.GetByRole(AriaRole.Link, new() { Name = "Continue" });
        private ILocator VerifyLink => page.GetByRole(AriaRole.Link, new() { Name = "Verify" });

        public async Task Navigate()
        {
            await page.GotoAsync("https://test-certificates.apprenticeships.education.gov.uk/start-page");
        }

        public async Task ClickStart()
        {
            await StartButton.ClickAsync();
        }

        public async Task EnterAuthenticationDetails()
        {
            await IdTextbox.FillAsync("urn:fdc:gov.uk:2022:ruFjQz8uSpWAo2U0gxNszmm7zsRogQlvg5umbuWpYHA");
            await EmailTextbox.FillAsync("chris+sfa+one@humesoftware.com");
            await PhoneTextbox.FillAsync("9000804197");
        }

        public async Task UploadJson()
        {
            await FileUpload.SetInputFilesAsync("Import.json");
        }

        public async Task Authenticate()
        {
            await AuthenticateButton.ClickAsync();
        }

        public async Task Continue()
        {
            await ContinueLink.ClickAsync();
        }

        public async Task ClickVerify()
        {
            await VerifyLink.ClickAsync();
        }

        public async Task OpenCertificate(string name)
        {
            await page.GetByRole(AriaRole.Link, new() { Name = name }).ClickAsync();
        }

        public async Task ClickBack()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Back" }).ClickAsync();
        }

        public override Task VerifyPage()
        {
            throw new NotImplementedException();
        }
    }
}
