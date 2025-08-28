using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class AccessDenied_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You need a different service role to view this page");

        internal async Task GoBackToHomePage() => await page.GetByRole(AriaRole.Link, new() { Name = "homepage of this service." }).ClickAsync();

    }
}
