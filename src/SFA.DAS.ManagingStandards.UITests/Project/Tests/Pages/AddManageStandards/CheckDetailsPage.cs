using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;
using System.Text.RegularExpressions;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class CheckDetailsPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check details");
        }

        public async Task<ContactDetailsSavedPage> ConfirmUpdateContactDetailsAndContinue()
        {
            await (await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).IsVisibleAsync()
             ? page.GetByRole(AriaRole.Button, new() { Name = "Confirm" })
             : page.GetByRole(AriaRole.Button, new() { Name = "Continue" })).ClickAsync();

            return await VerifyPageAsync(() => new ContactDetailsSavedPage(context));
        }
    }
}