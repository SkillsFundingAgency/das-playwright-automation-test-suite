namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class UseYourSavedContactDetailsPage(ScenarioContext context, string pageTitle ="Do you want to use your saved contact details for this standard?"): ManagingStandardsBasePage(context)
{
    private readonly string _pageTitle = pageTitle;

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl"))
            .ToContainTextAsync(_pageTitle);
    }
    public async Task<YourContactInformationForThisStandardPage> YesUseExistingContactDetails()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourContactInformationForThisStandardPage(context));
    }
    public async Task<YourContactInformationForThisAppUnit> YesUseExistingContactDetails_ApprenticeshipUnit()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourContactInformationForThisAppUnit(context));
    }

    public async Task<YourContactInformationForThisAppUnit> NoDontUseExistingContactDetails_ApprenticeshipUnit()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourContactInformationForThisAppUnit(context));
    }

    public async Task<YourContactInformationForThisStandardPage> NoDontUseExistingContactDetails()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourContactInformationForThisStandardPage(context));
    }
}
