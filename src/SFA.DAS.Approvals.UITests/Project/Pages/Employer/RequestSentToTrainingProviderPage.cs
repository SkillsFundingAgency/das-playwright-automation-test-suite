using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class RequestSentToTrainingProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator CohortReference => page.Locator("dt:has-text('Reference') + dd");
        private ILocator FundingType => page.Locator("dt:has-text('Funding') + dd");
        private ILocator TrainingProvider => page.Locator("dt:has-text('Training provider') + dd");
        private ILocator NamesOfLearners => page.Locator("dt:has-text('Names of learners') + dd");

        public override async Task VerifyPage()
        {
            var heading = page.Locator("h1").First;

            await Assertions.Expect(heading).ToContainTextAsync("Request sent to training provider");
        }

        internal async Task<string> GetCohortId() => await CohortReference.InnerTextAsync();
        internal async Task<string> GetFundingType() => await FundingType.InnerTextAsync();
        internal async Task<string> GetTrainingProvider() => await TrainingProvider.InnerTextAsync();
        internal async Task<string> GetNamesOfLearners() => await NamesOfLearners.InnerTextAsync();

        internal async Task VerifyTrainingStatusDetails(string cohortRef, string fundingType)
        {
            await Assertions.Expect(CohortReference).ToBeVisibleAsync();
            await Assertions.Expect(TrainingProvider).ToBeVisibleAsync();
            await Assertions.Expect(FundingType).ToBeVisibleAsync();
            await Assertions.Expect(NamesOfLearners).ToBeVisibleAsync();

            await Assertions.Expect(CohortReference).ToContainTextAsync(cohortRef);
            await Assertions.Expect(FundingType).ToContainTextAsync(fundingType);
        }

    }
}
