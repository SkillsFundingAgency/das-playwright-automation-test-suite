namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class AnyWhereInEngland_ApprenticeshipUnitPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Can you deliver this training at employers’ addresses anywhere in England?");
    }

    public async Task<ManageAStandard_TeacherPage> YesDeliverAnyWhereInEngland_ManageStandard()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I can deliver training" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<ManageAnAppUnitPage> EditDeliverAnyWhereInEnglandToYes(string standardName)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I can deliver training" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAnAppUnitPage(context, standardName));
    }

    public async Task<WhereCanYouDeliverTrainingPage> EditDeliverAnyWhereInEnglandToNo()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, I want to select the" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new WhereCanYouDeliverTrainingPage(context));
    }

    public async Task<AddAstandardPage> YesDeliverAnyWhereInEngland_AddApprenticeshipUnit(string standardname)
    {
        await page.Locator("#Yes").CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAstandardPage(context, standardname));
    }

    public async Task<WhereCanYouDeliverTrainingPage> NoDeliverAnyWhereInEngland()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, I want to select the" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhereCanYouDeliverTrainingPage(context));
    }
}
