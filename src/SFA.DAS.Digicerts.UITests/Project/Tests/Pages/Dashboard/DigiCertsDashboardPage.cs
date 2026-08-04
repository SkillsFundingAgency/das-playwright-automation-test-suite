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


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Dashboard
{
    public class DigiCertsDashboardPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your apprenticeship courses");


        public async Task<DigiCertsDashboardPage> checkStandardDashboardPageElements()
        {
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Business administrator" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Accounting finance manager" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Select a course to view your")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("If you're waiting for the")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("If any of these details are")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "contact us" })).ToBeVisibleAsync();

            return await VerifyPageAsync(() => new DigiCertsDashboardPage(context));
        }

        public async Task<DigiCertsDashboardPage> checkFrameworkDashboardPageElements()
        {
            await Assertions.Expect(page.GetByText("Your apprenticeship courses")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Select a course to view your")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Network engineer" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Hospitality" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Cell, new() { Name = "4" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Cell, new() { Name = "Advanced" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("If you're waiting for the")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("If any of these details are")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "contact us" })).ToBeVisibleAsync();

            return await VerifyPageAsync(() => new DigiCertsDashboardPage(context));
        }


        public async Task<DigiCertsStandardDetailsPage> clickStandardCertificate()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Business administrator" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsStandardDetailsPage(context));
        }

        public async Task<DigiCertsFrameworkDetailsPage> clickFrameworkCertificate()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Hospitality" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsFrameworkDetailsPage(context));
        }
    }
}