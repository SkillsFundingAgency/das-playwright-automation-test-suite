using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System.Globalization;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ViewApprenticeDetails_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region Locators
        private ILocator Employer => page.Locator("#employer-value");
        private ILocator Course => page.Locator("#course-value").First;
        private ILocator ULN => page.Locator("#uln-value");
        private ILocator FirstName => page.Locator("#firstname-value");
        private ILocator LastName => page.Locator("#lastname-value");
        private ILocator DoB => page.Locator("#dateofbirth-value");
        private ILocator Email => page.GetByRole(AriaRole.Textbox, new() { Name = "Email address" });
        private ILocator PlannedTrainingStartDate => page.Locator("#startdate-value");
        private ILocator PlannedTrainingEndDate => page.Locator("#enddate-value");
        private ILocator Price => page.Locator("#cost-value");
        #endregion


        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("View apprentice details");

        internal async Task VerifyBanner(string title, string content) => await Assertions.Expect(page.GetByLabel(title)).ToContainTextAsync(content);

        internal async Task<RecognitionOfPriorLearningPage> UpdateEmail(string email)
        {
            await Email.FillAsync(email);
            await ClickOnButton("Continue");
            return await VerifyPageAsync(() => new RecognitionOfPriorLearningPage(context));
        }

        internal async Task VerifyApprenticeshipDetails(Apprenticeship apprenticeship)
        {
            var expectedDOB = apprenticeship.ApprenticeDetails.DateOfBirth.ToString("d MMM yyyy", CultureInfo.InvariantCulture);
            var expectedTrainingStartDate = apprenticeship.TrainingDetails.StartDate.ToString("MMM yyyy", CultureInfo.InvariantCulture); 
            var expectedTrainingEndDate = apprenticeship.TrainingDetails.EndDate.ToString("MMM yyyy", CultureInfo.InvariantCulture);
            var expectedPrice = apprenticeship.TrainingDetails.TotalPrice.ToString("C0");            


            await Assertions.Expect(Employer).ToContainTextAsync(apprenticeship.EmployerDetails.EmployerName);
            await Assertions.Expect(Course).ToContainTextAsync(apprenticeship.TrainingDetails.CourseTitle);
            await Assertions.Expect(ULN).ToContainTextAsync(apprenticeship.ApprenticeDetails.ULN.ToString());
            await Assertions.Expect(FirstName).ToContainTextAsync(apprenticeship.ApprenticeDetails.FirstName);
            await Assertions.Expect(LastName).ToContainTextAsync(apprenticeship.ApprenticeDetails.LastName);
            await Assertions.Expect(DoB).ToContainTextAsync(expectedDOB);
            await Assertions.Expect(Email).ToHaveValueAsync(apprenticeship.ApprenticeDetails.Email);
            await Assertions.Expect(PlannedTrainingStartDate).ToContainTextAsync(expectedTrainingStartDate);
            await Assertions.Expect(PlannedTrainingEndDate).ToContainTextAsync(expectedTrainingEndDate);
            await Assertions.Expect(Price).ToContainTextAsync(expectedPrice);
        }



    }
}
