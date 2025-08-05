using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages
{
    public abstract class ApprovalsBasePage(ScenarioContext context) : BasePage(context)
    {
        private readonly ApprenticeDataHelper apprenticeDataHelper;
        internal async Task ClickNavBarLinkAsync(string linkName)
        {
            await page.Locator("#navigation").GetByRole(AriaRole.Link, new() { Name = linkName }).ClickAsync();
        }
    }

}
