using Azure;
using Polly;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class PausePaymentsToYourTrainingProviderPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;
        private ILocator ApprenticeName => page.Locator("dt:has-text('Name') + dd");
        private ILocator ULN => page.Locator("dt:has-text('Unique learner number') + dd");
        private ILocator TrainingCourse => page.Locator("dt:has-text('Training course') + dd");
        private ILocator PauseDate => page.Locator("dt:has-text('Pause date') + dd");

        internal PausePaymentsToYourTrainingProviderPage(ScenarioContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Pause payments to your training provider");
        }

        internal async Task<PaymentsPausedConfirmationPage> VerifyDetailsAndPauseRecord(Apprenticeship apprenticeship)
        {
            await VerifyDetails(apprenticeship);
            await page.GetByLabel("Reason for pausing payments").SelectOptionAsync(new[] { "1" });
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, pause payments" }).CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm changes" }).ClickAsync();
            return await VerifyPageAsync(() => new PaymentsPausedConfirmationPage(context));
        }


        private async Task VerifyDetails(Apprenticeship apprenticeship)
        {
            Assert.That(await ApprenticeName.InnerTextAsync(), Is.EqualTo(apprenticeship.ApprenticeDetails.FullName));
            Assert.That(await ULN.InnerTextAsync(), Is.EqualTo(apprenticeship.ApprenticeDetails.ULN));
            Assert.That(await TrainingCourse.InnerTextAsync(), Is.EqualTo(apprenticeship.TrainingDetails.CourseTitle));
            Assert.That(await PauseDate.InnerTextAsync(), Is.EqualTo(DateTime.Now.ToString("MMM yyyy")));
        }

    }
}
