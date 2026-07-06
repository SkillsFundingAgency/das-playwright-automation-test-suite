using Azure;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ResumePaymentsToTrainingProviderPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;
                
        internal ResumePaymentsToTrainingProviderPage(ScenarioContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Resume payments to training provider");
        }

        private async Task VerifyDetails()
        { 
        
        }

        internal async Task<PaymentsResumedConfirmationPage> VerifyDetailsAndResumeRecord()
        {
            await VerifyDetails();
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, resume payments" }).CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm changes" }).ClickAsync();
            return await VerifyPageAsync(() => new PaymentsResumedConfirmationPage(context));
        }



    
    }
}
