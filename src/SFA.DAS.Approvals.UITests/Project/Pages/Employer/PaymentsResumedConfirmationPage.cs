using Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class PaymentsResumedConfirmationPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;

        internal PaymentsResumedConfirmationPage(ScenarioContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Payments resumed");
        }

        internal async Task ClickViewLearnerDetailLink()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "View learner details" }).ClickAsync();
        }


    }
}
