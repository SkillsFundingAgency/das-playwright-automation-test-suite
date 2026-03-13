using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class VenueAddedPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Venue details and standards");
        }

        public async Task<VenueDetailsPage> Click_ChangeVenueName()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Change venue name" }).ClickAsync();

            return await VerifyPageAsync(() => new VenueDetailsPage(context));
        }
    }

}