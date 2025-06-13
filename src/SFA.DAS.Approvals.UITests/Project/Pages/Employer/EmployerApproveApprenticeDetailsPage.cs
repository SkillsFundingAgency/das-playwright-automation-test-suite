using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class EmployerApproveApprenticeDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator organisation => page.Locator("dt:has-text('Organisation') + dd");
        private ILocator reference => page.Locator("dt:has-text('Reference') + dd");
        private ILocator status => page.Locator("dt:has-text('Status') + dd");
        private ILocator messageFromProvider => page.Locator(".govuk-inset-text").First;
        private ILocator approveRadioOption => page.Locator("label:has-text('Yes, approve and notify training provider')");
        private ILocator doNotApproveRadioOption => page.Locator("label:has-text('No, request changes from training provider')");
        private ILocator messageToEmployerTextBox => page.Locator(".govuk-textarea").First;
        private ILocator saveAndSubmitButton => page.Locator("button:has-text('Save and submit')");
        private ILocator saveAndexitLink => page.Locator("a:has-text('Save and exit')");
        #endregion


        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Approve apprentice details");
        }

        public async Task VerifyCohort(Apprenticeship apprenticeship)
        {
            await Assertions.Expect(organisation).ToHaveTextAsync(apprenticeship.EmployerDetails.EmployerName.ToString());
            await Assertions.Expect(reference).ToHaveTextAsync(apprenticeship.CohortReference);
            await Assertions.Expect(status).ToHaveTextAsync("Ready for approval");
            await Assertions.Expect(messageFromProvider).ToHaveTextAsync("Please review the details and approve the request.");
        }

        public async Task<ApprenticeDetailsApproved> EmployerApproveCohort()
        {
            await approveRadioOption.ClickAsync();
            await saveAndSubmitButton.ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeDetailsApproved(context));
        }
    }
}
