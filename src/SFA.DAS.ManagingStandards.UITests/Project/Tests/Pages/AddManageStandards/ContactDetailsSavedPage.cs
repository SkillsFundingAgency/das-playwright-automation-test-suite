using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class ContactDetailsSavedPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Contact details saved");
        }

        public async Task<YourStandardsAndTrainingVenuesPage> ReturnToManagingStandardsDashboard()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Manage your standards" }).ClickAsync();

            return await VerifyPageAsync(() => new YourStandardsAndTrainingVenuesPage(context));
        }

        public async Task VerifyUpdatedContactDetails(string? expectedEmail = null, string? expectedPhone = null)
        {
            var confirmationText = page.GetByText("You have updated your contact details", new() { Exact = false });

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