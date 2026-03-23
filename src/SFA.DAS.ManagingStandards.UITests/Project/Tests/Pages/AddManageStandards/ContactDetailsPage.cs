using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class ContactDetailsPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Contact details");
        }

        public async Task<SaveContactDetailsPage> ChangeContactDetails()
        {

            await page.GetByRole(AriaRole.Link, new() { Name = "Change" }).ClickAsync();

            return await VerifyPageAsync(() => new SaveContactDetailsPage(context));
        }
    }

}