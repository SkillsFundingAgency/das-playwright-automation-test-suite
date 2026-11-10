using System;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class ManageTheStandardsYouDeliverPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage your apprenticeships");
    }

    public async Task<ManageAStandard_TeacherPage> AccessTeacher()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Teacher - Postgraduate (level 6)"}).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<ManageTheStandardsYouDeliverPage> VerifyStandardPresence(string standardName, bool shouldExist = true)
    {
        var locator = page.Locator($"//a[contains(@class, 'govuk-link') and normalize-space(text())='{standardName}']");

        var count = await locator.CountAsync();

        if (shouldExist && count == 0)
            throw new Exception($"Expected to find the standard '{standardName}', but it was not listed.");

        if (!shouldExist && count > 0)
            throw new Exception($"The standard '{standardName}' was found on the page, but it should NOT be listed.");

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }

    public async Task<ManageTheStandardsYouDeliverPage> VerifyOrangeMoreDetailsNeededTagForStandardAsync(string standardName, bool shouldExist = true)
    {
        var child = page.Locator(".govuk-link", new PageLocatorOptions { HasTextString = standardName });
        var listItem = child.Locator("../..");
        var locator = listItem.Locator(".govuk-warning-text__text");
        
        var count = await locator.CountAsync();

        if (shouldExist && count == 0)
            throw new Exception($"Expected to find an orange 'More details needed' tag for the standard '{standardName}', but it was not found.");

        if (!shouldExist && count > 0)
            throw new Exception($"Found an orange 'More details needed' tag for the standard '{standardName}', but it should NOT be present.");

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }


    public async Task<SelectAStandardPage> AccessAddStandard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add apprenticeship" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectAStandardPage(context));

    }

    public async Task<ManageAStandardPage> AccessActuaryLevel7(string standardName)
    {
        await page.GetByRole(AriaRole.Link, new() { Name = standardName }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandardPage(context, standardName));
    }

    public async Task<YourStandardsAndTrainingVenuesPage> ReturnToYourStandardsAndTrainingVenues()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage your training courses", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new YourStandardsAndTrainingVenuesPage(context));
    }

}
