using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Tests.Pages.Provider
{
    internal class AddApprenticeDetailsPage(ScenarioContext context) : ApprovalsProviderBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Add apprentice details");
        }


    }
}
