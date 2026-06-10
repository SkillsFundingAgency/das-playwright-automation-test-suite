using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ProviderSelectAReservationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator entryMethod => page.GetByLabel(new Regex("Choose details from ILR \\(individual learner record\\)|Select apprentices from ILR|Add apprentice details", RegexOptions.IgnoreCase));

        public override async Task VerifyPage()
        {
            if (await entryMethod.IsVisibleAsync())     //this condition to be removed when APPMAN-1741 feature is rolled out
            {
                await entryMethod.CheckAsync();
                await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            }

            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Choose a reservation");
        }


        internal async Task<SelectLearnerFromILRPage> SelectReservation(string reservationId)
        {
            if(string.IsNullOrEmpty(reservationId))
                reservationId = "99999999-9999-9999-9999-999999999999";     //follow auto reservation route

            await page.Locator($"input[type='radio'][value='{reservationId}']").ClickAsync();
            await page.Locator("button:has-text('Continue')").ClickAsync();
            return await VerifyPageAsync(() => new SelectLearnerFromILRPage(context));
        }   

    }
}
