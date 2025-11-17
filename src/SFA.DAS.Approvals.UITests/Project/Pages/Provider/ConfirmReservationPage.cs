using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ConfirmReservationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm your reservation");
        }

        internal async Task<YouHaveSuccessfullyReservedFundingForApprenticeshipTrainingPage> ClickConfirmButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
            
            return await VerifyPageAsync(() => new YouHaveSuccessfullyReservedFundingForApprenticeshipTrainingPage(context));
        }


    }
}
