using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    internal class EmployerCoCSteps
    {
        private readonly ScenarioContext context;
        private readonly EmployerStepsHelper employerStepsHelper;

        public EmployerCoCSteps(ScenarioContext _context)
        {
            context = _context;
            employerStepsHelper = new EmployerStepsHelper(context);
        }

        [Then(@"^employer verifies that record has been ""(.*)"" in Employer portal")]
        public async Task ThenEmployerVerifiesThatRecordHasBeenInEmployerPortal(string status)
        {
            var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var fullName = apprenticeship.ApprenticeDetails.FullName;
            var page = await employerStepsHelper.CheckLearnerOnManageYourLearnersPage(true);
            var page1 = await page.OpenFirstItemFromTheList(fullName);

            switch (status)
            {
                case "Completed":
                    await page1.EmployerVerifyApprenticeStatus(ApprenticeshipStatus.Completed, "Completion payment month", DateTime.Now);
                    //Assert editiability of completed record:
                    Assert.False(await page1.IsEditStatusLinkAvailable(), "IsEditStatusLinkAvailable");
                    Assert.False(await page1.IsEditPaymentStatusLinkAvailable(), "IsEditPaymentStatusLinkAvailable");
                    Assert.False(await page1.IsChangeProviderLinkAvailable(), "IsChangeProviderLinkAvailable");
                    Assert.False(await page1.IsEditApprenticeDetailsLinkAvailable(), "IsEditApprenticeDetailsLinkAvailable");
                    Assert.False(await page1.IsEditVersionLinkAvailable(), "IsEditVersionLinkAvailable");
                    Assert.True(await page1.IsEditPlannedTrainingEndDateLinkAvailable(), "IsEditPlannedTrainingEndDateLinkAvailable");
                    break;
                case "Stopped":
                    await page1.EmployerVerifyApprenticeStatus(ApprenticeshipStatus.Stopped, "Stopped date", DateTime.Now);
                    //Assert editiability of stopped record:
                    Assert.False(await page1.IsEditStatusLinkAvailable(), "IsEditStatusLinkAvailable");
                    Assert.False(await page1.IsEditPaymentStatusLinkAvailable(), "IsEditPaymentStatusLinkAvailable");
                    Assert.True(await page1.IsChangeProviderLinkAvailable(), "IsChangeProviderLinkAvailable");
                    Assert.False(await page1.IsEditApprenticeDetailsLinkAvailable(), "IsEditApprenticeDetailsLinkAvailable");
                    Assert.False(await page1.IsEditVersionLinkAvailable(), "IsEditVersionLinkAvailable");
                    Assert.False(await page1.IsEditPlannedTrainingEndDateLinkAvailable(), "IsEditPlannedTrainingEndDateLinkAvailable");
                    //Check history logs:
                    var page2 = await page1.ClickOnViewChangeHistoryLink(fullName);
                    await page2.AssertChangeHistoryRow(DateTime.Now, "ILR Learner status changed from Live to Withdrawn", "Auto approved");
                    break;
                case "Paused":
                    await page1.EmployerVerifyApprenticeStatus(ApprenticeshipStatus.Paused, "Apprenticeship pause date", DateTime.Now);
                    break;
                default:
                    break;
            }
        }

    }
}
