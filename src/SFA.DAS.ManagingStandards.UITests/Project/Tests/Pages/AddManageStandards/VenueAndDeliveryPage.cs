namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class VenueAndDeliveryPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Venue and delivery");
    }

    public async Task<TrainingVenuesPage> ChooseTheVenueDeliveryAndContinue()
    {
        await page.Locator("#TrainingVenueNavigationId")
                  .SelectOptionAsync(new[] { "CENTRAL HAIR ESSEX" });

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Day release" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Block release" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingVenuesPage(context));
    }
}