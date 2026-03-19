namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class YourContactInformationForThisStandardPage(ScenarioContext context, string pageTitle = "Your contact information for this standard"): ManagingStandardsBasePage(context)
{
    private readonly string _pageTitle = pageTitle;

    public override async Task VerifyPage()
    {
        var heading = page.Locator(".govuk-heading-xl, .govuk-fieldset__heading");

        await Assertions.Expect(heading).ToContainTextAsync(_pageTitle);
    }

    public async Task<ManageAStandard_TeacherPage> UpdateContactInformation()
    {
        await DoNotUseExistingContactInformation();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<WhereWillThisStandardBeDeliveredPage> Add_ContactInformation()
    {
        await UseExistingContactInformation();

        return await VerifyPageAsync(() => new WhereWillThisStandardBeDeliveredPage(context));
    }
    public async Task<WhereWillThisStandardBeDeliveredPage> Add_ContactInformation_ApprenticeshipUnit()
    {
        await UseExistingContactInformation();

        return await VerifyPageAsync(() => new WhereWillThisStandardBeDeliveredPage(context, "Where can you deliver this apprenticeship unit?"));
    }

    public async Task<WhereWillThisStandardBeDeliveredPage> AddNewContactInformation()
    {
        await DoNotUseExistingContactInformation();

        return await VerifyPageAsync(() => new WhereWillThisStandardBeDeliveredPage(context));
    }

    public async Task UseExistingContactInformation()
    {
        var websiteTextbox = page.GetByRole(AriaRole.Textbox, new() { Name = "Your website page" });

        if (await websiteTextbox.CountAsync() == 0)
        {
            websiteTextbox = page.Locator("#StandardInfoUrl");
        }

        await websiteTextbox.FillAsync(managingStandardsDataHelpers.Website);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }

    private async Task DoNotUseExistingContactInformation()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email address" }).FillAsync(managingStandardsDataHelpers.EmailAddress);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Telephone number" }).FillAsync(managingStandardsDataHelpers.ContactNumber);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Your website page" }).FillAsync(managingStandardsDataHelpers.UpdatedWebsite);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    }
}
