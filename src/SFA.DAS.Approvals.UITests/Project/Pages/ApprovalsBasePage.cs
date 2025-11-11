using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages
{
    public abstract class ApprovalsBasePage(ScenarioContext context) : BasePage(context)
    {
        public async Task ClickOnNavBarLinkAsync(string linkName)
        {
            await page.Locator("#navigation").GetByRole(AriaRole.Link, new() { Name = linkName }).ClickAsync();
        }

        public async Task NavigateBrowserBack()
        {
            await page.GoBackAsync();
        }

        internal async Task ClickOnLink(string linkText) => await page.GetByRole(AriaRole.Link, new() { Name = $"{linkText}", Exact = true }).ClickAsync();

        internal async Task ClickOnButton(string linkText) => await page.GetByRole(AriaRole.Button, new() { Name = $"{linkText}"}).ClickAsync();

    }

}
