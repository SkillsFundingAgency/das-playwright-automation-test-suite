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
        private ILocator selectApprenticesFromILR => page.Locator("text=/Select apprentice[s]? from ILR/");

        private ILocator ContinueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add apprentice details");
        }

        public async Task<AddApprenticeDetails_SelectJourneyPage> SelectOptionToApprenticesFromILR()
        {
            await selectApprenticesFromILR.CheckAsync();

            await ContinueButton.ClickAsync();

            return await VerifyPageAsync(() => new AddApprenticeDetails_SelectJourneyPage(context));
        }

        public async Task<SelectApprenticeFromILRPage> SelectOptionToAddApprenticesFromILRList_AddAnotherApprenticeRoute()
        {
            await selectApprenticesFromILR.CheckAsync();

            await ContinueButton.ClickAsync();

            return await VerifyPageAsync(() => new SelectApprenticeFromILRPage(context));
        }



    }

}
