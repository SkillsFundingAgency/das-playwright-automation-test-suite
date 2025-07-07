using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ViewApprenticeDetails_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            var heading = @"^View(\s\d+)?\s(apprentice|apprentices') details$";
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(heading);
        }






    }
}
