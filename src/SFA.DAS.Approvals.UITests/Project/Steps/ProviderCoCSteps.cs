using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    internal class ProviderCoCSteps
    {
        private readonly ScenarioContext context;
        private readonly ProviderStepsHelper providerStepsHelper;

        public ProviderCoCSteps(ScenarioContext _context)
        {
            context = _context;
            providerStepsHelper = new ProviderStepsHelper(context);            
        }

        [Then(@"^Provider verifies that recrod status stays as ""(.*)""")]
        [Then(@"^provider verifies that record is set as ""(.*)"" in Provider portal")]
        public async Task ThenProviderVerifiesThatRecordIsSetAsInProviderPortal(string status)
        {
            var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var apprenticeName = apprenticeship.ApprenticeDetails.FullName;
            var expectedDate = apprenticeship.TrainingDetails.StopDate;

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            await new ProviderHomePage(context).GoToProviderManageYourApprenticePage();
            var page = await new ManageYourLearners_ProviderPage(context).SelectViewCurrentApprenticeDetails(apprenticeName);

            switch (status)
            {
                case "Live":
                    await page.ProviderVerifyApprenticeStatus(ApprenticeshipStatus.Live, null);
                      break;
                case "Stopped":
                    await page.ProviderVerifyApprenticeStatus(ApprenticeshipStatus.Stopped, expectedDate);
                    //verify editability:
                    Assert.True(await page.IsChangeHistoryLinkVisible(), "IsChangeHistoryLinkVisible");
                    Assert.False(await page.IsEditApprenticeDetailsLinkVisible(), "IsEditApprenticeDetailsLinkVisible");
                    Assert.True(await page.IsChangeOfEmployerLinkVisible(), "IsChangeOfEmployerLinkVisible");
                    Assert.False(await page.IsChangeOfVersionLinkVisible(), "IsChangeOfVersionLinkVisible");
                    //verify history logs:
                    var page2 = await page.ClickOnViewChangeHistoryLink(apprenticeName);
                    await page2.AssertChangeHistoryRow(DateTime.Now, "ILR Learner status changed from Live to Withdrawn", "Auto approved");
                    break;
                case "Completed":
                    await page.ProviderVerifyApprenticeStatus(ApprenticeshipStatus.Completed, DateTime.Now);
                    //verify editability:
                    Assert.False(await page.IsChangeHistoryLinkVisible(), "IsChangeHistoryLinkVisible");
                    Assert.False(await page.IsEditApprenticeDetailsLinkVisible(), "IsEditApprenticeDetailsLinkVisible");
                    Assert.False(await page.IsChangeOfEmployerLinkVisible(), "IsChangeOfEmployerLinkVisible");
                    Assert.False(await page.IsChangeOfVersionLinkVisible(), "IsChangeOfVersionLinkVisible");
                    break;
                case "Paused":
                    await page.ProviderVerifyApprenticeStatus(ApprenticeshipStatus.Paused, expectedDate);
                    //verify editability:
                    Assert.True(await page.IsChangeHistoryLinkVisible(), "IsChangeHistoryLinkVisible");
                    Assert.True(await page.IsEditApprenticeDetailsLinkVisible(), "IsEditApprenticeDetailsLinkVisible");
                    Assert.True(await page.IsChangeOfEmployerLinkVisible(), "IsChangeOfEmployerLinkVisible");
                    Assert.False(await page.IsChangeOfVersionLinkVisible(), "IsChangeOfVersionLinkVisible");
                    //verify history logs:
                    page2 = await page.ClickOnViewChangeHistoryLink(apprenticeName);
                    await page2.AssertChangeHistoryRow(DateTime.Now, $"Learning has been paused on {expectedDate.ToString("d MMM yyyy")}", "Auto approved");
                    break;
                default:
                    break;
            }

            await page.ReturnBackToManageYourApprenticesPage();
        }




    }

}
