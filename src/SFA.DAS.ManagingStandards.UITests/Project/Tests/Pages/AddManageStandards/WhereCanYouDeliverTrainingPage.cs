namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class WhereCanYouDeliverTrainingPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Where can you deliver this training?");
    }

    public async Task<ManageAStandard_TeacherPage> SelectDerbyRutlandRegionsAndConfirm()
    {
        await SelectCheckboxByText("Derby");

        await SelectCheckboxByText("Rutland");

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<ManageAnAppUnitPage> SelectRegionsAndConfirm(string[] regions, string standardName)
    {
        foreach (var region in regions)
        {
            await SelectCheckboxByText(region);
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAnAppUnitPage(context, standardName));
    }

    public async Task<ManageAnAppUnitPage> SelectRegionsAndContinue(string[] regions, string standardName)
    {
        foreach (var region in regions)
        {
            await SelectCheckboxByText(region);
        }       

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAnAppUnitPage(context, standardName));
    }
    
    public async Task<ManageAStandard_TeacherPage> EditRegionsAddLutonEssexAndConfirm()
    {
        await SelectCheckboxByText("Luton");

        await SelectCheckboxByText("Essex");

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    private async Task SelectCheckboxByText(string text)
    {
        var checkbox = page.Locator(".govuk-checkboxes__item").Filter(new() { HasText = text }).First.Locator("input[type='checkbox']");

        await checkbox.CheckAsync();
    }
}
