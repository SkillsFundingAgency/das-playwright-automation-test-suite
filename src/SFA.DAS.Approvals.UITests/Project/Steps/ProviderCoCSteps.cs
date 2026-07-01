using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    internal class ProviderCoCSteps
    {
        private readonly ScenarioContext context;
        private readonly ProviderStepsHelper providerStepsHelper;
        private readonly DbSteps dbSteps;
        private const string ChangesForReviewApprentice = "DoNotUse_TestData ChangeStatusApprentice";

        public ProviderCoCSteps(ScenarioContext _context)
        {
            context = _context;
            providerStepsHelper = new ProviderStepsHelper(context);
            dbSteps = new DbSteps(context);
        }

        [Then("^the user can view details of the apprenticeship on apprenticeship details page$")]
        public async Task ThenTheUserCanViewDetailsOfTheApprenticeshipOnApprenticeshipDetailsPage()
        {
            var page = await new ManageYourLearners_ProviderPage(context).SelectViewCurrentApprenticeDetails(ChangesForReviewApprentice);
            await page.ReturnBackToManageYourApprenticesPage();
        }


    }

}
