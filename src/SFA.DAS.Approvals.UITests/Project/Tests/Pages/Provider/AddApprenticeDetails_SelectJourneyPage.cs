using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Tests.Pages.Provider
{
    public class AddApprenticeDetails_SelectJourneyPage(ScenarioContext context) : ApprovalsProviderBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add apprentice details");
        }
    }
}
