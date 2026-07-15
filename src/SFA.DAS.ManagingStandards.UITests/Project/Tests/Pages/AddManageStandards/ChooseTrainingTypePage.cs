namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class ChooseTrainingTypePage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose training type");
    }

    public async Task<ManageTheStandardsYouDeliverPage> AccessStandards_Apprenticeships()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Apprenticeships", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }
    public async Task<ManageYourAppUnitPage> AccessStandards_ApprenticeshipsUnits()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Apprenticeship units", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ManageYourAppUnitPage(context));
    }

    public async Task<YourStandardsAndTrainingVenuesPage> NavigateBackToReviewYourDetails()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new YourStandardsAndTrainingVenuesPage(context));
    }

}
