using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Globalization;

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
                    await page2.AssertChangeHistoryRow(DateTime.Now, "Status change from Live to Stopped", "Auto approved");
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
                    await page2.AssertChangeHistoryRow(DateTime.Now, $"Learning has been paused on {expectedDate.ToString("d MMM yyyy", CultureInfo.InvariantCulture)}", "Auto approved");
                    break;
                default:
                    break;
            }

            await page.ReturnBackToManageYourApprenticesPage();
        }

        [Then(@"^provider verifies that Employment Status is ""(.*)""$")]
        public async Task ThenProviderVerifiesThatEmploymentStatusIs(string expectedEmploymentStatus)
        {
            var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var apprenticeName = apprenticeship.ApprenticeDetails.FullName;

            //verify Employment Verification status on 'Manage your learners' page:
            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            await new ProviderHomePage(context).GoToProviderManageYourApprenticePage();
            var page = await new ManageYourLearners_ProviderPage(context).SearchApprentice(apprenticeName);            
            Assert.IsTrue(await page.GetEmploymentStatus() == expectedEmploymentStatus, "Employment Verification status on 'Manage your learners' page");

            //verify Employment Verification status on 'Learner details' page:
            var page1 = await page.OpenFirstItemFromTheList(apprenticeName);
            if (expectedEmploymentStatus == "blank")
            {
                Assert.IsFalse(await page1.IsEmploymentStatusVisible(), "Employment Verification status is not visible on 'Learner details' page");
            }
            else
            {
                Assert.IsTrue(await page1.GetEmploymentStatus() == expectedEmploymentStatus, "Employment Verification status on 'Learner details' page");
            }


        }



    }

}
