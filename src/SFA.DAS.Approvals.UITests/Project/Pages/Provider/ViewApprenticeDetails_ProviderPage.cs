using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System.Globalization;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ViewApprenticeDetails_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region Locators
        private ILocator Employer => page.Locator("#employer-value");
        private ILocator Course => page.Locator("#course-value").First;
        private ILocator ULN => page.Locator("dt:has-text('Unique learner number') + dd");
        private ILocator FirstName => page.Locator("#firstname-value");
        private ILocator LastName => page.Locator("#lastname-value");
        private ILocator FullName => page.Locator("dt:has-text('Name') + dd").First;
        private ILocator DoB => page.Locator("dt:has-text('Date of birth') + dd").First;
        private ILocator Email => page.Locator("dt:has-text('Email address') + dd");
        private ILocator EmailBox => page.Locator("#Email");
        private ILocator ChangeEmailBtn => page.Locator("#change-email-link");
        private ILocator PlannedTrainingStartDate => page.Locator("#startdate-value").First;
        private ILocator PlannedTrainingEndDate => page.Locator("#enddate-value").First;
        private ILocator Price => page.Locator("#cost-value").First;
        #endregion


        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("View details");

        internal async Task VerifyBanner(string title, string content) => await Assertions.Expect(page.GetByLabel(title)).ToContainTextAsync(content);

        internal async Task UpdateEmail(string email)
        {
            await ChangeEmailBtn.ClickAsync();
            await EmailBox.FillAsync(email);
            await ClickOnButton("Continue");
        }

        internal async Task VerifyApprenticeshipDetails(Apprenticeship apprenticeship)
        {
            var expectedDOB = apprenticeship.ApprenticeDetails.DateOfBirth.ToString("d MMM yyyy", CultureInfo.InvariantCulture);
            var expectedTrainingStartDate = apprenticeship.TrainingDetails.StartDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture); 
            var expectedTrainingEndDate = apprenticeship.TrainingDetails.EndDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture);
            var expectedPrice = apprenticeship.TrainingDetails.TotalPrice.ToString("C0");            


            await Assertions.Expect(Employer).ToContainTextAsync(apprenticeship.EmployerDetails.EmployerName);
            await Assertions.Expect(Course).ToContainTextAsync(apprenticeship.TrainingDetails.CourseTitle);
            await Assertions.Expect(ULN).ToContainTextAsync(apprenticeship.ApprenticeDetails.ULN);
            await Assertions.Expect(FullName).ToContainTextAsync(apprenticeship.ApprenticeDetails.FirstName);
            await Assertions.Expect(FullName).ToContainTextAsync(apprenticeship.ApprenticeDetails.LastName);
            await Assertions.Expect(DoB).ToContainTextAsync(expectedDOB);
            await Assertions.Expect(Email).ToContainTextAsync(apprenticeship.ApprenticeDetails.Email);
            await Assertions.Expect(PlannedTrainingStartDate).ToContainTextAsync(expectedTrainingStartDate);
            await Assertions.Expect(PlannedTrainingEndDate).ToContainTextAsync(expectedTrainingEndDate);
            await Assertions.Expect(Price).ToContainTextAsync(expectedPrice);
        }



    }
}
