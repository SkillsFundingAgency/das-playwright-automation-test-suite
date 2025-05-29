using SFA.DAS.Approvals.UITests.Project.Tests.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Tests.Steps
{
    [Binding]
    public class ProviderSteps(ScenarioContext context)
    {
        [Given(@"the provider logs into portal")]
        [When("Provider logs into Provider-Portal")]
        public async Task GivenTheProviderLogsIntoPortal() => await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);

        [When(@"creates an apprentice request \(cohort\) by selecting same apprentices")]
        public async Task WhenCreatesAnApprenticeRequestCohortBySelectingSameApprentices()
        {
            var page = await new ProviderHomePage(context).GotoSelectJourneyPage();

            var page1 = await new AddApprenticeDetails_EntryMothodPage(context).SelectOptionToApprenticesFromILR();

            var page2 = await page1.SelectOptionCreateANewCohort();

            var page3 = await page2.ChooseLevyEmployer();

            var page4 = await page3.ConfirmEmployer();

            var page5 = await page4.SelectApprenticeFromILRList();

            await page5.ValidateApprenticeDetailsMatchWithILRData();

            //verify apprentice details are correct
            //Click on Add button
            //Approve cohort
            //Save Cohort Ref
        }

        
        [Then("Provider can send it to the Employer for approval")]
        public void ThenProviderCanSendItToTheEmployerForApproval()
        {
            throw new PendingStepException();
        }

        [Then(@"apprentice request \(cohort\) is available under \'Apprentice requests \>\ With employers\'")]
        public void ThenApprenticeRequestCohortIsAvailableUnder()
        {
            throw new PendingStepException();
        }

        [When("Employer approves the cohort")]
        public void WhenEmployerApprovesTheCohort()
        {
            throw new PendingStepException();
        }
        
        [Then(@"apprentice request \(cohort\) is no longer available under any tab in \'Apprentice requests\' section")]
        public void ThenApprenticeRequestCohortIsNoLongerAvailableUnderAnyTabInSection()
        {
            throw new PendingStepException();
        }
        

    }
}
