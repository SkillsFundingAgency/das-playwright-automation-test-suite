using Polly;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class ProviderSteps
    {
        private readonly ScenarioContext context;
        private readonly ProviderStepsHelper providerStepsHelper;

        public ProviderSteps(ScenarioContext _context)
        {
            context = _context;
            providerStepsHelper = new ProviderStepsHelper(context);
        }


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

            await providerStepsHelper.AddFirstApprenticeFromILRList(page4);  
            
        }


        [Then("Provider can send it to the Employer for approval")]
        public async Task ThenProviderCanSendItToTheEmployerForApproval()
        {
            //provider add another apprentice to the cohort   <-- to be added

            var page = new ApproveApprenticeDetailsPage(context);

            await providerStepsHelper.ProviderApproveCohort(page);

            

        }

        [Then(@"apprentice request \(cohort\) is available under \'Apprentice requests \>\ With employers\'")]
        public async Task ThenApprenticeRequestCohortIsAvailableUnder()
        {
            throw new PendingStepException();
        }


        [Then(@"apprentice request \(cohort\) is no longer available under any tab in \'Apprentice requests\' section")]
        public async Task ThenApprenticeRequestCohortIsNoLongerAvailableUnderAnyTabInSection()
        {
            throw new PendingStepException();
        }


    }
}
