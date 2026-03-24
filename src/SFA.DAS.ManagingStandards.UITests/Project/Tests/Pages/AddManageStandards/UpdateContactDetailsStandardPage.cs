using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class UpdateContactDetailsStandardPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select the training that you want these contact details to apply to");
        }

        public async Task<CheckDetailsPage> YesUpdateAllStandardsContactDetails()
        {
            await page.GetByRole(AriaRole.Checkbox, new() { Name = "Select all" }).CheckAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new CheckDetailsPage(context));
        }
    }

}