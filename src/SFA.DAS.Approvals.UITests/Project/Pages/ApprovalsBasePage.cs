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

        
    }

}
