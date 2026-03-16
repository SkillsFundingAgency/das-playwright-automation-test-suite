using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class ChooseTheAddressPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose the address");
        }

        public async Task<AddVenueDetailsPage> ChooseTheAddressAndContinue()
        {
            await page.Locator("#SelectedAddressUprn").SelectOptionAsync(["100021525713"]);

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new AddVenueDetailsPage(context));
        }
    }

}