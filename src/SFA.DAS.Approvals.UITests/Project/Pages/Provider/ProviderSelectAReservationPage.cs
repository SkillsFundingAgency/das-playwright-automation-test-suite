using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ProviderSelectAReservationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator entryMethod => page.GetByText(new Regex("Select (learners|apprentices) from ILR"));

        public override async Task VerifyPage()
        {
            if (await entryMethod.IsVisibleAsync())     //this condition to be removed when APPMAN-1741 feature is rolled out
            {
                await entryMethod.CheckAsync();
                await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            }

            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Select a Reservation");
        }


        internal async Task<SelectLearnerFromILRPage> SelectReservation(string reservationId)
        {
            await page.Locator($"input[type='radio'][value='{reservationId}']").ClickAsync();
            await page.Locator("button:has-text('Continue')").ClickAsync();
            return await VerifyPageAsync(() => new SelectLearnerFromILRPage(context));
        }   

    }
}
