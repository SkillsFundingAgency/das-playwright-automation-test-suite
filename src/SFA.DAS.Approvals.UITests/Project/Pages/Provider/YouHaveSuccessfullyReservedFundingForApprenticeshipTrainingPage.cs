using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class YouHaveSuccessfullyReservedFundingForApprenticeshipTrainingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have successfully reserved funding for apprenticeship training");
        }

        internal async Task<ProviderHomePage> SelectGoToHomePageAndContinue()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Go to homepage" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new ProviderHomePage(context));
            
        }

        internal async Task<AddApprenticeDetails_EntryMothodPage> SelectAddApprenticesDetailsAndContinue()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Add apprentice's details" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new AddApprenticeDetails_EntryMothodPage(context));
        }

        internal async Task GetReservationIdFromUrl(Apprenticeship apprenticeship)
        {
            var url = this.page.Url;
            var match = Regex.Match(url, @"reservations/(?<guid>[a-fA-F0-9\-]{36})");
            string reservationsId = match.Success ? match.Groups["guid"].Value : null;

            apprenticeship.ReservationID = reservationsId;

            await Task.Delay(100);
            context.Set(apprenticeship, "Apprenticeship");
        }





    }
}
