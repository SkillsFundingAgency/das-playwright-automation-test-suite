using Azure;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class PausePaymentsToYourTrainingProviderPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;
                
        internal PausePaymentsToYourTrainingProviderPage(ScenarioContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Pause payments to your training provider");
        }

        private async Task VerifyDetails()
        { 
        
        }

        internal async Task<PaymentsPausedConfirmationPage> VerifyDetailsAndPauseRecord()
        {
            await VerifyDetails();
            await page.GetByLabel("Reason for pausing payments").SelectOptionAsync(new[] { "1" });
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, pause payments" }).CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm changes" }).ClickAsync();
            return await VerifyPageAsync(() => new PaymentsPausedConfirmationPage(context));
        }



    
    }
}
