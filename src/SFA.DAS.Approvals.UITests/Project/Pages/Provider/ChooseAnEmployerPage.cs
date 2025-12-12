using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ChooseAnEmployerPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose an employer");
        }

        internal async Task<ConfirmEmployerPage> ChooseAnEmployer(string agreementId)
        {
            await page.GetByRole(AriaRole.Row, new() { Name = agreementId }).GetByRole(AriaRole.Link).ClickAsync();

            return await VerifyPageAsync(() => new ConfirmEmployerPage(context));

        }

        internal async Task<bool> EmployerExistsInTheList(string agreementId) => await page.GetByRole(AriaRole.Row, new() { Name = agreementId }).IsVisibleAsync();

    }

}
