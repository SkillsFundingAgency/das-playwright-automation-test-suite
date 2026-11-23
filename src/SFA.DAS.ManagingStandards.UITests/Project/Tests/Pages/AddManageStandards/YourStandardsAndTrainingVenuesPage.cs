using System;
using static SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.VenueAndDelivery_ApprenticeshipUnitPage;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class YourStandardsAndTrainingVenuesPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage your apprenticeships and apprenticeship units");
    }

    public async Task<TrainingVenuesPage> AccessTrainingLocations()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Training venues", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new TrainingVenuesPage(context));
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

    public async Task VerifyHasBanner(string standardName, string bannerMessage)
    {
        var parent = page.Locator("li", new PageLocatorOptions { HasTextString = standardName });
        var listItem = parent;
        string banner = bannerMessage switch
        {
            "Funding closing for new starts" => ".govuk-tag--orange",
            "Funding closed for new starts"  => ".govuk-tag--grey",
             _        => throw new ArgumentException($"Unknown message type: {bannerMessage}")
        };
        await Assertions.Expect(listItem.Locator(banner)).ToContainTextAsync(bannerMessage);   
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