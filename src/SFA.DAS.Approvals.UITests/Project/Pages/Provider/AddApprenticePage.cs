using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class AddApprenticePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator startNowButton => page.GetByRole(AriaRole.Button, new() { Name = "Start now" });

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Add an apprentice");
        }       


        internal async Task<AddTrainingProviderPage> ClickStartNowButton()
        {
            await startNowButton.ClickAsync();

            return await VerifyPageAsync(() => new AddTrainingProviderPage(context));
        }
    }
}
