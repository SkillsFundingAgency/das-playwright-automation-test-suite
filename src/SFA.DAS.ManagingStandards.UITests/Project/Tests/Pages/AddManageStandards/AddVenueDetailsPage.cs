using Polly;
using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class AddVenueDetailsPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add venue name");
    }

    public async Task<TrainingLocation_ConfirmVenuePage> AddVenueDetailsAndSubmit()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Venue name" })
                  .FillAsync(managingStandardsDataHelpers.VenueName);

        // Commented out this for now as we might be reusing them
        // await page.Locator("#Website").FillAsync(managingStandardsDataHelpers.ContactWebsite);
        // await page.Locator("#EmailAddress").FillAsync(managingStandardsDataHelpers.EmailAddress);
        // await page.Locator("#PhoneNumber").FillAsync(managingStandardsDataHelpers.EmailAddress);

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingLocation_ConfirmVenuePage(context));
    }
}