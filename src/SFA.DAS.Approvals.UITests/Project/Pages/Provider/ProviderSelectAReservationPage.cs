using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ProviderSelectAReservationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Select a Reservation");
        }


        internal async Task<SelectApprenticeFromILRPage> SelectReservation(string reservationId)
        {
            await page.Locator($"input[type='radio'][value='{reservationId}']").ClickAsync();
            await page.Locator("button:has-text('Continue')").ClickAsync();
            return await VerifyPageAsync(() => new SelectApprenticeFromILRPage(context));
        }   

    }
}
