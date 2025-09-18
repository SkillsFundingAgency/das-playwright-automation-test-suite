using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class AddApprenticeDetails_SelectJourneyPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator optionToCreateANewCohort => page.GetByRole(AriaRole.Radio, new() { Name = "Create a new cohort" });
        private ILocator optionToAddToAnExistingCohort => page.GetByRole(AriaRole.Radio, new() { Name = "Add to an existing cohort" });
        private ILocator continueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        #endregion
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add apprentice details");
        }

        internal async Task<ChooseAnEmployerPage> SelectOptionCreateANewCohort()
        {
            await optionToCreateANewCohort.CheckAsync();
            await continueButton.ClickAsync();
            return await VerifyPageAsync(() => new ChooseAnEmployerPage(context));
        }

        internal async Task<ChooseACohortPage> SelectOptionUseExistingCohort()
        {
            await optionToAddToAnExistingCohort.CheckAsync();
            await continueButton.ClickAsync();
            return await VerifyPageAsync(() => new ChooseACohortPage(context));
        }

        internal async Task<bool> IsAddToAnExistingCohortOptionDisplayed() => await optionToAddToAnExistingCohort.IsVisibleAsync();
        
        internal async Task<bool> IsCreateANewCohortOptionDisplayed() => await optionToCreateANewCohort.IsVisibleAsync();
    
    
    }
}
