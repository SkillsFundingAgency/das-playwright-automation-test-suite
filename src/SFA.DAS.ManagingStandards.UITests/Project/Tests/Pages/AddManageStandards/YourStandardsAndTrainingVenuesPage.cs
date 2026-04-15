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

    public async Task<AddYourDeliveryForecastPage> AccessDeliveryForecast()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add your delivery forecast" }).ClickAsync();

        return await VerifyPageAsync(() => new AddYourDeliveryForecastPage(context));
    }
}

public class AddYourDeliveryForecastPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#header-apprenticeshipUnit")).ToContainTextAsync("Add your delivery forecast for apprenticeship units");
    }

    public async Task<EnterYourDeliveryForecastPage> SelectAppUnit(string standardName)
    {
        await page.GetByRole(AriaRole.Link, new() { Name = standardName }).ClickAsync();

        return await VerifyPageAsync(() => new EnterYourDeliveryForecastPage(context, standardName));
    }
}

public class EnterYourDeliveryForecastPage(ScenarioContext context, string standardName) : ManagingStandardsBasePage(context)
{

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#header-apprenticeshipUnit")).ToContainTextAsync(standardName);
    }

    public async Task<AddYourDeliveryForecastPage> EnterAppUnitForecast()
    {
        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "Feb 1st to Apr 30th" }).FillAsync("3");

        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "May 1st to Jul 31st" }).FillAsync("2");

        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "Aug 1st to Oct 31st" }).FillAsync("2");

        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "Nov 1st to Jan 31st" }).FillAsync("1");
        
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and return" }).ClickAsync();

        return await VerifyPageAsync(() => new AddYourDeliveryForecastPage(context));
    }
}