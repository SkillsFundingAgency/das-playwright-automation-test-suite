using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class VenueDetailsPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change venue name");
        }

        public async Task<VenueAddedPage> UpdateVenueDetailsAndSubmit()
        {
            await page.Locator("#LocationName").FillAsync(managingStandardsDataHelpers.UpdatedVenueName);

            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

            return await VerifyPageAsync(() => new VenueAddedPage(context));
        }
    }

}