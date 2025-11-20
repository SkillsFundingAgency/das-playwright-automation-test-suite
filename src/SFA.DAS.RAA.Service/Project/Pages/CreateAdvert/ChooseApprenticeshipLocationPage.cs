using System;

namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class ChooseApprenticeshipLocationPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Where is this apprenticeship available?");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> ChooseAddressAndGoToCreateApprenticeshipPage(string locationType)
    {
        locationType = locationType.Trim().ToLower();

        switch (locationType)
        {
            case "national":
                await page.GetByRole(AriaRole.Radio, new() { Name = "Across all of England" }).CheckAsync();

                await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

                await NationalLocationInformation();
                break;

            case "multiple":
                await page.GetByRole(AriaRole.Radio, new() { Name = "At more than one location" }).CheckAsync();

                await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

                await SelectMultipleLocations();
                break;

            case "employer":
                await page.GetByRole(AriaRole.Radio, new() { Name = "At one location" }).CheckAsync();

                await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

                await SelectSingleLocation();

                break;

            case "different":
                await DifferentLocation();

                return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));

            case "all location types":

                await page.GetByRole(AriaRole.Radio, new() { Name = "At one location" }).CheckAsync();

                await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

                await SelectSingleLocation();

                await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

                await page.GetByRole(AriaRole.Link, new() { Name = "Locations" }).ClickAsync();

                await page.GetByRole(AriaRole.Radio, new() { Name = "At more than one location" }).CheckAsync();

                await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

                await SelectMultipleLocations();

                await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

                await page.GetByRole(AriaRole.Link, new() { Name = "Locations" }).ClickAsync();

                await page.GetByRole(AriaRole.Radio, new() { Name = "Across all of England" }).CheckAsync();

                await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

                await NationalLocationInformation();

                break;
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }

    private async Task SelectSingleLocation()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a location");

        var radioElements = await page.Locator(".govuk-radios__input").AllAsync();

        var radioElement = RandomDataGenerator.GetRandom(radioElements);

        await radioElement.ClickAsync();
    }

    private async Task SelectMultipleLocations()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add more than one location");

        var detailsElements = await page.Locator("details.govuk-details").AllAsync();

        if (detailsElements != null && detailsElements.Any())
        {
            foreach (var details in detailsElements)
            {
                var summary = details.Locator("summary.govuk-details__summary");

                if (summary == null)
                {
                    throw new NullReferenceException("summary is null.");
                }

                var isOpen = await details.GetAttributeAsync("open");

                if (string.IsNullOrEmpty(isOpen) || !isOpen.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    await summary.ClickAsync();
                }
            }
        }

        Random random = new();
        int numberOfLocations = random.Next(2, 11);

        var multipleLocationsCheckboxes = await page.GetByRole(AriaRole.Checkbox).AllAsync();

        var selectedMultipleLocationsCheckboxes = multipleLocationsCheckboxes.OrderBy(x => random.Next()).Take(numberOfLocations);

        foreach (var checkbox in selectedMultipleLocationsCheckboxes)
        {
            if (!await checkbox.IsCheckedAsync())
            {
                await checkbox.CheckAsync();
            }
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
    }

    private async Task NationalLocationInformation()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Recruit across all of England");

        await IFrameFillAsync("AdditionalInformation_ifr", RandomDataGenerator.GenerateRandomAlphabeticString(100));
    }

    public async Task<ImportantDatesPage> ChooseAddress(bool isEmployerAddress)
    {
        if (isEmployerAddress) await SelectRadioOptionByForAttribute("OtherLocation_1");

        else await DifferentLocation();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();


        return await VerifyPageAsync(() => new ImportantDatesPage(context));
    }

    private async Task DifferentLocation()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "At one location" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a location");

        await page.GetByRole(AriaRole.Link, new() { Name = "Add a new location" }).ClickAsync();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a new location");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Postcode" }).FillAsync(RAADataHelper.EmployerAddress);

        await page.GetByRole(AriaRole.Button, new() { Name = "Find address" }).ClickAsync();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select an address");

        var locations = await page.Locator("#SelectedLocation option").AllTextContentsAsync();

        var location = RandomDataGenerator.GetRandom(locations.Where(x => !x.ContainsCompareCaseInsensitive("Select your address")).ToList());

        await page.Locator("#SelectedLocation").SelectOptionAsync(new[] { location });

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a location");

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
    }
}
