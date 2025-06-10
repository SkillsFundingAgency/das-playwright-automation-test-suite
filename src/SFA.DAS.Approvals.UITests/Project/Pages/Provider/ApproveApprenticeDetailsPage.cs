using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Runtime.CompilerServices;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ApproveApprenticeDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator employerName => page.Locator("dt:has-text('Employer') + dd");
        private ILocator cohortReference => page.Locator("dt:has-text('Cohort reference') + dd");
        private ILocator status => page.Locator("dt:has-text('Status') + dd");
        private ILocator message => page.Locator("h2:has-text('Message') + div.govuk-inset-text");
        private ILocator approveRadioOption => page.Locator("label:has-text('Yes, approve and notify employer')");
        private ILocator doNotApproveRadioOption => page.Locator("label:has-text('No, save and return to apprentice requests')");
        private ILocator messageToEmployerTextBox => page.Locator(".govuk-textarea").First;
        private ILocator saveAndSubmitButton => page.Locator("button:has-text('Save and submit')");
        private ILocator saveAndexitLink => page.Locator("a:has-text('Save and exit')");
        #endregion


        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Approve apprentice details");
        }

        public async Task VerifyCohort()
        {
            var apprenitceship = context.GetValue<Apprenticeship>();

            //verify employer name, cohort status and message:
            await Assertions.Expect(employerName).ToHaveTextAsync(apprenitceship.EmployerDetails.EmployerName.ToString());
            await Assertions.Expect(status).ToHaveTextAsync("New request");
            await Assertions.Expect(message).ToHaveTextAsync("No message added.");


            //get cohort reference; set it in the context;
            var cohortRef = await cohortReference.InnerTextAsync();
            apprenitceship.CohortReference = cohortRef;
            context.Set(apprenitceship, "Apprenticeship");
        }



        public async Task<CohortApprovedAndSentToEmployerPage> ProviderApproveCohort()
        {
            await approveRadioOption.ClickAsync();
            await messageToEmployerTextBox.FillAsync("Please review the details and approve the request.");
            await saveAndSubmitButton.ClickAsync();
            //return await VerifyPageAsync(() => new CohortApprovedAndSentToEmployerPage(context));
            return new CohortApprovedAndSentToEmployerPage(context);
        }


    }
}
