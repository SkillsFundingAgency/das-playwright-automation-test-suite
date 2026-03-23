using static SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.VenueAndDelivery_ApprenticeshipUnitPage;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class AddAstandardPage(ScenarioContext context, string standardName) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync(standardName);
    }

    public async Task<UseYourSavedContactDetailsPage> YesStandardIsCorrectAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new UseYourSavedContactDetailsPage(context));
    }
    public async Task<UseYourSavedContactDetailsPage> YesStandardIsCorrectAndContinue_ApprenticeshipUnit()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new UseYourSavedContactDetailsPage(context, "Do you want to use your saved contact details for this apprenticeship unit?"));
    }

    public async Task<ApprenticeshipUnit_SavedPage> Save_NewApprenticeshipUnit_Continue()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save apprenticeship unit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipUnit_SavedPage(context));
    }

    public async Task<ManageTheStandardsYouDeliverPage> Save_NewStandard_Continue()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save standard" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }
}
