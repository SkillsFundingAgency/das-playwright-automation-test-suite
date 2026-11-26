using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Authorisation;
using SFA.DAS.Framework;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace SFA.DAS.Digicerts.UITests.Project.Tests.Pages
{
    public class DigiCertsCreateSharingLinkPage(Reqnroll.ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page).ToHaveTitleAsync(new Regex("Create a sharing link"));


        public async Task<DigiCertsCreateSharingLinkPage> verifyCreateSharingLinkPage()
        {
            await Assertions.Expect(page.GetByText("You can create secure links")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("A sharing link includes a")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("You can:")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("share the link by email")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("see when your link has been")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("create multiple sharing links")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Sharing links will")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByText("Find out how we collect and")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "our privacy notice." })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Create new sharing link" })).ToBeVisibleAsync();
           
            return await VerifyPageAsync(() => new DigiCertsCreateSharingLinkPage(context));
        }

        public async Task<DigiCertsCreateNewSharingLinkPage> clickCreateNewSharingLink()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Create new sharing link" }).ClickAsync();

            return await VerifyPageAsync(() => new DigiCertsCreateNewSharingLinkPage(context));
        }
    }
}