using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;
using System.Text.RegularExpressions;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class ContactDetailsSavedPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Contact details updated");
        }

        public async Task<YourStandardsAndTrainingVenuesPage> ReturnToManagingStandardsDashboard()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Manage your courses" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

            return await VerifyPageAsync(() => new YourStandardsAndTrainingVenuesPage(context));
        }

        public async Task VerifyUpdatedContactDetails(string? expectedEmail = null, string? expectedPhone = null)
        {
            var confirmationText = page
       .GetByText(new Regex(@"You have updated your contact details|The contact details for your chosen training is now"));


            if (!string.IsNullOrEmpty(expectedEmail))
            {
                await Assertions.Expect(confirmationText).ToContainTextAsync(expectedEmail);
            }

            if (!string.IsNullOrEmpty(expectedPhone))
            {
                await Assertions.Expect(confirmationText).ToContainTextAsync(expectedPhone);
            }
        }
    }

}