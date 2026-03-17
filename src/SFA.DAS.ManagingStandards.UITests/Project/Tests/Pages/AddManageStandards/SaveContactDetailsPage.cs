using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
public partial class VenueAndDelivery_ApprenticeshipUnitPage
{
    public class SaveContactDetailsPage(ScenarioContext context) : ManagingStandardsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Save your contact details");
        }

        public async Task<UpdateStandardsWithNewContactDetailsPage> ChangeEmailAndPhonenumberContactDetails()
        {
            var email = page.Locator("#EmailAddress");
            await email.ClearAsync();
            await email.FillAsync(managingStandardsDataHelpers.UpdatedEmailAddress);

            var phone = page.Locator("#PhoneNumber");
            await phone.ClearAsync();
            await phone.FillAsync(managingStandardsDataHelpers.ContactNumber);

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new UpdateStandardsWithNewContactDetailsPage(context));
        }
        public async Task<UpdateStandardsWithNewContactDetailsPage> ChangeEmailAndPhonenumberContactDetails_Latest()
        {
            var email = page.Locator("#EmailAddress");
            await email.ClearAsync();
            await email.FillAsync(managingStandardsDataHelpers.EmailAddress);

            var phone = page.Locator("#PhoneNumber");
            await phone.ClearAsync();
            await phone.FillAsync(managingStandardsDataHelpers.NewlyUpdatedContactNumber);

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new UpdateStandardsWithNewContactDetailsPage(context));
        }
        public async Task<UpdateStandardsWithNewContactDetailsPage> ChangePhonenumberOnly()
        {
            var phone = page.Locator("#PhoneNumber");
            await phone.ClearAsync();
            await phone.FillAsync(managingStandardsDataHelpers.UpdatedContactNumber);

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new UpdateStandardsWithNewContactDetailsPage(context));
        }
        public async Task<UpdateStandardsWithNewContactDetailsPage> ChangeEmailOnly()
        {
            var email = page.Locator("#EmailAddress");
            await email.ClearAsync();
            await email.FillAsync(managingStandardsDataHelpers.NewlyUpdatedEmailAddress);

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new UpdateStandardsWithNewContactDetailsPage(context));
        }
    }

}