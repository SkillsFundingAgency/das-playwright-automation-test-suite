namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class ManageYourShortCoursesPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage your apprenticeship units");
    }
    public async Task<SelectAStandardPage> AccessAddApprenticeshipUnit()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add an apprenticeship unit" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectAStandardPage(context,"Select an apprenticeship unit"));

    }

}
