using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class AddTrainingProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator ukprnInputBox => page.Locator("#Ukprn");
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Select your training provider");
        }

        public async Task<ConfirmTrainingProvider> SubmitValidUkprn(int Ukprn)
        {
            await ukprnInputBox.ClickAsync();
            await ukprnInputBox.FillAsync(Ukprn.ToString());
            var option = page.Locator($"li[role='option']:has-text(\"{Ukprn}\")");
            await option.ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new ConfirmTrainingProvider(context)); 
        }
        
    }
}
