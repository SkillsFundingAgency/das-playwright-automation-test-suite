using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers;
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

    internal class CheckApprenticeDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator EmployerNameValue => page.Locator("#employer-value");
        private ILocator TrainingCourseValue => page.Locator("#course-value");
        private ILocator DeliveryModelValue => page.Locator("#delivery-model-value");
        private ILocator UlnValue => page.Locator("#uln-value");
        private ILocator FirstNameValue => page.Locator("#firstname-value");
        private ILocator LastNameValue => page.Locator("#lastname-value");
        private ILocator DateOfBirthValue => page.Locator("#dateofbirth-value");
        private ILocator EmailValue => page.GetByRole(AriaRole.Textbox, new() { Name = "Email" });
        private ILocator PlannedTrainingStartDateValue => page.Locator("#startdate-value");
        private ILocator PlannedTrainingEndDateValue => page.Locator("#enddate-value");
        private ILocator TotalPriceValue => page.Locator("#cost-value");
        private ILocator ReferenceValue => page.GetByRole(AriaRole.Textbox, new() { Name = "Reference (optional)" });
        private ILocator AddButton => page.GetByRole(AriaRole.Button, new() { Name = "Add" });
        private ILocator ErrorSummary => page.GetByRole(AriaRole.Heading, new() { Name = "There is a problem" });
        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Check apprentice details");
        }

        internal async Task ValidateApprenticeDetailsMatchWithILRData(Apprenticeship apprenticeship)
        {
            var employerDetails = apprenticeship.EmployerDetails;           
            await Assertions.Expect(EmployerNameValue).ToHaveTextAsync(employerDetails.EmployerName);


            var apprenticeDetails = apprenticeship.ApprenticeDetails;       
            await Assertions.Expect(UlnValue).ToContainTextAsync(apprenticeDetails.ULN.ToString());
            await Assertions.Expect(FirstNameValue).ToContainTextAsync(apprenticeDetails.FirstName);
            await Assertions.Expect(LastNameValue).ToContainTextAsync(apprenticeDetails.LastName);
            await Assertions.Expect(DateOfBirthValue).ToContainTextAsync(DateTimeExtension.FormatWithCustomMonth(apprenticeDetails.DateOfBirth));   //ToContainTextAsync(apprenticeDetails.DateOfBirth.ToString("dd MMM yyyy"));
            await Assertions.Expect(EmailValue).ToHaveValueAsync(apprenticeDetails.Email);

            var trainingDetails = apprenticeship.TrainingDetails;
            await Assertions.Expect(PlannedTrainingStartDateValue).ToContainTextAsync(DateTimeExtension.FormatWithCustomMonth(trainingDetails.StartDate));
            await Assertions.Expect(PlannedTrainingEndDateValue).ToContainTextAsync(DateTimeExtension.FormatWithCustomMonth(trainingDetails.EndDate));
            await Assertions.Expect(TotalPriceValue).ToContainTextAsync(trainingDetails.TotalPrice.ToString("C0"));
            await Assertions.Expect(ReferenceValue).ToHaveValueAsync("");
        }

        internal async Task ClickAddButton()
        {
            await AddButton.ClickAsync();           
        }

        internal async Task VerfiyErrorMessage(string fieldName, string errorMsg)
        {
            //assert error summary on top of the page
            await Assertions.Expect(ErrorSummary).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = errorMsg }).First).ToBeVisibleAsync();

            //assert error message is displayed above the text box too
            ILocator errorLocator = page.Locator($"#error-message-{fieldName}");
            if (errorMsg == "")
            {
                await Assertions.Expect(errorLocator).ToBeHiddenAsync();
            }
            else
            {
                await Assertions.Expect(errorLocator).ToBeVisibleAsync();
                await Assertions.Expect(errorLocator).ToContainTextAsync(errorMsg);
            }            

            
        }



    }
}
