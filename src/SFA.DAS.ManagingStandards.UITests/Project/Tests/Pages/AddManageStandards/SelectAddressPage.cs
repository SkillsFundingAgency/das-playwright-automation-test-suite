using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class SelectAddressPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select an address");
        }

        public async Task<AddVenueDetailsPage> EnterPostcodeAndContinue()
        {
            var postcode = managingStandardsDataHelpers.PostCode;
            var fullAddress = managingStandardsDataHelpers.FullAddressDetails;

            await page.Locator("input[role='combobox']").FillAsync(postcode);
            await page.Locator($"li:has-text(\"{fullAddress}\")").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new AddVenueDetailsPage(context));
        }
    }

}