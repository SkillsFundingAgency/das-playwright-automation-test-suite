using Azure;
using Polly;
using SFA.DAS.Approvals.APITests.Project.Tests.StepDefinitions;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper
{
    public class ProviderStepsHelper
    {
        private readonly ScenarioContext context;
        private List<Apprenticeship> listOfApprenticeship;

        public ProviderStepsHelper(ScenarioContext _context)
        {
            context = _context;
            listOfApprenticeship = _context.GetValue<List<Apprenticeship>>();
        }

        internal async Task<ApproveApprenticeDetailsPage> AddFirstApprenticeFromILRList(SelectApprenticeFromILRPage selectApprenticeFromILRPage)
        {
            var apprenticeship = listOfApprenticeship.FirstOrDefault();

            var page = await selectApprenticeFromILRPage.SelectApprenticeFromILRList(apprenticeship);

            await page.ValidateApprenticeDetailsMatchWithILRData(apprenticeship);

            var page1 = await page.ClickAddButton();

            return await page1.SelectNoForRPL();
        }

        internal async Task ProviderApproveCohort(ApproveApprenticeDetailsPage approveApprenticeDetailsPage)
        {
            var apprenticeship = listOfApprenticeship.FirstOrDefault();

            await approveApprenticeDetailsPage.VerifyCohort(apprenticeship);

            var page1 = await approveApprenticeDetailsPage.ProviderApproveCohort();

            await page1.VerifyCohortApprovedAndSentToEmployer(apprenticeship);

            await page1.GoToApprenticeRequests();

        }



    }
}
