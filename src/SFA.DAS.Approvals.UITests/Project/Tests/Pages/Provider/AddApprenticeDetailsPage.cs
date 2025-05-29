using Azure;
using SFA.DAS.Approvals.UITests.Helpers.DataHelpers;
using SFA.DAS.EmployerPortal.UITests.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Tests.Pages.Provider
{

    internal class AddApprenticeDetailsPage(ScenarioContext context) : ApprovalsProviderBasePage(context)
    {
        private ILocator ulnTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "Unique learner number" });
        private ILocator firstNameTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "FirstName" });
        private ILocator lastNameTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "LastName" });
        private ILocator emailTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "Email" });
        private ILocator dateOfBirthTextBox_Day => page.GetByRole(AriaRole.Textbox, new() { Name = "BirthDay" });
        private ILocator dateOfBirthTextBox_Month => page.GetByRole(AriaRole.Textbox, new() { Name = "BirthMonth" });
        private ILocator dateOfBirthTextBox_Year => page.GetByRole(AriaRole.Textbox, new() { Name = "BirthYear" });
        private ILocator trainingStartMonthTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "StartMonth" });
        private ILocator trainingStartYearTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "StartYear" });
        private ILocator trainingEndMonthTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "EndMonth" });
        private ILocator trainingEndYearTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "EndYear" });
        private ILocator trainingCostTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "Cost" });
        private ILocator referenceTextBox => page.GetByRole(AriaRole.Textbox, new() { Name = "Reference" });

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Add apprentice details");
        }

        public async Task ValidateApprenticeDetailsMatchWithILRData()
        {
            var apprenticeDetails = context.GetValue<Apprenticeship>().ApprenticeDetails;
            await Assertions.Expect(ulnTextBox).ToHaveAttributeAsync("value", apprenticeDetails.ULN);
            await Assertions.Expect(firstNameTextBox).ToHaveAttributeAsync("value", apprenticeDetails.FirstName);
            await Assertions.Expect(lastNameTextBox).ToHaveAttributeAsync("value", apprenticeDetails.LastName);
            await Assertions.Expect(emailTextBox).ToHaveAttributeAsync("value", apprenticeDetails.Email);
            await Assertions.Expect(dateOfBirthTextBox_Day).ToHaveAttributeAsync("value", apprenticeDetails.DateOfBirth.Day.ToString("00"));
            await Assertions.Expect(dateOfBirthTextBox_Month).ToHaveAttributeAsync("value", apprenticeDetails.DateOfBirth.Month.ToString("00"));
            await Assertions.Expect(dateOfBirthTextBox_Year).ToHaveAttributeAsync("value", apprenticeDetails.DateOfBirth.Year.ToString("0000"));
            
            var trainingDetails = context.GetValue<Apprenticeship>().TrainingDetails;
            await Assertions.Expect(trainingStartMonthTextBox).ToHaveAttributeAsync("value", trainingDetails.StartDate.Month.ToString("00"));
            await Assertions.Expect(trainingStartYearTextBox).ToHaveAttributeAsync("value", trainingDetails.StartDate.Year.ToString("0000"));
            await Assertions.Expect(trainingEndMonthTextBox).ToHaveAttributeAsync("value", trainingDetails.EndDate.Month.ToString("00"));
            await Assertions.Expect(trainingEndYearTextBox).ToHaveAttributeAsync("value", trainingDetails.EndDate.Year.ToString("0000"));
            await Assertions.Expect(trainingCostTextBox).ToHaveAttributeAsync("value", trainingDetails.TotalPrice.ToString());
            await Assertions.Expect(referenceTextBox).ToHaveAttributeAsync("value", "");




        }


    }
}
