using System;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class ManageTheStandardsYouDeliverPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#header-standards")).ToContainTextAsync("Manage your standards");
    }

    public async Task<ManageAStandard_TeacherPage> AccessPodiatrist()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Podiatrist (level 6)" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<ChooseTrainingTypePage> ReturnToYourStandardsAndTrainingVenues()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ChooseTrainingTypePage(context));
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
        var locator = page.Locator($@"
        //td[
            .//strong[contains(@class, 'govuk-tag--orange') and normalize-space(text())='More details needed']
            and .//a[normalize-space(text())='{standardName}']
        ]");

        var count = await locator.CountAsync();

        if (shouldExist && count == 0)
            throw new Exception($"Expected to find an orange 'More details needed' tag for the standard '{standardName}', but it was not found.");

        if (!shouldExist && count > 0)
            throw new Exception($"Found an orange 'More details needed' tag for the standard '{standardName}', but it should NOT be present.");

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }


    public async Task<SelectAStandardPage> AccessAddStandard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add a standard" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectAStandardPage(context));

    }

    public async Task<ManageAStandardPage> AccessActuaryLevel7(string standardName)
    {
        await page.GetByRole(AriaRole.Link, new() { Name = standardName }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandardPage(context, standardName));
    }
}
