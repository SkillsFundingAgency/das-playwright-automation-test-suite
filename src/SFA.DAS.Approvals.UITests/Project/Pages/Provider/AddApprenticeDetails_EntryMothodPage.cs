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
        private ILocator optionToSelectApprenticesFromILR => page.Locator("text=Select learners from ILR");     //("text=/Select apprentice[s]? from ILR/");
        private ILocator optionToUploadACsvFile => page.Locator("text=Upload a CSV file");
        private ILocator ContinueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add apprentice details");
        }

        internal async Task<AddApprenticeDetails_SelectJourneyPage> SelectOptionToApprenticesFromILR()
        {
            await SelectOptionToAddApprenticeFromILRAndContinue();
            return await VerifyPageAsync(() => new AddApprenticeDetails_SelectJourneyPage(context));
        }

        internal async Task<UsingFileUploadPage> SelectOptionToUploadCsvFile()
        {
            await optionToUploadACsvFile.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new UsingFileUploadPage(context));
        }

        internal async Task<SelectLearnerFromILRPage> SelectOptionToAddApprenticesFromILRList_AddAnotherApprenticeRoute()
        {
            await SelectOptionToAddApprenticeFromILRAndContinue();
            return await VerifyPageAsync(() => new SelectLearnerFromILRPage(context));
        }

        internal async Task<SelectLearnerFromILRPage> SelectOptionToAddApprenticesFromILRList_NonLevyRoute()
        {
            await SelectOptionToAddApprenticeFromILRAndContinue();
            return await VerifyPageAsync(() => new SelectLearnerFromILRPage(context));
        }

        internal async Task<ProviderSelectAReservationPage> SelectOptionToAddApprenticesFromILRList_SelectReservationRoute()
        {
            await SelectOptionToAddApprenticeFromILRAndContinue();
            return await VerifyPageAsync(() => new ProviderSelectAReservationPage(context));
        }

        private async Task SelectOptionToAddApprenticeFromILRAndContinue()
        {
            await optionToSelectApprenticesFromILR.CheckAsync();
            await ContinueButton.ClickAsync();
        }

    }

}
