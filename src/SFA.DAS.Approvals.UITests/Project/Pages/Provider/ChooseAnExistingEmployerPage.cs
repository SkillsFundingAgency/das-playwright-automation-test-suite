using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ChooseAnExistingEmployerPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose a cohort");
        }

        internal async Task<ApproveApprenticeDetailsPage> ChooseAnExistingEmployer(string cohortReference)
        {
            var linkId = $"#details_link_{cohortReference}";
            await page.Locator(linkId).ClickAsync();
            return await VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }
    }
}
