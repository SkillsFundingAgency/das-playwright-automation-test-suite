namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class VenueAndDelivery_ApprenticeshipUnitPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose a training venue");
    }

    public async Task<AddAstandardPage> ChooseTheVenueDeliveryAndContinue_ApprenticeshipUnit(string standardname)
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "CENTRAL HAIR ESSEX" }).CheckAsync();
        
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAstandardPage(context, standardname));
    }

    public async Task<AnyWhereInEngland_ApprenticeshipUnitPage> ChooseTheVenueDeliveryAndContinueToDeliver_ApprenticeshipUnit()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "CENTRAL HAIR ESSEX" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AnyWhereInEngland_ApprenticeshipUnitPage(context));
    }
}