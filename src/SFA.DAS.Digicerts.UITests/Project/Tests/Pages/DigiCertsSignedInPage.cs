using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework;
using SFA.DAS.Framework;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages
{
    public class DigiCertsSignedInPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprenticeship Certificates - Signed in stub");


        public async Task<DigiCertsCheckIdentityPage> clickContinue()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsCheckIdentityPage(context));
        }

        public async Task ClickSignOut()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();
        }

    }
}