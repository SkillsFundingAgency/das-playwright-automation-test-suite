using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class AddRecognitionOfPriorLearningDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        private ILocator ContinueButton => page.GetByRole(AriaRole.Button);
        private ILocator TrainingTotalHoursTextBox => page.Locator("#TrainingTotalHours");
        private ILocator DurationReducedByHoursTextBox => page.Locator("#DurationReducedByHours");
       // private ILocator DurationReducedByTextBox => page.Locator("#DurationReducedBy");
        private ILocator ReducedDuration => page.Locator("#radio-notreduced");
        private ILocator PriceReduced => page.Locator("#PriceReduced");

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add recognition of prior learning details");
        }

        public async Task<ApproveApprenticeDetailsPage> EnterRPLDataAndContinue(Apprenticeship apprentice)
        {
            var rpl = apprentice.RPLDetails;

            await TrainingTotalHoursTextBox.FillAsync(rpl.TrainingTotalHours.ToString());
            await DurationReducedByHoursTextBox.FillAsync(rpl.DurationReducedBy.ToString());
           // await ReducedDuration.ClickAsync();
            await PriceReduced.FillAsync(rpl.PriceReducedBy.ToString());
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }


        }
}
