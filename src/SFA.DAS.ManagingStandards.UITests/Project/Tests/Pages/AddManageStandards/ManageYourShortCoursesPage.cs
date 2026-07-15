namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class ManageYourAppUnitPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage your apprenticeship units");
    }

    public async Task VerifyAppUnitIsDeleted()
    {
        await Assertions.Expect(page.GetByLabel("Apprenticeship unit deleted").GetByRole(AriaRole.Paragraph)).ToContainTextAsync("Your record has been updated");
    }
    public async Task<SelectAStandardPage> AccessAddApprenticeshipUnit()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add an apprenticeship unit" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectAStandardPage(context, "Select an apprenticeship unit"));
    }

    public async Task<ManageAnAppUnitPage> ManageAnAppUnitPage(string standardName)
    {
        await page.GetByRole(AriaRole.Link, new() { Name = standardName }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAnAppUnitPage(context, standardName));
    }

}
