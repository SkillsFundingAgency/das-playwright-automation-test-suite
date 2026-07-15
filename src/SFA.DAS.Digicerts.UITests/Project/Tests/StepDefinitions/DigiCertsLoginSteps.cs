/*using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightTests
{
    public class DigiCertsLoginSteps : PageTest
    {
        [Test]
        public async Task Test()
        {
            await Page.GotoAsync("https://test-certificates.apprenticeships.education.gov.uk/start-page");

            await Page.GetByRole(AriaRole.Button, new() { Name = "Start" }).ClickAsync();

            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Id" }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Id" })
                .FillAsync("urn:fdc:gov.uk:2022:ruFjQz8uSpWAo2U0gxNszmm7zsRogQlvg5umbuWpYHA");

            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" })
                .FillAsync("greataruns@gmail.com");

            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Phone" }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Phone" })
                .FillAsync("9000804197");

            // File upload
            var fileInput = Page.GetByRole(AriaRole.Button, new() { Name = "Upload a JSON file that" });
            await fileInput.ClickAsync();
            await fileInput.SetInputFilesAsync("Import.json");

            await Page.GetByRole(AriaRole.Button, new() { Name = "Authenticate" }).ClickAsync();
            await Page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();
            await Page.GetByRole(AriaRole.Link, new() { Name = "Verify" }).ClickAsync();

            var cell = Page.GetByRole(AriaRole.Cell, new() { Name = "Installation electrician and" });

            await cell.ClickAsync();
            await cell.ClickAsync();
            await cell.DblClickAsync();

            await Page.GetByRole(AriaRole.Link, new() { Name = "Installation electrician and" })
                .DblClickAsync();
        }
    }
}
*/