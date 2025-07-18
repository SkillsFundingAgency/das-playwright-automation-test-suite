using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.EmployerPortal.UITests.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{

    internal class AddApprenticeDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator employerName => page.Locator("#employer-value");
        private ILocator trainingCourse => page.Locator("#course-value");
        private ILocator deliveryModel => page.Locator("#delivery-model-value");
        private ILocator ulnTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "Unique learner number" });
        private ILocator firstNameTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "First Name" });
        private ILocator lastNameTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "Last Name" });
        private ILocator emailTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "Email" });
        private ILocator dateOfBirthTextBox_Day => page.GetByRole(AriaRole.Spinbutton, new() { Name = "Day" });
        private ILocator dateOfBirthTextBox_Month => page.GetByRole(AriaRole.Group, new() { Name = "Date of birth" }).GetByLabel("Month");
        private ILocator dateOfBirthTextBox_Year => page.GetByRole(AriaRole.Group, new() { Name = "Date of birth" }).GetByLabel("Year");
        private ILocator trainingStartMonthTextBox => page.GetByRole(AriaRole.Group, new() { Name = "Planned training start date" }).GetByLabel("Month");
        private ILocator trainingStartYearTextBox => page.GetByRole(AriaRole.Group, new() { Name = "Planned training start date" }).GetByLabel("Year");
        private ILocator trainingEndMonthTextBox => page.GetByRole(AriaRole.Group, new() { Name = "Planned training end date" }).GetByLabel("Month");
        private ILocator trainingEndYearTextBox => page.GetByRole(AriaRole.Group, new() { Name = "Planned training end date" }).GetByLabel("Year");
        private ILocator trainingCostTextBox => page.GetByRole(AriaRole.Spinbutton, new() { Name = "Total agreed apprenticeship" });
        private ILocator referenceTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "Reference (optional)" });
        private ILocator addButton => page.GetByRole(AriaRole.Button, new() { Name = "Add" });
        private ILocator errorSummary => page.GetByRole(AriaRole.Heading, new() { Name = "There is a problem" });
        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Add apprentice details");
        }

        internal async Task ValidateApprenticeDetailsMatchWithILRData(Apprenticeship apprenticeship)
        {
            var employerDetails = apprenticeship.EmployerDetails;           
            await Assertions.Expect(employerName).ToHaveTextAsync(employerDetails.EmployerName);


            var apprenticeDetails = apprenticeship.ApprenticeDetails;       
            await Assertions.Expect(ulnTextBox).ToHaveValueAsync(apprenticeDetails.ULN.ToString());
            await Assertions.Expect(firstNameTextBox).ToHaveValueAsync(apprenticeDetails.FirstName);
            await Assertions.Expect(lastNameTextBox).ToHaveValueAsync(apprenticeDetails.LastName);
            await Assertions.Expect(emailTextBox).ToHaveValueAsync(apprenticeDetails.Email);
            await Assertions.Expect(dateOfBirthTextBox_Day).ToHaveValueAsync(apprenticeDetails.DateOfBirth.Day.ToString());
            await Assertions.Expect(dateOfBirthTextBox_Month).ToHaveValueAsync(apprenticeDetails.DateOfBirth.Month.ToString());
            await Assertions.Expect(dateOfBirthTextBox_Year).ToHaveValueAsync(apprenticeDetails.DateOfBirth.Year.ToString());

            var trainingDetails = apprenticeship.TrainingDetails;           
            await Assertions.Expect(trainingStartMonthTextBox).ToHaveValueAsync(trainingDetails.StartDate.Month.ToString());
            await Assertions.Expect(trainingStartYearTextBox).ToHaveValueAsync(trainingDetails.StartDate.Year.ToString());
            await Assertions.Expect(trainingEndMonthTextBox).ToHaveValueAsync(trainingDetails.EndDate.Month.ToString());
            await Assertions.Expect(trainingEndYearTextBox).ToHaveValueAsync(trainingDetails.EndDate.Year.ToString());
            await Assertions.Expect(trainingCostTextBox).ToHaveValueAsync(trainingDetails.TotalPrice.ToString());
            await Assertions.Expect(referenceTextBox).ToHaveValueAsync("");

        }

        internal async Task ClickAddButton()
        {
            await addButton.ClickAsync();           
        }

        internal async Task VerfiyErrorMessage(string fieldName, string errorMsg)
        {
            //assert error summary on top of the page
            await Assertions.Expect(errorSummary).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = errorMsg })).ToBeVisibleAsync();

            //assert error message is displayed above the text box too
            ILocator errorLocator = page.Locator($"#error-message-{fieldName}");
            await Assertions.Expect(errorLocator).ToBeVisibleAsync();
            await Assertions.Expect(errorLocator).ToContainTextAsync(errorMsg);
        }



    }
}
