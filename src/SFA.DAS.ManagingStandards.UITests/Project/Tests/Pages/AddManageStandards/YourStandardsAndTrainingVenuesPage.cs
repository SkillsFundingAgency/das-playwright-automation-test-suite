using static SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.VenueAndDelivery_ApprenticeshipUnitPage;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class YourStandardsAndTrainingVenuesPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage training and venues");
    }

    public async Task<TrainingVenuesPage> AccessTrainingLocations()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Training venues", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingVenuesPage(context));
    }

    public async Task<ChooseTrainingTypePage> AccessTrainingTypesPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Training", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ChooseTrainingTypePage(context));
    }

    public async Task<ContactDetailsPage> AccessContactDetails()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Contact details" }).ClickAsync();

        return await VerifyPageAsync(() => new ContactDetailsPage(context));
    }


    public async Task<TrainingProviderOverviewPage> AccessProviderOverview()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Overview" }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingProviderOverviewPage(context));
    }

}
