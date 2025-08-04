using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class AddTrainingProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Select your training provider");
        }

        public async Task<ConfirmTrainingProvider> SubmitValidUkprn()
        {
            var input = page.Locator("#Ukprn");
            await input.ClickAsync();
            await input.FillAsync("10000028");
            var option = page.Locator("li[role='option']:has-text(\"10000028\")");
            await option.ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new ConfirmTrainingProvider(context)); 
        }
        
    }
}
