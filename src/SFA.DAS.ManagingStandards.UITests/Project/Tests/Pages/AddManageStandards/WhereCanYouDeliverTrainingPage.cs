namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class WhereCanYouDeliverTrainingPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Where can you deliver this training?");
    }

    public async Task<ManageAStandard_TeacherPage> SelectDerbyRutlandRegionsAndConfirm()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Derby", Exact = true }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Rutland" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }
    public async Task<ManageAStandard_TeacherPage> EditRegionsAddLutonEssexAndConfirm()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Luton" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Essex" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }
}
