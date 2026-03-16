using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class UpdateStandardsWithNewContactDetailsPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Update your existing standards with these contact details");
        }

        public async Task<CheckDetailsPage> NoDontUpdateExistingStandards()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "No, I just want to save them as an option for when I add a new standard" }).CheckAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new CheckDetailsPage(context));
        }
        public async Task<UpdateContactDetailsStandardPage> YesUpdateExistingStandards()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I want to choose which standards to update with these details" }).CheckAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new UpdateContactDetailsStandardPage(context));
        }
    }

}