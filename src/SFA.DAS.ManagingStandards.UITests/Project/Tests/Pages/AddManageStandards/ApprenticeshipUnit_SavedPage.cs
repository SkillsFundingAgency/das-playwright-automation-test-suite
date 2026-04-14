using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class ApprenticeshipUnit_SavedPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Training saved");
        }

        public async Task<ManageYourAppUnitPage> GoToTrainingAndVenues()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "training types and venues" }).ClickAsync();

            return await VerifyPageAsync(() => new ManageYourAppUnitPage(context));
        }
    }

}