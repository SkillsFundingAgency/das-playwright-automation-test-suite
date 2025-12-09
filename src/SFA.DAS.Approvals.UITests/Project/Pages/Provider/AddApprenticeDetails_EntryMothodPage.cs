using Azure;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class AddApprenticeDetails_EntryMothodPage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        #region locators
        private ILocator optionToSelectApprenticesFromILR => page.Locator("text=/Select (learners|apprentices) from ILR/"); 
        private ILocator optionToUploadACsvFile => page.Locator("text=Upload a CSV file");
        private ILocator ContinueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add apprentice details");
        }

        internal async Task<AddApprenticeDetails_SelectJourneyPage> SelectOptionToApprenticesFromILR()
        {
            var page = await SelectOptionToAddApprenticeFromILRAndContinue();
            await page.ClickOnContinueButton();
            return await VerifyPageAsync(() => new AddApprenticeDetails_SelectJourneyPage(context));
        }

        internal async Task<UsingFileUploadPage> SelectOptionToUploadCsvFile()
        {
            await optionToUploadACsvFile.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new UsingFileUploadPage(context));
        }

        internal async Task<ProviderSelectAReservationPage> SelectOptionToAddApprenticesFromILRList_SelectReservationRoute()
        {
            var page = await SelectOptionToAddApprenticeFromILRAndContinue();
            await page.ClickOnContinueButton();
            return await VerifyPageAsync(() => new ProviderSelectAReservationPage(context));
        }

        internal async Task SelectOptionToAddApprenticesFromILRList_InsufficientPermissionsRoute()
        {
            await optionToSelectApprenticesFromILR.CheckAsync();
            await ContinueButton.ClickAsync();
        }

        internal async Task SelectOptionToAddApprenticeFromILRAndContinue()
        {
            await optionToSelectApprenticesFromILR.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new BeforeContinuePage(context));
        }

    }

}
